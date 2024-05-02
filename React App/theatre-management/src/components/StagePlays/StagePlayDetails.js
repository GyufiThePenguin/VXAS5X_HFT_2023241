import React, { useEffect, useState } from 'react';
import { fetchStagePlay, deleteStagePlay } from '../../services/stagePlaysService';
import { useParams, useNavigate } from 'react-router-dom';

function StagePlayDetails() {
  const [stagePlay, setStagePlay] = useState({});
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    fetchStagePlay(id)
      .then(response => setStagePlay(response.data))
      .catch(error => console.error('Error fetching stagePlay details:', error));
  }, [id]);

  const handleDelete = () => {
    deleteStagePlay(id)
      .then(() => navigate('/stagePlays'))
      .catch(error => console.error('Error deleting stagePlay:', error));
  };

  return (
    <div>
      <h1>StagePlay Details</h1>
      <p>Name: {stagePlay.name}</p>
      <p>Age: {stagePlay.age}</p>
      {/* Add more details as needed */}
      <button onClick={() => navigate(`/stagePlays/edit/${id}`)}>Edit</button>
      <button onClick={handleDelete}>Delete</button>
    </div>
  );
}

export default StagePlayDetails;
