import React, { createContext, useState, useContext, ReactNode, useEffect } from 'react';
import { User } from '../models/User';

interface UserContextType {
  currentUser: User  | null;
  setCurrentUser: (user: User | null) => void;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export const useCurrentUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error('useUser must be used within a UserProvider');
  }
  return context;
};

export const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [currentUser, setCurrentUser] = useState< User | null>(null);

  return (
    <UserContext.Provider value={{ currentUser: currentUser , setCurrentUser: setCurrentUser }}>
      {children}
    </UserContext.Provider>
  );
};
