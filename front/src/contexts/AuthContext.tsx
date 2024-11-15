import React, { createContext, useContext, useState, ReactNode, useEffect } from 'react';
import { User } from '../models/User';
import { useNavigate } from 'react-router-dom';

interface AuthContextType {
  currentUser: User | undefined;
  login: (userData: User | undefined) => void;
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
  const [currentUser, setUser] = useState<User | undefined>(undefined);
  const navigate = useNavigate();

  const login = (userData: User | undefined) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  const logout = () => {
    setUser(undefined);
    localStorage.removeItem('user');
    navigate("/");
  };

  const logIt = (message: string) => {
    console.log(message);
    }

  const hasRole = (roleName: string) => {
    return currentUser?.userRoles.some(role => role === roleName) ?? false;
  };

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) setUser(JSON.parse(storedUser));
  }, []);

  return (
    <AuthContext.Provider value={{ currentUser: currentUser, login, logout, hasRole , logIt}}>
      {children}
    </AuthContext.Provider>
  );
};
