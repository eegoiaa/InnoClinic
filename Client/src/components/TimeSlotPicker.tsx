import { Grid, Button, Typography, Box } from '@mui/material'; // Обычный Grid
import AccessTimeIcon from '@mui/icons-material/AccessTime';

interface TimeSlotPickerProps {
  selectedSlot: string;
  onSelect: (slot: string) => void;
  disabled: boolean;
}

const MOCK_SLOTS = ["08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30"];

export const TimeSlotPicker = ({ selectedSlot, onSelect, disabled }: TimeSlotPickerProps) => {
  return (
    <Box sx={{ opacity: disabled ? 0.5 : 1, pointerEvents: disabled ? 'none' : 'auto' }}>
      <Typography variant="subtitle2" sx={{ mb: 1.5, fontWeight: 700, display: 'flex', alignItems: 'center' }}>
        <AccessTimeIcon sx={{ fontSize: 18, mr: 1 }} color="primary" />
        Доступное время
      </Typography>
      
      <Grid container spacing={1}>
        {MOCK_SLOTS.map((slot) => (
          <Grid key={slot} size={{ xs: 4, sm: 3 }}> 
            <Button
              fullWidth
              variant={selectedSlot === slot ? "contained" : "outlined"}
              onClick={() => onSelect(slot)}
              sx={{ borderRadius: 2, fontWeight: 600 }}
            >
              {slot}
            </Button>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};