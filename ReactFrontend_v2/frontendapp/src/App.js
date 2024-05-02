import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Actors from './components/Actors';
import Actorsv2 from './components/Actorsv2';
import Dramaturgs from './components/Dramaturgs';
import StagePlays from './components/StagePlays';
import Home from './components/Home';

function App() {
  return (
    <Router>
      <div>
        <nav>
          <Link to="/">Home</Link> | <Link to="/actors">Actors</Link> | <Link to="/actorsv2">Actorsv2</Link>| <Link to="/dramaturgs">Dramaturgs</Link>| <Link to="/stagePlays">StagePlays</Link>
        </nav>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/actors" element={<Actors />} />
          <Route path="/actorsv2" element={<Actorsv2 />} />
          <Route path="/dramaturgs" element={<Dramaturgs />} />
          <Route path="/stagePlays" element={<StagePlays />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
