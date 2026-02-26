import type { Doctor } from '../types/doctor';

export interface DoctorFilters {
  searchByName?: string;
  specializationId?: string;
  officeId?: string;
}

export const fetchDoctors = async (filters: DoctorFilters = {}): Promise<Doctor[]> => {
  const params = new URLSearchParams();
  if (filters.searchByName) params.append('SearchByName', filters.searchByName);
  if (filters.specializationId) params.append('SpecializationId', filters.specializationId);
  if (filters.officeId) params.append('OfficeId', filters.officeId);

  const url = `/profiles/api/doctors?${params.toString()}`;
  const response = await fetch(url);
  
  if (!response.ok) {
    throw new Error('Ошибка при загрузке врачей');
  }
  
  return response.json();
};