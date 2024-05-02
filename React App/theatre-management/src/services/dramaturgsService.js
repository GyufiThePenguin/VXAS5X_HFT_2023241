import api from './api';

export const fetchDramaturgs = () => {
  return api.get('/dramaturg');
};

export const createDramaturg = (dramaturgData) => {
  return api.post('/dramaturg', dramaturgData);
};

export const updateDramaturg = (dramaturgData) => {
  return api.put(`/dramaturg/${dramaturgData.id}`, dramaturgData);
};

export const deleteDramaturg = (dramaturgId) => {
  return api.delete(`/dramaturg/${dramaturgId}`);
};
