export interface CreateAppointmentRequest {
  patientId: string;
  doctorId: string;
  serviceId: string;
  date: string;
  time: string; 
}

export interface AppointmentResponse {
  message: string;
  id?: string;
}

export const createAppointment = async (data: CreateAppointmentRequest): Promise<AppointmentResponse> => {
  const url = '/appointments/api/appointments';
  
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const errorData = await response.json();
    throw errorData; 
  }

  return response.json();
};