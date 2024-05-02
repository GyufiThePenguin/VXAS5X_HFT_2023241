/* import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createActor, fetchActor, updateActor } from '../../services/actorsService';

function ActorForm() {
  const [formData, setFormData] = useState({ name: '', age: '' });
  const { id } = useParams();
  const navigate = useNavigate();
  
  useEffect(() => {
    if (id) {
      fetchActor(id)
        .then(response => setFormData(response.data))
        .catch(error => console.error('Error fetching actor:', error));
    }
  }, [id]);

  const handleChange = (event) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const action = id ? updateActor : createActor;
    action(formData)
      .then(() => navigate('/actors'))
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

export default ActorForm;
 */

import React, { useState, useEffect } from 'react';
import { TextField, Button, Container, Typography } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import { createActor, fetchActor, updateActor } from '../../services/actorsService';

function ActorForm() {
  const [formData, setFormData] = useState({ name: '', age: '' });
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      fetchActor(id)
        .then(response => setFormData(response.data))
        .catch(error => console.error('Error fetching actor:', error));
    }
  }, [id]);

  const handleChange = (event) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const action = id ? updateActor : createActor;
    action(formData)
      .then(() => navigate('/actors'))
      .catch(error => console.error('Error submitting form:', error));
  };

  return (
    <Container component="main" maxWidth="xs">
      <Typography component="h1" variant="h5">
        {id ? 'Edit Actor' : 'Add Actor'}
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          variant="outlined"
          margin="normal"
          required
          fullWidth
          id="name"
          label="Actor Name"
          name="name"
          autoComplete="name"
          autoFocus
          value={formData.name}
          onChange={handleChange}
        />
        <TextField
          variant="outlined"
          margin="normal"
          required
          fullWidth
          name="age"
          label="Age"
          type="number"
          id="age"
          autoComplete="age"
          value={formData.age}
          onChange={handleChange}
        />
        <Button
          type="submit"
          fullWidth
          variant="contained"
          color="primary"
        >
          Save
        </Button>
      </form>
    </Container>
  );
}

export default ActorForm;
