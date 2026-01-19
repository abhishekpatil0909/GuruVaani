import { api } from './api';

export const bookingService = {
  create: (payload: unknown) => api.post('/bookings', payload),
  listForCustomer: () => api.get('/bookings/customer'),
};

export default bookingService;
