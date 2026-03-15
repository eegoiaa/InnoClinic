import type { SignUpRequest, SignInRequest } from '../types/auth';

export const signUp = async (data: SignUpRequest): Promise<void> => {
  const url = '/auth/api/Auth/sign-up'; 
  
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const errorData = await response.json().catch(() => ({}));
    throw new Error(errorData.Message || 'Ошибка при регистрации');
  }
};

export const signIn = async (data: SignInRequest): Promise<void> => {
  const url = '/auth/api/Auth/login'; 
  
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
    credentials: 'include', 
  });

  if (!response.ok) {
    const errorData = await response.json().catch(() => ({}));
    throw new Error(errorData.Message || 'Either an email or a password is incorrect');
  }
};

export const checkEmailExists = async (email: string): Promise<boolean> => {
  const url = `/auth/api/Auth/check-email?email=${encodeURIComponent(email)}`;
  
  const response = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    }
  });

  if (response.status === 404) return false;
  return response.ok;
};

export const confirmEmail = async (userId: string, token: string): Promise<void> => {
  const url = '/auth/api/Auth/confirm-email';
  
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ userId, token }),
  });

  if (!response.ok) {
    throw new Error('Ошибка подтверждения почты');
  }
};