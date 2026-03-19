import { useState } from 'react';
import { Stack, TextField, Button, IconButton, InputAdornment, Typography, Link, Box, CircularProgress } from '@mui/material';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import EmailIcon from '@mui/icons-material/Email';
import LockIcon from '@mui/icons-material/Lock';
import { signIn, checkEmailExists } from '../../api/authApi';
import { useAuth } from '../../context/AuthContext';

interface SignInFormProps {
  onSwitchToSignUp: () => void;
  onSuccess: (email: string) => void; 
}

export const SignInForm = ({ onSwitchToSignUp, onSuccess }: SignInFormProps) => {
  const { login } = useAuth(); 
  
  const [values, setValues] = useState({ email: '', password: '' });
  const [touched, setTouched] = useState<Record<string, boolean>>({});
  const [emailExistsError, setEmailExistsError] = useState('');
  const [showPwd, setShowPwd] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isCheckingEmail, setIsCheckingEmail] = useState(false); 
  const [serverError, setServerError] = useState('');

  const validate = () => {
    const errs: Record<string, string> = {};
    
    if (!values.email) errs.email = "Please, enter the email";
    else if (!/\S+@\S+\.\S+/.test(values.email)) errs.email = "You've entered an invalid email";
    else if (emailExistsError) errs.email = emailExistsError;

    if (!values.password) errs.password = "Please, enter the password";
    else if (values.password.length < 6 || values.password.length > 15) {
      errs.password = "Password must be 6-15 symbols";
    }

    return errs;
  };

  const errors = validate();

  const isFormValid = 
    Object.keys(errors).length === 0 && 
    values.email && 
    values.password && 
    !isCheckingEmail;

  const handleEmailBlur = async () => {
    setTouched({ ...touched, email: true });
    
    if (values.email && /\S+@\S+\.\S+/.test(values.email)) {
      setIsCheckingEmail(true); 
      try {
        const exists = await checkEmailExists(values.email);
        if (!exists) {
          setEmailExistsError("User with this email doesn’t exist"); 
        } else {
          setEmailExistsError('');
        }
      } catch {
        setEmailExistsError('');
      } finally {
        setIsCheckingEmail(false); 
      }
    }
  };

  const handleSubmit = async () => {
    setServerError('');
    setIsLoading(true);
    try {
      await signIn(values); 
      
      login(values.email); 
      
      onSuccess(values.email); 
    } catch (err: any) {
      const message = err.message || "";
      
      if (message.includes('POST') || message.includes('fetch') || message.includes('500')) {
        setServerError("The server is temporarily unavailable. Please try again later.");
      } else {
        setServerError(message || "Either an email or a password is incorrect");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Stack spacing={3} sx={{ py: 1 }}>
      <TextField
        label="E-mail"
        fullWidth
        InputLabelProps={{ shrink: true }}
        error={touched.email && !!errors.email}
        helperText={touched.email && errors.email}
        value={values.email}
        placeholder="example@mail.com"
        disabled={isLoading}
        onChange={(e) => {
          setValues({ ...values, email: e.target.value });
          setEmailExistsError('');
        }}
        onBlur={handleEmailBlur}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start" sx={{ mr: 1 }}>
              <EmailIcon color="action" />
            </InputAdornment>
          ),
          endAdornment: isCheckingEmail && (
            <InputAdornment position="end">
              <CircularProgress size={20} thickness={5} />
            </InputAdornment>
          )
        }}
      />

      <TextField
        label="Password"
        type={showPwd ? "text" : "password"}
        fullWidth
        InputLabelProps={{ shrink: true }}
        error={touched.password && !!errors.password}
        helperText={touched.password && errors.password}
        value={values.password}
        placeholder="Enter your password"
        disabled={isLoading}
        onChange={(e) => setValues({ ...values, password: e.target.value })}
        onBlur={() => setTouched({ ...touched, password: true })}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start" sx={{ mr: 1 }}>
              <LockIcon color="action" />
            </InputAdornment>
          ),
          endAdornment: (
            <InputAdornment position="end">
              <IconButton onClick={() => setShowPwd(!showPwd)} edge="end">
                {showPwd ? <VisibilityOff /> : <Visibility />}
              </IconButton>
            </InputAdornment>
          )
        }}
      />

      {serverError && (
        <Typography 
          variant="caption" 
          color="error" 
          sx={{ 
            textAlign: 'center', 
            fontWeight: 700, 
            mt: -1,
            bgcolor: '#fff5f5',
            p: 1,
            borderRadius: 1,
            border: '1px solid #ffcdd2'
          }}
        >
          {serverError}
        </Typography>
      )}

      <Button
        variant="contained"
        fullWidth
        size="large"
        disabled={!isFormValid || isLoading}
        onClick={handleSubmit}
        sx={{ 
          borderRadius: 3, py: 2, fontWeight: 800, bgcolor: '#1a237e',
          '&:hover': { bgcolor: '#0d145a' }, minHeight: '56px' 
        }}
      >
        {isLoading ? <CircularProgress size={24} sx={{ color: 'white' }} /> : "SIGN IN"}
      </Button>

      <Box sx={{ textAlign: 'center' }}>
        <Typography variant="body2" color="text.secondary">
          Don't have an account?{' '}
          <Link 
            component="button" 
            variant="body2" 
            onClick={onSwitchToSignUp}
            sx={{ fontWeight: 700, textDecoration: 'none', color: '#1a237e' }}
          >
            Sign up
          </Link>
        </Typography>
      </Box>
    </Stack>
  );
};