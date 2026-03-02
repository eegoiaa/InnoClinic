import { useState } from 'react';
import type { Doctor } from '../types/doctor';

export const useAppointmentForm = () => {
  const [values, setValues] = useState({
    specializationId: '',
    doctorId: '',
    serviceId: '',
    officeId: '',
    date: '',
    timeSlot: ''
  });

  const [isDirty, setIsDirty] = useState(false);

  const handleDoctorChange = (doctor: Doctor | null) => {
    setIsDirty(true);
    if (doctor) {
      setValues(prev => ({
        ...prev,
        doctorId: doctor.id,
        specializationId: doctor.specializationId,
        officeId: doctor.officeId 
      }));
    } else {
      setValues(prev => ({ ...prev, doctorId: '', officeId: '', specializationId: '' }));
    }
  };

  const isFormValid = !!(
    values.specializationId && 
    values.doctorId && 
    values.serviceId && 
    values.officeId && 
    values.date && 
    values.timeSlot
  );

  const isDateTimeEnabled = values.specializationId && values.serviceId;

  return {
    values,
    setValues,
    handleDoctorChange,
    isFormValid,
    isDateTimeEnabled,
    isDirty,
    setIsDirty
  };
};