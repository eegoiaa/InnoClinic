import { Dialog, DialogTitle, DialogContent, IconButton, Typography, Box } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { useState, useEffect } from 'react';
import { SignUpForm } from './SignUpForm';

interface AuthModalProps {
  open: boolean;
  onClose: () => void;
}

export const AuthModal = ({ open, onClose }: AuthModalProps) => {
  const [view, setView] = useState<'signin' | 'signup'>('signup');
  const [isSuccess, setIsSuccess] = useState(false);

  useEffect(() => {
    if (!open) {
      const timer = setTimeout(() => {
        setView('signup');
        setIsSuccess(false);
      }, 300); 
      return () => clearTimeout(timer);
    }
  }, [open]);

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle sx={{ fontWeight: 800, textAlign: 'center', pt: 3 }}>
        {!isSuccess && (view === 'signup' ? 'Create Account' : 'Sign In')}
        <IconButton onClick={onClose} sx={{ position: 'absolute', right: 8, top: 8 }}>
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      
      <DialogContent sx={{ pb: 4, px: 3 }}>
        {isSuccess ? (
          <Box sx={{ textAlign: 'center', py: 4 }}>
            <Typography variant="h5" color="primary" sx={{ fontWeight: 800, mb: 2 }}>
              Email Sent! 📧
            </Typography>
            <Typography variant="body1" color="text.secondary">
              Please check your inbox and click the link to confirm your registration.
            </Typography>
          </Box>
        ) : view === 'signup' ? (
          <SignUpForm 
            onSwitchToSignIn={() => setView('signin')} 
            onSuccess={() => setIsSuccess(true)} 
          />
        ) : (
          <Box sx={{ textAlign: 'center', py: 2 }}>
            <Typography sx={{ mb: 2 }}>Sign In logic is coming soon...</Typography>
            <Typography variant="body2">
              Don't have an account?{' '}
              <Typography 
                component="span" 
                color="primary" 
                onClick={() => setView('signup')}
                sx={{ cursor: 'pointer', fontWeight: 700 }}
              >
                Sign Up
              </Typography>
            </Typography>
          </Box>
        )}
      </DialogContent>
    </Dialog>
  );
};