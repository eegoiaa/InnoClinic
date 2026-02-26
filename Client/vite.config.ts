import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    proxy: {
      '/appointments/api': {
        target: 'https://localhost:8000', 
        changeOrigin: true,
        secure: false,
      },
      '/profiles/api': {
        target: 'https://localhost:8000',
        changeOrigin: true,
        secure: false, 
      },
      '/services/api': {
        target: 'https://localhost:8000',
        changeOrigin: true,
        secure: false,
      }
    }
  }
})