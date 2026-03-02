import type { ServicesResponse, ServiceCategoryType } from '../types/service';

export const fetchServices = async (category: ServiceCategoryType): Promise<ServicesResponse> => {
  const response = await fetch(`/services/api/services?category=${category}`);
  
  if (!response.ok) {
    throw new Error('Не удалось загрузить услуги');
  }
  
  return response.json();
};