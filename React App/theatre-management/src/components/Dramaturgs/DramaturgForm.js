import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createDramaturg, fetchDramaturg, updateDramaturg } from '../../services/dramaturgsService';

function DramaturgForm() {
  const [formData, setFormData] = useState({ name: '', age: '' });
  const { id } = useParams();
  const navigate = useNavigate();
  
  useEffect(() => {
    if (id) {
      fetchDramaturg(id)
        .then(response => setFormData(response.data))
        .catch(error => console.error('Error fetching dramaturg:', error));
    }
  }, [id]);

  const handleChange = (event) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const action = id ? updateDramaturg : createDramaturg;
    action(formData)
      .then(() => navigate('/dramaturgs'))
      .catch(error => console.error('Error submitting form:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>Name:
        <input type="text" name="name" value={formData.name} onChange={handleChange} required />
      </label>
      <label>Age:
        <input type="number" name="age" value={formData.age} onChange={handleChange} required />
      </label>
      <button type="submit">Save</button>
    </form>
  );
}

export default DramaturgForm;
