import { Paper, Grid, TextField, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import FilterListIcon from '@mui/icons-material/FilterList';

interface FilterItem { id: string; name: string; }

interface FilterPanelProps {
  searchTerm: string;
  onSearchChange: (val: string) => void;
  specFilter: string;
  onSpecChange: (val: string) => void;
  officeFilter: string;
  onOfficeChange: (val: string) => void;
  specializations: FilterItem[];
  offices: FilterItem[];
}

export const FilterPanel = ({
  searchTerm, onSearchChange, specFilter, onSpecChange,
  officeFilter, onOfficeChange, specializations, offices
}: FilterPanelProps) => {
  return (
    <Paper elevation={0} sx={{ p: 3, mb: 5, borderRadius: 4, border: '1px solid #e2e8f0' }}>
      <Grid container spacing={3} alignItems="center">
        {/* Поиск по ФИО */}
        <Grid size={{ xs: 12, md: 4 }}>
          <TextField
            fullWidth
            label="Поиск по имени"
            value={searchTerm}
            onChange={(e) => onSearchChange(e.target.value)}
            InputProps={{ startAdornment: <SearchIcon sx={{ color: 'action.active', mr: 1 }} /> }}
          />
        </Grid>

        {/* Шаг 1: Обязательный выбор специализации */}
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <FormControl fullWidth required>
            <InputLabel>Специализация</InputLabel>
            <Select
              value={specFilter}
              label="Специализация"
              onChange={(e) => onSpecChange(e.target.value as string)}
              startAdornment={<FilterListIcon sx={{ color: 'action.active', mr: 1, ml: -0.5, fontSize: 20 }} />}
            >
              <MenuItem value=""><em>Выберите специализацию</em></MenuItem>
              {specializations.map(s => <MenuItem key={s.id} value={s.id}>{s.name}</MenuItem>)}
            </Select>
          </FormControl>
        </Grid>

        {/* Шаг 2: Выбор офиса (заблокирован, пока нет специализации) */}
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <FormControl fullWidth required disabled={!specFilter}>
            <InputLabel>Офис</InputLabel>
            <Select
              value={officeFilter}
              label="Офис"
              onChange={(e) => onOfficeChange(e.target.value as string)}
            >
              <MenuItem value=""><em>Выберите офис</em></MenuItem>
              {offices.map(o => <MenuItem key={o.id} value={o.id}>{o.name}</MenuItem>)}
            </Select>
          </FormControl>
        </Grid>
      </Grid>
    </Paper>
  );
};