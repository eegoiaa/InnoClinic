import { useEffect, useState, useRef } from 'react'; // Добавили useRef
import { useSearchParams, useNavigate } from 'react-router-dom';
import { Box, Typography, CircularProgress, Button, Paper, Container, Stack } from '@mui/material';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
import { confirmEmail } from '../api/authApi';

export const ConfirmEmailPage = () => {
  const [searchParams] = useSearchParams();
  const [status, setStatus] = useState<'loading' | 'success' | 'error'>('loading');
  const navigate = useNavigate();
  
  const hasCalledApi = useRef(false);

  useEffect(() => {
    if (hasCalledApi.current) return;

    const userId = searchParams.get('userId');
    const token = searchParams.get('token');

    if (userId && token) {
      hasCalledApi.current = true; 

      const startTime = Date.now();

      confirmEmail(userId, token)
        .then(() => {
          const duration = Date.now() - startTime;
          const delay = Math.max(0, 1500 - duration); 

          setTimeout(() => setStatus('success'), delay);
        })
        .catch(() => {
          setStatus('error');
        });
    } else {
      setStatus('error');
    }
  }, [searchParams]);

  return (
    <Container maxWidth="sm" sx={{ mt: 10 }}>
      <Paper elevation={3} sx={{ p: 6, textAlign: 'center', borderRadius: 4 }}>
        {status === 'loading' && (
          <Stack alignItems="center" spacing={3}>
            <CircularProgress size={60} thickness={4} sx={{ color: '#1a237e' }} />
            <Typography variant="h6" sx={{ color: 'text.secondary' }}>
              Проверяем данные...
            </Typography>
          </Stack>
        )}

        {status === 'success' && (
          <Box>
            <CheckCircleOutlineIcon color="success" sx={{ fontSize: 100, mb: 2 }} />
            <Typography variant="h4" gutterBottom sx={{ fontWeight: 800, color: '#2e7d32' }}>
              Успех!
            </Typography>
            <Typography variant="body1" color="text.secondary" sx={{ mb: 4, px: 2 }}>
              Ваша электронная почта подтверждена. Теперь все функции клиники доступны вам в полном объеме.
            </Typography>
            <Button 
              variant="contained" 
              fullWidth 
              onClick={() => navigate('/')} 
              sx={{ 
                bgcolor: '#1a237e', 
                py: 1.8, 
                fontWeight: 700, 
                borderRadius: 3,
                boxShadow: '0 4px 12px rgba(26, 35, 126, 0.3)'
              }}
            >
              ПЕРЕЙТИ К ВРАЧАМ
            </Button>
          </Box>
        )}

        {status === 'error' && (
          <Box>
            <ErrorOutlineIcon color="error" sx={{ fontSize: 100, mb: 2 }} />
            <Typography variant="h4" gutterBottom sx={{ fontWeight: 800, color: '#d32f2f' }}>
              Упс...
            </Typography>
            <Typography variant="body1" color="text.secondary" sx={{ mb: 4 }}>
              Похоже, эта ссылка уже была использована или её срок действия истек.
            </Typography>
            <Button 
              variant="outlined" 
              fullWidth 
              onClick={() => navigate('/')}
              sx={{ borderRadius: 3, py: 1.5 }}
            >
              Вернуться на главную
            </Button>
          </Box>
        )}
      </Paper>
    </Container>
  );
};