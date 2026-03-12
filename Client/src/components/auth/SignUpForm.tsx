import { useState } from 'react';
import { Stack, TextField, Button, IconButton, InputAdornment, Typography, Link, Box, CircularProgress } from '@mui/material';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import EmailIcon from '@mui/icons-material/Email';
import LockIcon from '@mui/icons-material/Lock';
import { signUp } from '../../api/authApi';

export const SignUpForm = ({ onSwitchToSignIn, onSuccess }: { onSwitchToSignIn: () => void, onSuccess: () => void }) => {
  const [values, setValues] = useState({ email: '', password: '', confirmPassword: '' });
  const [touched, setTouched] = useState<Record<string, boolean>>({});
  const [showPwd, setShowPwd] = useState(false);
  const [serverError, setServerError] = useState('');
  
  // Правка: Состояние загрузки
  const [isLoading, setIsLoading] = useState(false);

  const validate = () => {
    const errs: Record<string, string> = {};
    if (!values.email) errs.email = "Please, enter the email";
    else if (!/\S+@\S+\.\S+/.test(values.email)) errs.email = "You've entered an invalid email";
    if (values.password.length < 6) errs.password = "Min 6 symbols required";
    if (values.confirmPassword !== values.password) errs.confirmPassword = "The passwords don't coincide";
    return errs;
  };

  const errors = validate();
  const isFormValid = Object.keys(errors).length === 0 && values.email && values.password;

  const handleSubmit = async () => {
    setServerError('');
    setIsLoading(true);
    
    try {
      await signUp(values);
      onSuccess();
    } catch (err: any) {
      setServerError(err.message);
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
        error={(touched.email && !!errors.email) || !!serverError}
        helperText={(touched.email && errors.email) || serverError}
        value={values.email}
        placeholder="example@mail.com"
        onChange={(e) => setValues({ ...values, email: e.target.value })}
        onBlur={() => setTouched({ ...touched, email: true })}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start" sx={{ mr: 1 }}>
              <EmailIcon color="action" />
            </InputAdornment>
          ),
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

      <TextField
        label="Repeat entered password"
        type="password"
        fullWidth
        InputLabelProps={{ shrink: true }}
        placeholder="Repeat your password"
        error={touched.confirmPassword && !!errors.confirmPassword}
        helperText={touched.confirmPassword && errors.confirmPassword}
        value={values.confirmPassword}
        onChange={(e) => setValues({ ...values, confirmPassword: e.target.value })}
        onBlur={() => setTouched({ ...touched, confirmPassword: true })}
      />

      <Button
        variant="contained"
        fullWidth
        size="large"
        disabled={!isFormValid || isLoading}
        onClick={handleSubmit}
        sx={{ 
          borderRadius: 3, 
          py: 2, 
          fontWeight: 800, 
          bgcolor: '#1a237e',
          fontSize: '1rem',
          boxShadow: '0 4px 12px rgba(26, 35, 126, 0.3)',
          '&:hover': { bgcolor: '#0d145a', boxShadow: '0 6px 16px rgba(26, 35, 126, 0.4)' },
          minHeight: '56px' 
        }}
      >
        {isLoading ? (
          <CircularProgress size={24} sx={{ color: 'white' }} />
        ) : (
          "SIGN UP"
        )}
      </Button>

      <Box sx={{ textAlign: 'center' }}>
        <Typography variant="body2" color="text.secondary">
          Already have an account?{' '}
          <Link 
            component="button" 
            variant="body2" 
            onClick={onSwitchToSignIn}
            sx={{ fontWeight: 700, textDecoration: 'none', color: '#1a237e' }}
          >
            Sign in
          </Link>
        </Typography>
      </Box>
    </Stack>
  );
};