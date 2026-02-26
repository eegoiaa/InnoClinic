import { Card, CardContent, Avatar, Box, Typography, Divider, Stack, Paper, IconButton, Tooltip } from '@mui/material';
import MedicalServicesIcon from '@mui/icons-material/MedicalServices';
import WorkHistoryIcon from '@mui/icons-material/WorkHistory';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import MapIcon from '@mui/icons-material/Map'; 
import type { Doctor } from '../types/doctor';

interface DoctorCardProps {
  doctor: Doctor;
}

export const DoctorCard = ({ doctor }: DoctorCardProps) => {
  return (
    <Card 
      component={Paper} 
      elevation={2}
      sx={{ 
        height: '100%', 
        borderRadius: 4, 
        transition: '0.3s',
        '&:hover': { transform: 'translateY(-6px)', boxShadow: '0 10px 20px rgba(0,0,0,0.08)' }
      }}
    >
      <CardContent sx={{ p: 3 }}>
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 2.5 }}>
          <Avatar sx={{ bgcolor: '#3949ab', width: 60, height: 60, mr: 2, fontWeight: 600 }}>
            {doctor.fullname ? doctor.fullname[0] : 'D'}
          </Avatar>
          <Box>
            <Typography variant="h6" sx={{ fontWeight: 700, lineHeight: 1.2 }}>
              {doctor.fullname}
            </Typography>
            <Typography variant="body2" color="primary" sx={{ fontWeight: 600, mt: 0.5, display: 'flex', alignItems: 'center' }}>
              <MedicalServicesIcon sx={{ fontSize: 16, mr: 0.5 }} />
              {doctor.specializationName}
            </Typography>
          </Box>
        </Box>

        <Divider sx={{ my: 2, opacity: 0.5 }} />

        <Stack spacing={1.5}>
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <WorkHistoryIcon sx={{ fontSize: 18, mr: 1, color: 'text.secondary' }} />
            <Typography variant="body2">
              Стаж: <strong>{doctor.experienceYears} лет</strong>
            </Typography>
          </Box>
          
          <Box sx={{ display: 'flex', alignItems: 'flex-start', justifyContent: 'space-between' }}>
            <Box sx={{ display: 'flex', alignItems: 'flex-start' }}>
              <LocationOnIcon sx={{ fontSize: 18, mr: 1, color: 'text.secondary', mt: 0.2 }} />
              <Typography variant="body2" color="text.secondary">
                {doctor.officeAddressName}
              </Typography>
            </Box>
            
            <Tooltip title="Посмотреть на карте">
              <IconButton 
                size="small" 
                color="primary" 
                onClick={() => window.open(`https://yandex.ru/maps/?text=${encodeURIComponent(doctor.officeAddressName)}`, '_blank')}
                sx={{ ml: 1, mt: -0.5 }}
              >
                <MapIcon sx={{ fontSize: 18 }} />
              </IconButton>
            </Tooltip>
          </Box>
        </Stack>
      </CardContent>
    </Card>
  );
};