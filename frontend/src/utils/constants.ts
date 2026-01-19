export const ROLES = {
  ADMIN: 'admin',
  GURU: 'guru',
  CUSTOMER: 'customer',
} as const;

export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: '/api/auth/login',
    REGISTER: '/api/auth/register',
  },
  GURUJIS: '/api/gurujis',
  BOOKINGS: {
    CREATE: '/api/bookings',
    CUSTOMER: '/api/bookings/customer',
  },
  PAYMENTS: {
    INITIATE: '/api/payments/initiate',
  },
} as const;
