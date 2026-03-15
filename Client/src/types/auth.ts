export interface SignUpRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface SignInRequest {
  email: string;
  password: string;
}

export interface ConfirmEmailRequest {
  userId: string;
  token: string;
}

export interface AuthError {
  message: string;
  errors?: Record<string, string[]>;
}