import { refreshTokens } from './authApi';

const BASE_URL = ''; 

export const apiFetch = async (url: string, options: RequestInit = {}): Promise<Response> => {
  const defaultOptions: RequestInit = {
    ...options,
    credentials: 'include', 
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
  };

  let response = await fetch(`${BASE_URL}${url}`, defaultOptions);

  if (response.status === 401) {
    console.warn('Access token expired, attempting to refresh...');

    try {
      await refreshTokens();
      response = await fetch(`${BASE_URL}${url}`, defaultOptions);
      
    } catch (refreshError) {
      console.error('Refresh token expired or invalid. Redirecting to login.');
      window.dispatchEvent(new Event('session-expired'));
      throw new Error('Session expired. Please log in again.');
    }
  }

  return response;
};