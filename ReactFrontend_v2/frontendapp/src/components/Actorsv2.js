import React, { useState, useEffect } from 'react';
import axios from 'axios';

function Actorsv2() {
    const [actors, setActors] = useState([]);
    const [selectedActor, setSelectedActor] = useState({ id: null, name: '', age: '', gender: '' });
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [formError, setFormError] = useState(''); // To handle and display form errors

    useEffect(() => {
        fetchActors();
    }, []);

    const fetchActors = async () => {
        setIsLoading(true);
        try {
            const response = await axios.get('http://localhost:62255/actor');
            setActors(response.data);
        } catch (error) {
            setError(error.message);
        }
        setIsLoading(false);
    };

    const handleFormSubmit = async (e) => {
        e.preventDefault();
        if (!selectedActor.name) {
            setFormError('Name is required.');
            return;
        }
        if (selectedActor.age < 0 || selectedActor.age === '') {
            setFormError('Please provide a valid age.');
            return;
        }
    
        const actorData = {
            name: selectedActor.name,
            age: Number(selectedActor.age),
            gender: selectedActor.gender,
            role: selectedActor.role
        };
    
        try {
            if (selectedActor.id) {
                await updateActor({ ...actorData, id: selectedActor.id });
            } else {
                await addActor(actorData); // Makes suure no id is sent when creating a new actor
            }
            setSelectedActor({ id: null, name: '', age: '', gender: '', role: '' }); // Reset form
            setFormError(''); // Clear any form errors
        } catch (error) {
            console.error("Failed to save actor", error);
            setFormError('Failed to save actor.');
        }
    };
    
    

    const addActor = async (actor) => {
        try {
            const response = await axios.post('http://localhost:62255/actor', actor);
            console.log("Actor added:", response.data);
            setActors([...actors, response.data]);
        } catch (error) {
            console.error("Failed to add actor:", error.response ? error.response.data : error);
            setFormError('Failed to add actor. ' + (error.response ? error.response.data : 'No error data'));
        }
    };
    

    const updateActor = async (actor) => {
        await axios.put(`http://localhost:62255/actor/${actor.id}`, actor);
        const updatedActors = actors.map(a => a.id === actor.id ? actor : a);
        setActors(updatedActors);
    };

    const deleteActor = async (id) => {
        await axios.delete(`http://localhost:62255/actor/${id}`);
        setActors(actors.filter(actor => actor.id !== id));
    };

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div>
            <h1>Actors</h1>
            <button onClick={() => setSelectedActor({ id: null, name: '', age: '', gender: '', role: '' })}>
                Add Actor
            </button>
            {actors.map(actor => (
                <div key={actor.id} onClick={() => setSelectedActor(actor)}>
                    {actor.name}
                </div>
            ))}
            {selectedActor && (
                <div>
                    <h2>{selectedActor.id ? 'Edit' : 'Add'} Actor</h2>
                    <form onSubmit={handleFormSubmit}>
                        <label>Name:</label>
                        <input
                            type="text"
                            value={selectedActor.name}
                            onChange={e => setSelectedActor({ ...selectedActor, name: e.target.value })}
                        />
                        <label>Age:</label>
                        <input
                            type="number"
                            value={selectedActor.age}
                            onChange={e => setSelectedActor({ ...selectedActor, age: e.target.value })}
                        />
                        <label>Gender:</label>
                        <input
                            type="text"
                            value={selectedActor.gender}
                            onChange={e => setSelectedActor({ ...selectedActor, gender: e.target.value })}
                        />
                        {formError && <p className="error">{formError}</p>}
                        <button type="submit">Save</button>
                        {selectedActor.id && (
                            <button onClick={() => deleteActor(selectedActor.id)} type="button">
                                Delete
                            </button>
                        )}
                    </form>
                </div>
            )}
        </div>
    );
}

export default Actorsv2;

