import type { SignUpRequest } from '../types/auth';

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