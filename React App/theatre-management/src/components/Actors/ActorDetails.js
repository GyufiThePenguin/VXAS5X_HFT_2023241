import React, { useState, useEffect } from 'react';
//import { updateActor } from '../../services/actorsService';
import { fetchActor, updateActor, createActor, deleteActor } from '../../services/actorsService';


function ActorDetails({ selectedActor }) {
  const [actorDetails, setActorDetails] = useState(selectedActor || {});

  useEffect(() => {
    if (selectedActor) {
      setActorDetails(selectedActor);
    }
  }, [selectedActor]);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setActorDetails(prevDetails => ({ ...prevDetails, [name]: value }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const action = actorDetails.id ? updateActor : createActor;  // Choose function based on presence of id
    action(actorDetails)
      .then(() => {
        alert(actorDetails.id ? 'Actor updated successfully!' : 'Actor added successfully!');
        // Navigate back or refresh list
      })
      .catch(error => {
        console.error('Failed to submit actor:', error);
        alert('Failed to submit actor');
      });
};

  const handleDelete = () => {
    if (window.confirm('Are you sure you want to delete this actor?')) {
        deleteActor(actorDetails.id)
          .then(() => {
            alert('Actor deleted successfully!');
            // Navigate back to the list or clear the current selection
          })
          .catch(error => {
            console.error('Failed to delete actor:', error);
            alert('Failed to delete actor');
          });
    }
};

  return (
    <form onSubmit={handleSubmit}>
      <label>Name:
        <input type="text" name="name" value={actorDetails.name} onChange={handleChange} />
      </label>
      <label>Age:
        <input type="text" name="age" value={actorDetails.age} onChange={handleChange} />
      </label>
      <label>Gender:
        <input type="text" name="gender" value={actorDetails.gender} onChange={handleChange} />
      </label>
      <button type="submit">Save</button>
    </form>
  );
}

export default ActorDetails;
