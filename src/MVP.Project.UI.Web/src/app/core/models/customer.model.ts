export interface CustomerViewModel {
  id?: string;
  name: string;
  email: string;
  documentNumber?: string;
  birthDate: Date;
  phone?: string;
  stateInscription?: string;
  streetAddress?: string;
  buildingNumber?: string;
  secondaryAddress?: string;
  neighborhood?: string;
  zipCode?: string;
  city?: string;
  state?: string;
  active: boolean;
}