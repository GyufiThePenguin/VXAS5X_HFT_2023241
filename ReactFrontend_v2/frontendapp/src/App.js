import React from 'react';
import { BrowserRouter as Router, Routes, Route, NavLink } from 'react-router-dom';
import { AppBar, Toolbar, Button, Container, Typography } from '@mui/material';
import Actors from './components/Actors';
import Actorsv2 from './components/Actorsv2';
import Dramaturgs from './components/Dramaturgs';
import StagePlays from './components/StagePlays';
import Home from './components/Home';

function App() {
  return (
    <Router>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            Actors Repository
          </Typography>
          <Button color="inherit" component={NavLink} to="/" end>Home</Button>
          <Button color="inherit" component={NavLink} to="/actors">Actors</Button>
          <Button color="inherit" component={NavLink} to="/actorsv2">Actorsv2</Button>
          <Button color="inherit" component={NavLink} to="/dramaturgs">Dramaturgs</Button>
          <Button color="inherit" component={NavLink} to="/stagePlays">Stage Plays</Button>
        </Toolbar>
      </AppBar>
      <Container style={{ marginTop: 20 }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/actors" element={<Actors />} />
          <Route path="/actorsv2" element={<Actorsv2 />} />
          <Route path="/dramaturgs" element={<Dramaturgs />} />
          <Route path="/stagePlays" element={<StagePlays />} />
        </Routes>
      </Container>
    </Router>
  );
}

export default App;
