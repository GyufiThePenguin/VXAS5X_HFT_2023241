import React, { useState, useEffect } from 'react';
import axios from 'axios';

function Dramaturgs() {
    const [dramaturgs, setDramaturgs] = useState([]);
    const [selectedDramaturg, setSelectedDramaturg] = useState({ id: null, name: '', age: '', gender: '' });
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [formError, setFormError] = useState(''); // To handle and display form errors

    useEffect(() => {
        fetchDramaturgs();
    }, []);

    const fetchDramaturgs = async () => {
        setIsLoading(true);
        try {
            const response = await axios.get('http://localhost:62255/dramaturg');
            setDramaturgs(response.data);
        } catch (error) {
            setError(error.message);
        }
        setIsLoading(false);
    };

    const handleFormSubmit = async (e) => {
        e.preventDefault();
        if (!selectedDramaturg.name) {
            setFormError('Name is required.');
            return;
        }
        if (selectedDramaturg.age < 0 || selectedDramaturg.age === '') {
            setFormError('Please provide a valid age.');
            return;
        }
    
        const dramaturgData = {
            name: selectedDramaturg.name,
            age: Number(selectedDramaturg.age),
            gender: selectedDramaturg.gender,
            role: selectedDramaturg.role
        };
    
        try {
            if (selectedDramaturg.id) {
                await updateDramaturg({ ...dramaturgData, id: selectedDramaturg.id });
            } else {
                await addDramaturg(dramaturgData); // Makes suure no id is sent when creating a new dramaturg
            }
            setSelectedDramaturg({ id: null, name: '', age: '', gender: '', role: '' }); // Reset form
            setFormError(''); // Clear any form errors
        } catch (error) {
            console.error("Failed to save dramaturg", error);
            setFormError('Failed to save dramaturg.');
        }
    };
    
    

    const addDramaturg = async (dramaturg) => {
        try {
            const response = await axios.post('http://localhost:62255/dramaturg', dramaturg);
            console.log("Dramaturg added:", response.data);
            setDramaturgs([...dramaturgs, response.data]);
        } catch (error) {
            console.error("Failed to add dramaturg:", error.response ? error.response.data : error);
            setFormError('Failed to add dramaturg. ' + (error.response ? error.response.data : 'No error data'));
        }
    };
    

    const updateDramaturg = async (dramaturg) => {
        await axios.put(`http://localhost:62255/dramaturg/${dramaturg.id}`, dramaturg);
        const updatedDramaturgs = dramaturgs.map(a => a.id === dramaturg.id ? dramaturg : a);
        setDramaturgs(updatedDramaturgs);
    };

    const deleteDramaturg = async (id) => {
        await axios.delete(`http://localhost:62255/dramaturg/${id}`);
        setDramaturgs(dramaturgs.filter(dramaturg => dramaturg.id !== id));
    };

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div>
            <h1>Dramaturgs</h1>
            <button onClick={() => setSelectedDramaturg({ id: null, name: '', age: '', gender: '', role: '' })}>
                Add Dramaturg
            </button>
            {dramaturgs.map(dramaturg => (
                <div key={dramaturg.id} onClick={() => setSelectedDramaturg(dramaturg)}>
                    {dramaturg.name}
                </div>
            ))}
            {selectedDramaturg && (
                <div>
                    <h2>{selectedDramaturg.id ? 'Edit' : 'Add'} Dramaturg</h2>
                    <form onSubmit={handleFormSubmit}>
                        <label>Name:</label>
                        <input
                            type="text"
                            value={selectedDramaturg.name}
                            onChange={e => setSelectedDramaturg({ ...selectedDramaturg, name: e.target.value })}
                        />
                        <label>Age:</label>
                        <input
                            type="number"
                            value={selectedDramaturg.age}
                            onChange={e => setSelectedDramaturg({ ...selectedDramaturg, age: e.target.value })}
                        />
                        <label>Gender:</label>
                        <input
                            type="text"
                            value={selectedDramaturg.gender}
                            onChange={e => setSelectedDramaturg({ ...selectedDramaturg, gender: e.target.value })}
                        />
                        {formError && <p className="error">{formError}</p>}
                        <button type="submit">Save</button>
                        {selectedDramaturg.id && (
                            <button onClick={() => deleteDramaturg(selectedDramaturg.id)} type="button">
                                Delete
                            </button>
                        )}
                    </form>
                </div>
            )}
        </div>
    );
}

export default Dramaturgs;

