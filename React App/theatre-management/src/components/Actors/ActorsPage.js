import React, { useState } from 'react';
import ActorsList from './ActorsList';
import ActorDetails from './ActorDetails';
import { deleteActor } from '../../services/actorsService';

function ActorsPage() {
  const [selectedActor, setSelectedActor] = useState(null);

  const handleDelete = (actorId) => {
    if (window.confirm('Are you sure you want to delete this actor?')) {
        deleteActor(actorId)
            .then(() => {
                alert('Actor deleted successfully!');
                // Refresh the list or manage state to remove deleted actor
            })
            .catch(error => {
                console.error('Failed to delete actor:', error);
                alert('Failed to delete actor');
            });
    }
};

  return (
    <div style={{ display: 'flex' }}>
      <div style={{ flex: 1 }}>
        <ActorsList onSelectActor={setSelectedActor} />
      </div>
      <div style={{ flex: 1 }}>
        {selectedActor && <ActorDetails selectedActor={selectedActor} />}
        <button onClick={() => setSelectedActor({})}>Add New Actor</button>
        <button onClick={handleDelete}>Delete</button>
      </div>
    </div>
  );
}

export default ActorsPage;
