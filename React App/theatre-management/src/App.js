import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import ActorsPage from './components/Actors/ActorsPage';
import DramaturgsList from './components/Dramaturgs/DramaturgsList';
import StagePlaysList from './components/StagePlays/StagePlaysList';
import Home from './components/Home';  // A Home component if you have one

function App() {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/actors">Actors</Link>
            </li>
            <li>
              <Link to="/dramaturgs">Dramaturgs</Link>
            </li>
            <li>
              <Link to="/stageplays">Stage Plays</Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/actors" element={<ActorsPage />} />
          <Route path="/dramaturgs" element={<DramaturgsList />} />
          <Route path="/stageplays" element={<StagePlaysList />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
