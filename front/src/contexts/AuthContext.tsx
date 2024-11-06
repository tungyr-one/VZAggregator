import React, { createContext, useContext, useState, ReactNode, useEffect } from 'react';
import { User } from '../models/User';

interface AuthContextType {
  user: User | null;
  login: (userData: User) => void;
  logout: () => void;
  hasRole: (roleName: string) => boolean;
  logIt:(message:string) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within an AuthProvider");
  return context;
};

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);

  const login = (userData: User) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem('user');
  };

  const logIt = (message: string) => {
    console.log(message);
    }

  const hasRole = (roleName: string) => {
    return user?.userRoles.some(role => role === roleName) ?? false;
  };

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) setUser(JSON.parse(storedUser));
  }, []);

  return (
    <AuthContext.Provider value={{ user, login, logout, hasRole , logIt}}>
      {children}
    </AuthContext.Provider>
  );
};
