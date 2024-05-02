import React, { useEffect, useState } from 'react';
import { fetchStagePlays } from '../../services/stagePlaysService';

function StagePlaysList() {
  const [stagePlays, setStagePlays] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchStagePlays()
      .then(response => {
        setStagePlays(response.data);
      })
      .catch(error => {
        console.error('Error fetching stage plays:', error);
        setError("Failed to load stage plays.");
      });
  }, []);

  return (
    <div>
      <h1>Stage Plays List</h1>
      {error ? <p>{error}</p> : (
        <ul>
          {stagePlays.map(play => <li key={play.id}>{play.title}</li>)}
        </ul>
      )}
    </div>
  );
}

export default StagePlaysList;
