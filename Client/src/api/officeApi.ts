export interface Office {
  id: string;
  address: string;
  isActive: boolean;
}

export const fetchOffices = async (): Promise<Office[]> => {
  return [
    { id: 'office-1-uuid', address: 'ул. Пушкина, 10', isActive: true },
    { id: 'office-2-uuid', address: 'пр. Мира, 45', isActive: true }
  ];
};