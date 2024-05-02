import React from 'react';
import { Link } from 'react-router-dom';
import { Button, Typography, Container } from '@mui/material';

function Home() {
    return (
        <Container>
            <Typography variant="h2" gutterBottom>Welcome to the Actors Repository</Typography>
            <Button variant="contained" color="primary" component={Link} to="/actors">Actors</Button>
            <Button variant="contained" color="primary" component={Link} to="/dramaturgs">Dramaturgs</Button>
            <Button variant="contained" color="primary" component={Link} to="/stageplays">Stage Plays</Button>
        </Container>
    );
}

export default Home;
