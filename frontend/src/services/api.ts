import axios from 'axios';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL ?? '/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor placeholder to attach JWT token
api.interceptors.request.use(
  (config) => {
    try {
      const token = localStorage.getItem('auth_token');
      if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    } catch (e) {
      // ignore - localStorage may be unavailable in some environments
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default api;
