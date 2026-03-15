// Client/src/App.tsx
import { useState } from 'react';
import { Routes, Route, useNavigate, useLocation } from 'react-router-dom';
import { Box, AppBar, Toolbar, Button, Container, Typography, Avatar, IconButton, Tooltip } from '@mui/material';
import MedicalServicesIcon from '@mui/icons-material/MedicalServices';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import LogoutIcon from '@mui/icons-material/Logout'; 

import DoctorsPage from './pages/DoctorsPage'; 
import { ServicesPage } from './pages/ServicesPage';
import { ConfirmEmailPage } from './pages/ConfirmEmailPage';
import { AppointmentModal } from './components/AppointmentModal'; 
import { AuthModal } from './components/auth/AuthModal';
import { useAuth } from './context/AuthContext'; 

function App() {
  const navigate = useNavigate();
  const location = useLocation();
  const { user, logout } = useAuth(); 
  const [isAppointmentModalOpen, setIsAppointmentModalOpen] = useState(false); 
  const [isAuthModalOpen, setIsAuthModalOpen] = useState(false);

  return (
    <Box sx={{ flexGrow: 1, bgcolor: '#f8fafc', minHeight: '100vh' }}>
      <AppBar position="sticky" sx={{ bgcolor: '#1a237e' }}>
        <Container maxWidth="lg">
          <Toolbar disableGutters>
            <Typography variant="h6" onClick={() => navigate('/')} sx={{ mr: 4, fontWeight: 800, color: '#fff', cursor: 'pointer' }}>
              InnoClinic
            </Typography>
            
            <Box sx={{ flexGrow: 1, display: 'flex', gap: 2 }}>
              <Button 
                color="inherit" 
                onClick={() => navigate('/')}
                sx={{ 
                  borderBottom: location.pathname === '/' ? '2px solid white' : 'none', 
                  borderRadius: 0 
                }}
              >
                Врачи
              </Button>
              <Button 
                color="inherit" 
                onClick={() => navigate('/services')}
                sx={{ 
                  borderBottom: location.pathname === '/services' ? '2px solid white' : 'none', 
                  borderRadius: 0 
                }}
              >
                Услуги
              </Button>
            </Box>

            {user ? (
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 2, mr: 2 }}>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                  <Avatar sx={{ bgcolor: '#f50057', width: 32, height: 32, fontSize: '1rem' }}>
                    {user.email[0].toUpperCase()}
                  </Avatar>
                  <Typography variant="body2" sx={{ fontWeight: 600, color: '#fff' }}>
                    {user.email}
                  </Typography>
                </Box>
                
                <Tooltip title="Выйти">
                  <IconButton onClick={logout} sx={{ color: '#fff' }}>
                    <LogoutIcon fontSize="small" />
                  </IconButton>
                </Tooltip>
              </Box>
            ) : (
              <Button 
                color="inherit"
                startIcon={<AccountCircleIcon />}
                onClick={() => setIsAuthModalOpen(true)}
                sx={{ 
                  fontWeight: 700, mr: 2, borderRadius: 2, px: 2,
                  '&:hover': { bgcolor: 'rgba(255, 255, 255, 0.2)' }
                }}
              >
                Войти
              </Button>
            )}

            <Button 
              variant="contained" color="secondary"
              startIcon={<MedicalServicesIcon />}
              onClick={() => setIsAppointmentModalOpen(true)}
              sx={{ fontWeight: 700, borderRadius: 2, bgcolor: '#f50057' }}
            >
              Записаться
            </Button>
          </Toolbar>
        </Container>
      </AppBar>

      <Box sx={{ py: 4 }}>
        <Routes>
          <Route path="/" element={<DoctorsPage />} />
          <Route path="/services" element={<ServicesPage />} />
          <Route path="/confirm-email" element={<ConfirmEmailPage />} />
        </Routes>
      </Box>

      <AppointmentModal open={isAppointmentModalOpen} onClose={() => setIsAppointmentModalOpen(false)} />
      <AuthModal open={isAuthModalOpen} onClose={() => setIsAuthModalOpen(false)} />
    </Box>
  );
}

export default App;