import { useState } from 'react';
import { 
  Modal, Box, Typography, IconButton, Button, 
  Dialog, DialogTitle, DialogContent, DialogActions,
  Snackbar, Alert 
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import { AppointmentForm } from './AppointmentForm';

interface AppointmentModalProps {
  open: boolean;
  onClose: () => void;
}

export const AppointmentModal = ({ open, onClose }: AppointmentModalProps) => {
  const [showExitConfirm, setShowExitConfirm] = useState(false);
  const [isDirty, setIsDirty] = useState(false);
  
  // Состояние для уведомления об успехе (AC-5)
  const [snackbar, setSnackbar] = useState({ open: false, message: '' });

  const handleCloseClick = () => {
    if (isDirty) setShowExitConfirm(true);
    else onClose();
  };

  const handleSuccess = () => {
    setIsDirty(false);
    setSnackbar({ open: true, message: 'Запись успешно создана! Мы ждем вас в клинике.' });
    
    // Закрываем модалку после небольшой паузы, чтобы юзер увидел успех
    setTimeout(() => {
      onClose();
    }, 2000);
  };

  return (
    <>
      <Modal open={open} onClose={handleCloseClick}>
        <Box sx={{
          position: 'absolute', top: '50%', left: '50%', transform: 'translate(-50%, -50%)',
          width: { xs: '95%', sm: 550 }, bgcolor: 'background.paper', borderRadius: 4,
          boxShadow: '0 24px 48px rgba(0,0,0,0.2)', p: 4, outline: 'none'
        }}>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
            <Typography variant="h5" sx={{ fontWeight: 800, color: '#1a237e' }}>Запись на прием</Typography>
            <IconButton onClick={handleCloseClick}><CloseIcon /></IconButton>
          </Box>
          
          <AppointmentForm 
            onDataChange={() => setIsDirty(true)} 
            onSuccess={handleSuccess} 
          />
        </Box>
      </Modal>

      {/* Уведомление (Snackbar) */}
      <Snackbar 
        open={snackbar.open} 
        autoHideDuration={5000} 
        onClose={() => setSnackbar({ ...snackbar, open: false })}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert 
          onClose={() => setSnackbar({ ...snackbar, open: false })} 
          severity="success" 
          // ИСПРАВЛЕНО: 'filled' вместо 'contained'
          variant="filled" 
          icon={<CheckCircleOutlineIcon fontSize="inherit" />}
          sx={{ 
            width: '100%', 
            borderRadius: 3, 
            fontWeight: 600,
            bgcolor: '#2e7d32', 
            boxShadow: '0 8px 16px rgba(46, 125, 50, 0.25)'
          }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>

      {/* Подтверждение выхода (AC-7) */}
      <Dialog open={showExitConfirm} onClose={() => setShowExitConfirm(false)} PaperProps={{ sx: { borderRadius: 3 } }}>
        <DialogTitle sx={{ fontWeight: 700 }}>Выйти без сохранения?</DialogTitle>
        <DialogContent>Ваша запись не будет сохранена в системе.</DialogContent>
        <DialogActions sx={{ p: 2.5 }}>
          <Button onClick={() => setShowExitConfirm(false)} sx={{ fontWeight: 600 }}>Отмена</Button>
          <Button 
            onClick={() => { setShowExitConfirm(false); onClose(); setIsDirty(false); }} 
            color="error" 
            variant="contained" 
            sx={{ borderRadius: 2, fontWeight: 700 }}
          >
            Да, выйти
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};