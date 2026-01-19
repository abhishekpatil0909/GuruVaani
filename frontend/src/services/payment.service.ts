import { api } from './api';

export const paymentService = {
  initiate: (payload: unknown) => api.post('/payments/initiate', payload),
};

export default paymentService;
