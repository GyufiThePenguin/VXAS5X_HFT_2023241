import React, { useState, useEffect } from 'react';
import axios from 'axios';

function StagePlays() {
    const [stagePlays, setStagePlays] = useState([]);
    const [selectedStagePlay, setSelectedStagePlay] = useState({ id: null, title: '', premier: 0, profit: 0 });
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [formError, setFormError] = useState(''); // To handle and display form errors

    useEffect(() => {
        fetchStagePlays();
    }, []);

    const fetchStagePlays = async () => {
        setIsLoading(true);
        try {
            const response = await axios.get('http://localhost:62255/stagePlay');
            setStagePlays(response.data);
        } catch (error) {
            setError(error.messpremier);
        }
        setIsLoading(false);
    };

    const handleFormSubmit = async (e) => {
        e.preventDefault();
        if (!selectedStagePlay.title) {
            setFormError('Title is required.');
            return;
        }
        if (selectedStagePlay.premier < 0 || selectedStagePlay.premier === '') {
            setFormError('Please provide a valid premier.');
            return;
        }
    
        const stagePlayData = {
            title: selectedStagePlay.title,
            premier: Number(selectedStagePlay.premier),
            profit: Number(selectedStagePlay.profit),
            rating: selectedStagePlay.rating
        };
    
        try {
            if (selectedStagePlay.id) {
                await updateStagePlay({ ...stagePlayData, id: selectedStagePlay.id });
            } else {
                await addStagePlay(stagePlayData); // Makes suure no id is sent when creating a new stagePlay
            }
            setSelectedStagePlay({ id: null, title: '', premier: 0, profit: 0, rating: '' }); // Reset form
            setFormError(''); // Clear any form errors
        } catch (error) {
            console.error("Failed to save stagePlay", error);
            setFormError('Failed to save stagePlay.');
        }
    };
    
    

    const addStagePlay = async (stagePlay) => {
        try {
            const response = await axios.post('http://localhost:62255/stagePlay', stagePlay);
            console.log("StagePlay added:", response.data);
            setStagePlays([...stagePlays, response.data]);
        } catch (error) {
            console.error("Failed to add stagePlay:", error.response ? error.response.data : error);
            setFormError('Failed to add stagePlay. ' + (error.response ? error.response.data : 'No error data'));
        }
    };
    

    const updateStagePlay = async (stagePlay) => {
        await axios.put(`http://localhost:62255/stagePlay/${stagePlay.id}`, stagePlay);
        const updatedStagePlays = stagePlays.map(a => a.id === stagePlay.id ? stagePlay : a);
        setStagePlays(updatedStagePlays);
    };

    const deleteStagePlay = async (id) => {
        await axios.delete(`http://localhost:62255/stagePlay/${id}`);
        setStagePlays(stagePlays.filter(stagePlay => stagePlay.id !== id));
    };

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div>
            <h1>StagePlays</h1>
            <button onClick={() => setSelectedStagePlay({ id: null, title: '', premier: '', profit: '', rating: '' })}>
                Add StagePlay
            </button>
            {stagePlays.map(stagePlay => (
                <div key={stagePlay.id} onClick={() => setSelectedStagePlay(stagePlay)}>
                    {stagePlay.title}
                </div>
            ))}
            {selectedStagePlay && (
                <div>
                    <h2>{selectedStagePlay.id ? 'Edit' : 'Add'} StagePlay</h2>
                    <form onSubmit={handleFormSubmit}>
                        <label>Title:</label>
                        <input
                            type="text"
                            value={selectedStagePlay.title}
                            onChange={e => setSelectedStagePlay({ ...selectedStagePlay, title: e.target.value })}
                        />
                        <label>Premier:</label>
                        <input
                            type="number"
                            value={selectedStagePlay.premier}
                            onChange={e => setSelectedStagePlay({ ...selectedStagePlay, premier: e.target.value })}
                        />
                        <label>Profit:</label>
                        <input
                            type="number"
                            value={selectedStagePlay.profit}
                            onChange={e => setSelectedStagePlay({ ...selectedStagePlay, profit: e.target.value })}
                        />
                        {formError && <p classTitle="error">{formError}</p>}
                        <button type="submit">Save</button>
                        {selectedStagePlay.id && (
                            <button onClick={() => deleteStagePlay(selectedStagePlay.id)} type="button">
                                Delete
                            </button>
                        )}
                    </form>
                </div>
            )}
        </div>
    );
}

export default StagePlays;

