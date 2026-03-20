import { createContext, useContext, useState, useEffect, useCallback } from 'react';
import type { ReactNode } from 'react'; 
import { signOut } from '../api/authApi';

interface User {
  email: string;
}

interface AuthContextType {
  user: User | null;
  login: (email: string) => void;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);

  const login = (email: string) => setUser({ email });

  const logout = useCallback(async () => {
    try {
      await signOut(); 
    } catch (error) {
      console.error('Logout failed on server:', error);
    } finally {
      setUser(null);
    }
  }, []);

  useEffect(() => {
    const handleSessionExpired = () => {
      setUser(null);
    };

    window.addEventListener('session-expired', handleSessionExpired);
    
    return () => {
      window.removeEventListener('session-expired', handleSessionExpired);
    };
  }, []);

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth must be used within an AuthProvider');
  return context;
};