import React, { useState, useEffect } from 'react';
import axios from 'axios';

function Actors() {
    const [actors, setActors] = useState([]); // Holds the list of actors
    const [selectedActor, setSelectedActor] = useState(null); // Holds the selected actor for editing or deleting
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    // Fetch actors from the backend on component mount
    useEffect(() => {
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

        fetchActors();
    }, []);

    // Add a new actor
    const addActor = async (actor) => {
        try {
            const response = await axios.post('http://localhost:62255/actor', actor);
            setActors([...actors, response.data]); // Update the list with the new actor
        } catch (error) {
            console.error("Failed to add actor", error);
        }
    };

    // Update an existing actor
    const updateActor = async (actor) => {
        try {
            await axios.put(`http://localhost:62255/actor/${actor.id}`, actor);
            const updatedActors = actors.map(a => a.id === actor.id ? actor : a);
            setActors(updatedActors);
        } catch (error) {
            console.error("Failed to update actor", error);
        }
    };

    // Delete an actor
    const deleteActor = async (id) => {
        try {
            await axios.delete(`http://localhost:62255/actor/${id}`);
            setActors(actors.filter(actor => actor.id !== id));
        } catch (error) {
            console.error("Failed to delete actor", error);
        }
    };

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div>
            <h1>Actors</h1>
            <button onClick={() => setSelectedActor({ id: null, name: '', age: '', gender: '' })}>
                Add Actor
            </button>
            {actors.map(actor => (
                <div key={actor.id} onClick={() => setSelectedActor(actor)}>
                    {actor.name}
                </div>
            ))}
            {selectedActor && (
                <div>
                    <h2>Edit/Delete Actor</h2>
                    <form onSubmit={e => {
                        e.preventDefault();
                        selectedActor.id ? updateActor(selectedActor) : addActor(selectedActor);
                    }}>
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
                        <button type="submit">Save</button>
                        <button onClick={() => deleteActor(selectedActor.id)} type="button">
                            Delete
                        </button>
                    </form>
                </div>
            )}
        </div>
    );
}

export default Actors;
