export interface Inmate {
  id: string;
  externalId: string;
  firstName: string;
  lastName: string;
  status: string;
  bookingDate: string;
  jail?: string;
  cell?: string;
  unit?: string;
}
