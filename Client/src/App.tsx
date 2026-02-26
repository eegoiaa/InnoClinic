import { useState } from 'react';
import { Box, AppBar, Toolbar, Button, Container, Typography } from '@mui/material';
import MedicalServicesIcon from '@mui/icons-material/MedicalServices';

import DoctorsPage from './pages/DoctorsPage'; 
import { ServicesPage } from './pages/ServicesPage';
import { AppointmentModal } from './components/AppointmentModal'; 
function App() {
  const [currentPage, setCurrentPage] = useState<'doctors' | 'services'>('doctors');
  const [isAppointmentModalOpen, setIsAppointmentModalOpen] = useState(false); 

  return (
    <Box sx={{ flexGrow: 1, bgcolor: '#f8fafc', minHeight: '100vh' }}>
      <AppBar position="sticky" sx={{ bgcolor: '#1a237e' }}>
        <Container maxWidth="lg">
          <Toolbar disableGutters>
            <Typography variant="h6" sx={{ mr: 4, fontWeight: 800, color: '#fff' }}>
              InnoClinic
            </Typography>
            
            <Box sx={{ flexGrow: 1, display: 'flex', gap: 2 }}>
              <Button 
                color="inherit" 
                onClick={() => setCurrentPage('doctors')}
                sx={{ 
                  borderBottom: currentPage === 'doctors' ? '2px solid white' : 'none', 
                  borderRadius: 0,
                  opacity: currentPage === 'doctors' ? 1 : 0.7
                }}
              >
                Врачи
              </Button>
              <Button 
                color="inherit" 
                onClick={() => setCurrentPage('services')}
                sx={{ 
                  borderBottom: currentPage === 'services' ? '2px solid white' : 'none', 
                  borderRadius: 0,
                  opacity: currentPage === 'services' ? 1 : 0.7
                }}
              >
                Услуги
              </Button>
            </Box>

            <Button 
              variant="contained" 
              color="secondary"
              startIcon={<MedicalServicesIcon />}
              onClick={() => setIsAppointmentModalOpen(true)}
              sx={{ 
                ml: 2, 
                fontWeight: 700, 
                borderRadius: 2,
                bgcolor: '#f50057',
                '&:hover': { bgcolor: '#c51162' }
              }}
            >
              Записаться на прием
            </Button>
          </Toolbar>
        </Container>
      </AppBar>

      <Box sx={{ py: 4 }}>
        {currentPage === 'doctors' ? <DoctorsPage /> : <ServicesPage />}
      </Box>

      <AppointmentModal 
        open={isAppointmentModalOpen} 
        onClose={() => setIsAppointmentModalOpen(false)} 
      />
    </Box>
  );
}

export default App;