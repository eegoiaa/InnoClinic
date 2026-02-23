import { useEffect, useState } from 'react';
import { 
  Container, Typography, Tabs, Tab, Box, CircularProgress, 
  Alert, Accordion, AccordionSummary, AccordionDetails, 
  List, ListItem, Paper, Chip, Stack 
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import PaymentsIcon from '@mui/icons-material/Payments';
import MedicalInformationIcon from '@mui/icons-material/MedicalInformation';

import { fetchServices } from '../api/serviceApi';
import { ServiceCategoryType } from '../types/service';
import type { ServicesResponse } from '../types/service';

export const ServicesPage = () => {
  const [activeTab, setActiveTab] = useState<number>(0);
  const [data, setData] = useState<ServicesResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const categories = [
    ServiceCategoryType.Consultations,
    ServiceCategoryType.Diagnostics,
    ServiceCategoryType.Analyses
  ];

  useEffect(() => {
    setLoading(true);
    fetchServices(categories[activeTab])
      .then(res => {
        setData(res);
        setError(null);
        setLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setLoading(false);
      });
  }, [activeTab]);

  return (
    <Container maxWidth="md">
      <Box sx={{ mb: 6, textAlign: 'center' }}>
        <Typography variant="h3" sx={{ fontWeight: 800, color: '#1a237e', mb: 2 }}>
          Прайс-лист услуг
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Выберите категорию, чтобы увидеть список доступных медицинских услуг
        </Typography>
      </Box>

      <Paper elevation={0} sx={{ borderRadius: 4, bgcolor: '#fff', border: '1px solid #e2e8f0', p: 1, mb: 4 }}>
        <Tabs 
          value={activeTab} 
          onChange={(_, newValue) => setActiveTab(newValue)} 
          centered
          sx={{ '& .MuiTab-root': { fontWeight: 600, fontSize: '1rem' } }}
        >
          <Tab label="Консультации" />
          <Tab label="Диагностика" />
          <Tab label="Анализы" />
        </Tabs>
      </Paper>

      {loading && (
        <Box sx={{ textAlign: 'center', py: 10 }}>
          <CircularProgress size={60} thickness={4} />
        </Box>
      )}

      {error && <Alert severity="error" sx={{ borderRadius: 3 }}>{error}</Alert>}

      {!loading && !error && data && (
        <Box sx={{ animation: 'fadeIn 0.5s ease-in' }}>
          {/* Консультации с группировкой */}
          {activeTab === 0 && data.groupedServices?.map((group) => (
            <Accordion 
              key={group.specialization} 
              elevation={0}
              sx={{ 
                mb: 2, 
                border: '1px solid #e2e8f0', 
                borderRadius: '16px !important',
                '&:before': { display: 'none' }
              }}
            >
              <AccordionSummary expandIcon={<ExpandMoreIcon color="primary" />}>
                <Stack direction="row" spacing={2} alignItems="center">
                  <MedicalInformationIcon color="primary" />
                  <Typography sx={{ fontWeight: 700, fontSize: '1.1rem' }}>
                    {group.specialization}
                  </Typography>
                </Stack>
              </AccordionSummary>
              <AccordionDetails sx={{ pt: 0 }}>
                <List disablePadding>
                  {group.services.map(s => (
                    <ListItem 
                      key={s.id} 
                      sx={{ 
                        py: 2, 
                        borderTop: '1px solid #f1f5f9',
                        display: 'flex', 
                        justifyContent: 'space-between' 
                      }}
                    >
                      <Typography sx={{ fontWeight: 500 }}>{s.serviceName}</Typography>
                      <Chip 
                        icon={<PaymentsIcon />} 
                        label={`${s.price} ₽`} 
                        color="primary" 
                        variant="outlined" 
                        sx={{ fontWeight: 700, borderRadius: 2 }}
                      />
                    </ListItem>
                  ))}
                </List>
              </AccordionDetails>
            </Accordion>
          ))}

          {/* Диагностика и Анализы (Плоский список) */}
          {activeTab !== 0 && (
            <Paper elevation={0} sx={{ border: '1px solid #e2e8f0', borderRadius: 4, overflow: 'hidden' }}>
              <List disablePadding>
                {(data.flatServices ?? []).map((s, index) => (
                  <ListItem 
                    key={s.id} 
                    sx={{ 
                      py: 3, 
                      px: 4,
                      bgcolor: index % 2 === 0 ? '#fff' : '#f8fafc',
                      display: 'flex', 
                      justifyContent: 'space-between',
                      '&:not(:last-child)': { borderBottom: '1px solid #e2e8f0' }
                    }}
                  >
                    <Typography sx={{ fontWeight: 600, fontSize: '1.05rem' }}>{s.serviceName}</Typography>
                    <Typography variant="h6" color="primary" sx={{ fontWeight: 800 }}>
                      {s.price} ₽
                    </Typography>
                  </ListItem>
                ))}
              </List>
            </Paper>
          )}
        </Box>
      )}
    </Container>
  );
};