import React, { createContext, useEffect, useState } from 'react';
import type { User, Role } from '../types';

type AuthState = {
  user: User | null;
  // mock login accepts a role to set on the user
  login: (role: Role, id?: string) => void;
  logout: () => void;
};

export const AuthContext = createContext<AuthState | null>(null);

const STORAGE_KEY = 'guruVani_user';

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(() => {
    try {
      const raw = localStorage.getItem(STORAGE_KEY);
      return raw ? (JSON.parse(raw) as User) : null;
    } catch {
      return null;
    }
  });

  useEffect(() => {
    try {
      if (user) localStorage.setItem(STORAGE_KEY, JSON.stringify(user));
      else localStorage.removeItem(STORAGE_KEY);
    } catch {
      // ignore storage errors
    }
  }, [user]);

  const login = (role: Role, id = 'mock-id') => {
    // Mock login: set user with provided role
    const u: User = { id, role };
    setUser(u);
  };

  const logout = () => {
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
