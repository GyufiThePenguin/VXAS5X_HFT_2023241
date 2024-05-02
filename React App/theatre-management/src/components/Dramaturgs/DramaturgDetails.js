import React, { useEffect, useState } from 'react';
import { fetchDramaturg, deleteDramaturg } from '../../services/dramaturgsService';
import { useParams, useNavigate } from 'react-router-dom';

function DramaturgDetails() {
  const [dramaturg, setDramaturg] = useState({});
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    fetchDramaturg(id)
      .then(response => setDramaturg(response.data))
      .catch(error => console.error('Error fetching dramaturg details:', error));
  }, [id]);

  const handleDelete = () => {
    deleteDramaturg(id)
      .then(() => navigate('/dramaturgs'))
      .catch(error => console.error('Error deleting dramaturg:', error));
  };

  return (
    <div>
      <h1>Dramaturg Details</h1>
      <p>Name: {dramaturg.name}</p>
      <p>Age: {dramaturg.age}</p>
      {/* Add more details as needed */}
      <button onClick={() => navigate(`/dramaturgs/edit/${id}`)}>Edit</button>
      <button onClick={handleDelete}>Delete</button>
    </div>
  );
}

export default DramaturgDetails;
