import React, { useEffect, useState } from 'react';
import { fetchActors } from '../../services/actorsService';

function ActorsList({ onSelectActor }) {
  const [actors, setActors] = useState([]);

  useEffect(() => {
    fetchActors()
      .then(response => {
        setActors(response.data);
      })
      .catch(error => console.error('Error fetching actors:', error));
  }, []);

  return (
    <div>
      {actors.map(actor => (
        <div key={actor.id} onClick={() => onSelectActor(actor)}>
          {actor.name}
        </div>
      ))}
    </div>
  );
}

export default ActorsList;
