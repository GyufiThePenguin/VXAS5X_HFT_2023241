import React, { useEffect, useState } from 'react';
import { fetchDramaturgs } from '../../services/dramaturgsService';

function DramaturgsList() {
  const [dramaturgs, setDramaturgs] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchDramaturgs()
      .then(response => {
        setDramaturgs(response.data);
      })
      .catch(error => {
        console.error('Error fetching dramaturgs:', error);
        setError("Failed to load dramaturgs.");
      });
  }, []);

  return (
    <div>
      <h1>Dramaturgs List</h1>
      {error ? <p>{error}</p> : (
        <ul>
          {dramaturgs.map(dramaturg => <li key={dramaturg.id}>{dramaturg.name}</li>)}
        </ul>
      )}
    </div>
  );
}

export default DramaturgsList;
