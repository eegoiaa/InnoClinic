import { useEffect, useState } from 'react';
import { Stack, TextField, Autocomplete, Box, Divider, Button } from '@mui/material';

import { fetchDoctors } from '../api/doctorApi';
import { fetchServices } from '../api/serviceApi';
import { createAppointment } from '../api/appointmentApi';
import { ServiceCategoryType } from '../types/service';
import type { Doctor } from '../types/doctor';
import type { Service } from '../types/service';
import { TimeSlotPicker } from './TimeSlotPicker';

interface AppointmentFormProps {
  onDataChange: () => void;
  onSuccess: () => void;
}

export const AppointmentForm = ({ onDataChange, onSuccess }: AppointmentFormProps) => {
  const [values, setValues] = useState({
    specializationId: '', doctorId: '', serviceId: '', officeId: '', date: '', timeSlot: ''
  });

  // Состояние для отслеживания полей, которые пользователь покинул (F-1, F-2, F-3, F-4)
  const [touched, setTouched] = useState<Record<string, boolean>>({});

  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [services, setServices] = useState<Service[]>([]);
  const [specializations, setSpecializations] = useState<{id: string, name: string}[]>([]);
  const [offices, setOffices] = useState<{id: string, name: string}[]>([]);
  const [loadingServices, setLoadingServices] = useState(false);

  useEffect(() => {
    fetchDoctors().then(data => {
      setDoctors(data);
      const specs = data.map(d => ({ id: d.specializationId, name: d.specializationName }));
      setSpecializations(Array.from(new Map(specs.map(s => [s.id, s])).values()));
      const offs = data.map(d => ({ id: d.officeId, name: d.officeAddressName }));
      setOffices(Array.from(new Map(offs.map(o => [o.id, o])).values()));
    });
  }, []);

  useEffect(() => {
    if (values.specializationId) {
      setLoadingServices(true);
      const spec = specializations.find(s => s.id === values.specializationId);
      
      fetchServices(ServiceCategoryType.Consultations).then(res => {
        let group = res.groupedServices?.find(g => 
          spec && g.specialization.toLowerCase().includes(spec.name.toLowerCase().substring(0, 4))
        ) || res.groupedServices?.find(g => g.specialization === "Other");

        setServices(group ? group.services : (res.flatServices || []));
        setLoadingServices(false);
      }).catch(() => setLoadingServices(false));
    }
  }, [values.specializationId, specializations]);

  const handleDoctorSelect = (doctor: Doctor | null) => {
    onDataChange();
    if (doctor) {
      setValues(prev => ({
        ...prev, doctorId: doctor.id, specializationId: doctor.specializationId, officeId: doctor.officeId 
      }));
    } else {
      setValues(prev => ({ ...prev, doctorId: '', officeId: '', specializationId: '' }));
    }
  };

  const handleBlur = (field: string) => {
    setTouched(prev => ({ ...prev, [field]: true }));
  };

  const isDateTimeEnabled = !!(values.specializationId && values.serviceId);
  const isFormValid = isDateTimeEnabled && !!(values.doctorId && values.officeId && values.date && values.timeSlot);

  const handleSubmit = async () => {
    try {
      await createAppointment({
        patientId: "550e8400-e29b-41d4-a716-446655440000", 
        doctorId: values.doctorId,
        serviceId: values.serviceId,
        date: values.date,
        time: values.timeSlot
      });
      onSuccess();
    } catch (err: any) {
      alert(err.Message || "Ошибка при сохранении!");
    }
  };

  return (
    <Stack spacing={2.5}>
      {/* F-1: Специализация */}
      <Autocomplete
        options={specializations}
        getOptionLabel={(opt) => opt.name}
        value={specializations.find(s => s.id === values.specializationId) || null}
        onBlur={() => handleBlur('specialization')}
        onChange={(_, val) => {
          setValues(v => ({ ...v, specializationId: val?.id || '', doctorId: '', serviceId: '' }));
          onDataChange();
        }}
        renderInput={(params) => (
          <TextField 
            {...params} 
            label="Специализация" 
            required
            error={touched.specialization && !values.specializationId}
            helperText={touched.specialization && !values.specializationId ? "Please, choose the specialization" : ""}
          />
        )}
      />

      {/* F-3: Услуга */}
      <Autocomplete
        options={services}
        getOptionLabel={(opt) => opt.serviceName}
        loading={loadingServices}
        disabled={!values.specializationId}
        value={services.find(s => s.id === values.serviceId) || null}
        onBlur={() => handleBlur('service')}
        onChange={(_, val) => { setValues(v => ({ ...v, serviceId: val?.id || '' })); onDataChange(); }}
        renderInput={(params) => (
          <TextField 
            {...params} 
            label="Услуга" 
            required
            error={touched.service && !values.serviceId}
            helperText={touched.service && !values.serviceId ? "Please, choose the service" : ""}
          />
        )}
      />

      <Divider />

      {/* F-2: Врач */}
      <Autocomplete
        options={doctors.filter(d => !values.specializationId || d.specializationId === values.specializationId)}
        getOptionLabel={(opt) => opt.fullname}
        value={doctors.find(d => d.id === values.doctorId) || null}
        onBlur={() => handleBlur('doctor')}
        onChange={(_, val) => handleDoctorSelect(val)}
        renderInput={(params) => (
          <TextField 
            {...params} 
            label="Врач" 
            required
            error={touched.doctor && !values.doctorId}
            helperText={touched.doctor && !values.doctorId ? "Please, choose the doctor" : ""}
          />
        )}
      />

      {/* F-4: Офис */}
      <Autocomplete
        options={offices}
        getOptionLabel={(opt) => opt.name}
        value={offices.find(o => o.id === values.officeId) || null}
        disabled={!!values.doctorId}
        onBlur={() => handleBlur('office')}
        onChange={(_, val) => { setValues(v => ({ ...v, officeId: val?.id || '' })); onDataChange(); }}
        renderInput={(params) => (
          <TextField 
            {...params} 
            label="Офис" 
            required
            error={touched.office && !values.officeId}
            helperText={touched.office && !values.officeId ? "Please, choose the office" : ""}
          />
        )}
      />

      {/* Блок даты и времени разблокируется по AC-11 */}
      <Box sx={{ p: 2, bgcolor: isDateTimeEnabled ? '#f0f4ff' : '#fafafa', borderRadius: 4, border: '1px solid #e2e8f0' }}>
        <Stack spacing={2.5}>
          <TextField
            type="date" fullWidth label="Дата записи" disabled={!isDateTimeEnabled}
            InputLabelProps={{ shrink: true }}
            value={values.date}
            onChange={(e) => { setValues(v => ({ ...v, date: e.target.value })); onDataChange(); }}
          />
          <TimeSlotPicker 
            disabled={!isDateTimeEnabled || !values.date}
            selectedSlot={values.timeSlot}
            onSelect={(slot) => { setValues(v => ({ ...v, timeSlot: slot })); onDataChange(); }}
          />
        </Stack>
      </Box>

      <Button
        variant="contained" disabled={!isFormValid}
        fullWidth size="large" onClick={handleSubmit}
        sx={{ borderRadius: 3, py: 2, fontWeight: 800, bgcolor: '#1a237e' }}
      >
        ПОДТВЕРДИТЬ ЗАПИСЬ
      </Button>
    </Stack>
  );
};