export interface AppointmentFormValues {
  specializationId: string;
  doctorId: string;
  serviceId: string;
  officeId: string;
  date: string;
  timeSlot: string;
}

export interface Specialization {
  id: string;
  name: string;
  isActive: boolean;
}

export interface Office {
  id: string;
  address: string;
  isActive: boolean;
}