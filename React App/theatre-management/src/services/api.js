import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:62255',
  //baseURL: 'http://192.168.1.162:62255',
  headers: {
    'Content-Type': 'application/json'
  }
});

export default api;
