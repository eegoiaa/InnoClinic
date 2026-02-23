import { useState } from 'react';
import { Box, AppBar, Toolbar, Button, Container, Typography } from '@mui/material';

import DoctorsPage from './pages/DoctorsPage'; 
import { ServicesPage } from './pages/ServicesPage';

function App() {
  const [currentPage, setCurrentPage] = useState<'doctors' | 'services'>('doctors');

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
                sx={{ borderBottom: currentPage === 'doctors' ? '2px solid white' : 'none', borderRadius: 0 }}
              >
                Врачи
              </Button>
              <Button 
                color="inherit" 
                onClick={() => setCurrentPage('services')}
                sx={{ borderBottom: currentPage === 'services' ? '2px solid white' : 'none', borderRadius: 0 }}
              >
                Услуги
              </Button>
            </Box>
          </Toolbar>
        </Container>
      </AppBar>

      <Box sx={{ py: 4 }}>
        {currentPage === 'doctors' ? <DoctorsPage /> : <ServicesPage />}
      </Box>
    </Box>
  );
}

export default App;