import api from './api';

export const fetchStagePlays = () => {
  return api.get('/stagePlay');
};

export const createStagePlay = (stagePlayData) => {
  return api.post('/stagePlay', stagePlayData);
};

export const updateStagePlay = (stagePlayData) => {
  return api.put(`/stagePlay/${stagePlayData.id}`, stagePlayData);
};

export const deleteStagePlay = (stagePlayId) => {
  return api.delete(`/stagePlay/${stagePlayId}`);
};
