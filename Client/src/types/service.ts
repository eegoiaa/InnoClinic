export interface Service {
  id: string;
  serviceName: string;
  price: number;
}

export interface ServiceGroup {
  specialization: string;
  services: Service[];
}

export interface ServicesResponse {
  groupedServices: ServiceGroup[] | null;
  flatServices: Service[] | null;
}

export const ServiceCategoryType = {
  Consultations: "Consultations",
  Diagnostics: "Diagnostics",
  Analyses: "Analyses"
} as const;

export type ServiceCategoryType = typeof ServiceCategoryType[keyof typeof ServiceCategoryType];