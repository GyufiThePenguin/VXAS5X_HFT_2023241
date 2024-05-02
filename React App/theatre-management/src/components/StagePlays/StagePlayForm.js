import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createStagePlay, fetchStagePlay, updateStagePlay } from '../../services/stagePlaysService';

function StagePlayForm() {
  const [formData, setFormData] = useState({ title: '', premier: '' });
  const { id } = useParams();
  const navigate = useNavigate();
  
  useEffect(() => {
    if (id) {
      fetchStagePlay(id)
        .then(response => setFormData(response.data))
        .catch(error => console.error('Error fetching stagePlay:', error));
    }
  }, [id]);

  const handleChange = (event) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const action = id ? updateStagePlay : createStagePlay;
    action(formData)
      .then(() => navigate('/stagePlays'))
      .catch(error => console.error('Error submitting form:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>Name:
        <input type="text" name="title" value={formData.title} onChange={handleChange} required />
      </label>
      <label>Age:
        <input type="number" name="premier" value={formData.premier} onChange={handleChange} required />
      </label>
      <button type="submit">Save</button>
    </form>
  );
}

export default StagePlayForm;
