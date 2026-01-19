export type Role = 'admin' | 'guru' | 'customer';

export type User = {
  id: string;
  name?: string;
  role?: Role;
};

export type AuthToken = string;
