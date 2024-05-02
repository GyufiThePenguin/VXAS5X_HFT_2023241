import api from './api';

export const fetchActors = () => {
  return api.get('/actor');
};

// export const createActor = (actorData) => {
//   return api.post('/actor', actorData);
// };

export const updateActor = (actorData) => {
  return api.put(`/actor/${actorData.id}`, actorData);
};

// export const deleteActor = (actorId) => {
//   return api.delete(`/actor/${actorId}`);
// };

export const fetchActor = (actorId) => {
  return api.get(`/actor/${actorId}`);
};


//

// export const createActor = async (actorData) => {
//   // API call to create a new actor
//   const response = await fetch('your-api-url/actors', {
//       method: 'POST',
//       headers: {
//           'Content-Type': 'application/json'
//       },
//       body: JSON.stringify(actorData)
//   });
//   const data = await response.json();
//   return data;
// };

export const createActor = async (actorData) => {
  console.log('Sending data to create actor:', actorData);
  try {
      const response = await fetch('http://localhost:62255/actors', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(actorData)
      });
      const data = await response.json();
      console.log('Received data:', data);
      return data;
  } catch (error) {
      console.error('Error creating actor:', error);
      throw error;  // Rethrow to handle it in the calling component
  }
};

export const deleteActor = async (actorId) => {
  // API call to delete an actor
  const response = await fetch(`http://localhost:62255/actors/${actorId}`, {
      method: 'DELETE'
  });
  return response.ok;
};


