export interface Doctor {
  id: string;
  fullname: string;
  specializationName: string;
  specializationId: string; 
  officeAddressName: string;
  officeId: string;
  experienceYears: number;
  photoId?: string;
}