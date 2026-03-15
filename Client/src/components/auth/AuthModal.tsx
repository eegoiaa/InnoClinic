import { Dialog, DialogTitle, DialogContent, IconButton, Typography, Box } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { useState, useEffect } from 'react';
import { SignUpForm } from './SignUpForm';
import { SignInForm } from './SignInForm'; 

interface AuthModalProps {
  open: boolean;
  onClose: () => void;
}

export const AuthModal = ({ open, onClose }: AuthModalProps) => {
  const [view, setView] = useState<'signin' | 'signup'>('signin');
  const [successType, setSuccessType] = useState<'signup' | 'signin' | null>(null);

  useEffect(() => {
    if (!open) {
      const timer = setTimeout(() => {
        setView('signin');
        setSuccessType(null);
      }, 300); 
      return () => clearTimeout(timer);
    }
  }, [open]);

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle sx={{ fontWeight: 800, textAlign: 'center', pt: 3 }}>
        {!successType && (view === 'signup' ? 'Create Account' : 'Sign In')}
        <IconButton onClick={onClose} sx={{ position: 'absolute', right: 8, top: 8 }}>
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      
      <DialogContent sx={{ pb: 4, px: 3 }}>
        {successType === 'signup' ? (
          <Box sx={{ textAlign: 'center', py: 4 }}>
            <Typography variant="h5" color="primary" sx={{ fontWeight: 800, mb: 2 }}>
              Email Sent! 📧
            </Typography>
            <Typography variant="body1" color="text.secondary">
              Please check your inbox and click the link to confirm your registration.
            </Typography>
          </Box>
        ) : successType === 'signin' ? (
          <Box sx={{ textAlign: 'center', py: 4 }}>
            <Typography variant="h5" sx={{ color: '#2e7d32', fontWeight: 800, mb: 2 }}>
              Welcome Back! 👋
            </Typography>
            <Typography variant="body1" color="text.secondary">
              You've signed in successfully.
            </Typography>
          </Box>
        ) : view === 'signup' ? (
          <SignUpForm 
            onSwitchToSignIn={() => setView('signin')} 
            onSuccess={() => setSuccessType('signup')} 
          />
        ) : (
          <SignInForm 
            onSwitchToSignUp={() => setView('signup')} 
            onSuccess={() => setSuccessType('signin')} 
          />
        )}
      </DialogContent>
    </Dialog>
  );
};