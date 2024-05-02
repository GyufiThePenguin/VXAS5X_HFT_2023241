import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from '@mui/material';

function Actors() {
    const [actors, setActors] = useState([]);
    const [open, setOpen] = useState(false);
    const [selectedActor, setSelectedActor] = useState({ name: '', age: '', gender: '', role: '' });

    useEffect(() => {
        axios.get('http://localhost:62255/actor').then(response => {
            setActors(response.data);
        });
    }, []);

    const handleOpen = (actor) => {
        setSelectedActor(actor);
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleSave = () => {
        const method = selectedActor.id ? 'put' : 'post';
        const url = selectedActor.id ? `http://localhost:62255/actor/${selectedActor.id}` : 'http://localhost:62255/actor';
        axios[method](url, selectedActor).then(() => {
            setActors(prev => prev.map(a => a.id === selectedActor.id ? selectedActor : a));
            handleClose();
        });
    };

    return (
        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Name</TableCell>
                        <TableCell>Age</TableCell>
                        <TableCell>Gender</TableCell>
                        <TableCell>Role</TableCell>
                        <TableCell>Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {actors.map((actor) => (
                        <TableRow key={actor.id}>
                            <TableCell>{actor.name}</TableCell>
                            <TableCell>{actor.age}</TableCell>
                            <TableCell>{actor.gender}</TableCell>
                            <TableCell>{actor.role}</TableCell>
                            <TableCell>
                                <Button onClick={() => handleOpen(actor)}>Edit</Button>
                                <Button onClick={() => setActors(prev => prev.filter(a => a.id !== actor.id))}>Delete</Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>{selectedActor.id ? 'Edit Actor' : 'Add Actor'}</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Name"
                        type="text"
                        fullWidth
                        variant="standard"
                        value={selectedActor.name}
                        onChange={e => setSelectedActor({ ...selectedActor, name: e.target.value })}
                    />
                    <TextField
                        margin="dense"
                        label="Age"
                        type="number"
                        fullWidth
                        variant="standard"
                        value={selectedActor.age}
                        onChange={e => setSelectedActor({ ...selectedActor, age: e.target.value })}
                    />
                    <TextField
                        margin="dense"
                        label="Gender"
                        type="text"
                        fullWidth
                        variant="standard"
                        value={selectedActor.gender}
                        onChange={e => setSelectedActor({ ...selectedActor, gender: e.target.value })}
                    />
                    <TextField
                        margin="dense"
                        label="Role"
                        type="text"
                        fullWidth
                        variant="standard"
                        value={selectedActor.role}
                        onChange={e => setSelectedActor({ ...selectedActor, role: e.target.value })}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={handleSave}>Save</Button>
                </DialogActions>
            </Dialog>
        </TableContainer>
    );
}

export default Actors;
