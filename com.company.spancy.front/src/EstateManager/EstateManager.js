import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function EstateManager() {
    const [estateName, setEstateName] = useState('');
    const [estateType, setEstateType] = useState('');
    const [floors, setFloors] = useState('');
    const [area, setArea] = useState('');
    const [estates, setEstates] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [errorMessages, setErrorMessages] = useState([]);
    const [selectedEstate, setSelectedEstate] = useState(null);

    // ------ Estate actions ------
    const handleCreateOrEditEstate = () => {
        const url = selectedEstate
            ? "/concretetableinheritance/edit"
            : "/concretetableinheritance/create";

        const basePayload = {
            Name: estateName,
            Type: estateType
        };

        const typePayload =
            estateType === 'building'
                ? { Floors: floors }
                : estateType === 'land'
                    ? { Area: area }
                    : {};

        const payload = selectedEstate
            ? { Id: selectedEstate.id, ...basePayload, ...typePayload }
            : { ...basePayload, ...typePayload };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedEstate(null);
                setEstateName("");
                setEstateType("");
                setFloors("");
                setArea("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteEstate = id =>
        axios.post(`/concretetableinheritance/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    
    // ------ Data fetch ------
    useEffect(() => {
        axios.get("/concretetableinheritance/findAll")
            .then(response => setEstates(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Estate Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Estate Section ------ */}
                <Col md={{ span: 8, offset: 2 }}>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="estateType">
                            <Form.Label column sm={3} className="text-start">
                                Choose Estate:
                            </Form.Label>
                            <Col sm={9}>
                                <Form.Select
                                    value={estateType}
                                    onChange={e => setEstateType(e.target.value)}
                                >
                                    <option value="">Select Estate Type</option>
                                    <option value="building">Building</option>
                                    <option value="land">Land</option>
                                </Form.Select>
                            </Col>
                        </Form.Group>
                        {estateType === 'building' && (
                            <Form.Group as={Row} className="mb-3 align-items-center" controlId="floors">
                                <Form.Label column sm={3} className="text-start">
                                    Floors:
                                </Form.Label>
                                <Col sm={9}>
                                    <Form.Control
                                        type="number"
                                        placeholder="Enter number of floors"
                                        value={floors}
                                        onChange={e => setFloors(e.target.value)}
                                    />
                                </Col>
                            </Form.Group>
                        )}
                        {estateType === 'land' && (
                            <Form.Group as={Row} className="mb-3 align-items-center" controlId="area">
                                <Form.Label column sm={3} className="text-start">
                                    Area:
                                </Form.Label>
                                <Col sm={9}>
                                    <Form.Control
                                        type="number"
                                        placeholder="Enter land area"
                                        value={area}
                                        onChange={e => setArea(e.target.value)}
                                    />
                                </Col>
                            </Form.Group>
                        )}
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="estateName">
                            <Form.Label column sm={3} className="text-start">Name:</Form.Label>
                            <Col sm={9}>
                                <div className="d-flex gap-2">
                                    <Form.Control type="text" placeholder="Enter estate name" value={estateName} onChange={(e) => setEstateName(e.target.value)} />
                                    <Button variant="primary" onClick={handleCreateOrEditEstate}>
                                        {selectedEstate ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedEstate && (
                                        <Button variant="secondary" onClick={() => { setSelectedEstate(null); setEstateName(""); setEstateType(""); setFloors(""); setArea(""); }}>Cancel</Button>
                                    )}
                                </div>
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Type</th>
                                <th>Name</th>
                                <th>Details</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {estates.length === 0 ? (
                                <tr>
                                    <td colSpan="4" className="text-center text-muted">No estates</td>
                                </tr>
                            ) : estates.map(e => (
                                <tr key={e.id}>
                                    <td>{e.type}</td>
                                    <td>{e.name}</td>
                                    <td>
                                        {e.type === 'building' && `Floors: ${e.floors}`}
                                        {e.type === 'land' && `Area: ${e.area}`}
                                    </td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                            setSelectedEstate(e);
                                            setEstateName(e.name);
                                            setEstateType(e.type);
                                            if (e.type === 'building') {
                                                setFloors(e.floors || '');
                                                setArea('');
                                            } else if (e.type === 'land') {
                                                setArea(e.area || '');
                                                setFloors('');
                                            }
                                        }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteEstate(e.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>
    );
};

export default EstateManager;