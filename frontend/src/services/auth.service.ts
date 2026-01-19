import { api } from './api';

export const authService = {
  login: (payload: unknown) => api.post('/auth/login', payload),
  register: (payload: unknown) => api.post('/auth/register', payload),
};

export default authService;
