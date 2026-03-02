import { useEffect, useState } from 'react';
import { useDebounce } from 'use-debounce';
import type { Doctor } from '../types/doctor';
import { fetchDoctors } from '../api/doctorApi';
import { Container, Typography, CircularProgress, Alert, Grid, Box } from '@mui/material';
import { DoctorCard } from '../components/DoctorCard';
import { FilterPanel } from '../components/FilterPanel';

const DoctorsPage = () => {
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const [searchTerm, setSearchTerm] = useState('');
  const [specFilter, setSpecFilter] = useState('');
  const [officeFilter, setOfficeFilter] = useState('');
  const [debouncedSearch] = useDebounce(searchTerm, 500);

  const [availableSpecs, setAvailableSpecs] = useState<{id: string, name: string}[]>([]);
  const [availableOffices, setAvailableOffices] = useState<{id: string, name: string}[]>([]);

  useEffect(() => {
    fetchDoctors().then(data => {
      const specs = data.map(d => ({ id: d.specializationId, name: d.specializationName }));
      setAvailableSpecs(Array.from(new Map(specs.map(s => [s.id, s])).values()));
      const offs = data.map(d => ({ id: d.officeId, name: d.officeAddressName }));
      setAvailableOffices(Array.from(new Map(offs.map(o => [o.id, o])).values()));
    }).catch(() => setError("Ошибка инициализации фильтров"));
  }, []);

  useEffect(() => {
    if (specFilter && officeFilter) {
      setLoading(true);
      fetchDoctors({
        searchByName: debouncedSearch,
        specializationId: specFilter,
        officeId: officeFilter
      })
      .then(data => {
        setDoctors(data);
        setError(null);
        setLoading(false);
      })
      .catch(() => {
        setError("Ошибка при загрузке врачей");
        setLoading(false);
      });
    } else {
      setDoctors([]);
    }
  }, [debouncedSearch, specFilter, officeFilter]);

  return (
    <Container maxWidth="lg">
      <Typography variant="h3" align="center" sx={{ fontWeight: 800, color: '#1a237e', mb: 5 }}>
        Наши Специалисты
      </Typography>

      <FilterPanel 
        searchTerm={searchTerm} 
        onSearchChange={setSearchTerm}
        specFilter={specFilter} 
        onSpecChange={(id) => { setSpecFilter(id); setOfficeFilter(''); }}
        officeFilter={officeFilter} 
        onOfficeChange={setOfficeFilter}
        specializations={availableSpecs}
        offices={availableOffices}
      />

      {loading ? (
        <Box sx={{ textAlign: 'center', py: 8 }}><CircularProgress /></Box>
      ) : error ? (
        <Alert severity="error" sx={{ mb: 4 }}>{error}</Alert>
      ) : (
        <Grid container spacing={3}>
          {doctors.map((doc) => (
            <Grid key={doc.id} size={{ xs: 12, sm: 6, md: 4 }}>
              <DoctorCard doctor={doc} />
            </Grid>
          ))}
          {!specFilter && (
            <Typography sx={{ width: '100%', mt: 4 }} align="center" color="text.secondary">
              Выберите специализацию и офис для поиска врача
            </Typography>
          )}
        </Grid>
      )}
    </Container>
  );
};

export default DoctorsPage;