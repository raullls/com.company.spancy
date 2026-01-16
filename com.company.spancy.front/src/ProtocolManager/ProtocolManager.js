import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function ProtocolManager() {
    const [protocolName, setProtocolName] = useState('');
    const [protocolType, setProtocolType] = useState('');
    const [protocols, setProtocols] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [errorMessages, setErrorMessages] = useState([]);
    const [selectedProtocol, setSelectedProtocol] = useState(null);

    // ------ Protocol actions ------
    const handleCreateOrEditProtocol = () => {
        const url = selectedProtocol
            ? "/singletableinheritance/edit"
            : "/singletableinheritance/create";

        const payload = selectedProtocol
            ? { Id: selectedProtocol.id, Name: protocolName, Type: protocolType }
            : { Name: protocolName, Type: protocolType };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedProtocol(null);
                setProtocolName("");
                setProtocolType("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteProtocol = id =>
        axios.post(`/singletableinheritance/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    
    // ------ Data fetch ------
    useEffect(() => {
        axios.get("/singletableinheritance/findAll")
            .then(response => setProtocols(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Protocol Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Protocol Section ------ */}
                <Col md={{ span: 8, offset: 2 }}>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="protocolType">
                            <Form.Label column sm={3} className="text-start">
                                Choose Protocol:
                            </Form.Label>
                            <Col sm={9}>
                                <Form.Select
                                    value={protocolType}
                                    onChange={e => setProtocolType(e.target.value)}
                                >
                                    <option value="">Select Protocol Type</option>
                                    <option value="tcp">TCP</option>
                                    <option value="snmp">SNMP</option>
                                </Form.Select>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="protocolName">
                            <Form.Label column sm={3} className="text-start">Name:</Form.Label>
                            <Col sm={9}>
                                <div className="d-flex gap-2">
                                    <Form.Control type="text" placeholder="Enter protocol name" value={protocolName} onChange={(e) => setProtocolName(e.target.value)} />
                                    <Button variant="primary" onClick={handleCreateOrEditProtocol}>
                                        {selectedProtocol ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedProtocol && (
                                        <Button variant="secondary" onClick={() => { setSelectedProtocol(null); setProtocolName(""); setProtocolType(""); }}>Cancel</Button>
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
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {protocols.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No protocols</td>
                                </tr>
                            ) : protocols.map(p => (
                                <tr key={p.id}>
                                    <td>{p.type}</td>
                                    <td>{p.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => { setSelectedProtocol(p); setProtocolName(p.name); setProtocolType(p.type); }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteProtocol(p.id)}>Delete</Button>
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

export default ProtocolManager;