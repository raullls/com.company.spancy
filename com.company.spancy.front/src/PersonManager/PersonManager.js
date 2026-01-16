import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function PersonManager() {
    const [personName, setPersonName] = useState('');
    const [phoneNumbers, setPhoneNumbers] = useState(['']);
    const [persons, setPersons] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedPerson, setSelectedPerson] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handlePhoneChange = (index, value) => {
        const newPhones = [...phoneNumbers];
        newPhones[index] = value;
        setPhoneNumbers(newPhones);
    };

    const handleAddPhone = () => {
        setPhoneNumbers([...phoneNumbers, '']);
    };

    const handleRemovePhone = (index) => {
        if (phoneNumbers.length > 1) {
            const newPhones = phoneNumbers.filter((_, i) => i !== index);
            setPhoneNumbers(newPhones);
        }
    };

    const handleCreateOrEditPerson = () => {
        if (selectedPerson) {
            axios.post("/onetomanyunidirectional/edit", {
                Id: selectedPerson.id,
                Name: personName,
                Numbers: phoneNumbers
            }).then(() => {
                setErrorMessages([]);
                setSelectedPerson(null);
                setPersonName("");
                setPhoneNumbers([""]);
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (personName) {
            axios.post("/onetomanyunidirectional/create", {
                Name: personName,
                Numbers: phoneNumbers
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setPersonName("");
                setPhoneNumbers([""]);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditPerson = () => {
        setSelectedPerson(null);
        setPersonName("");
        setPhoneNumbers([""]);
    };

    const handleDeletePerson = (personId) => {
        axios.post(`/onetomanyunidirectional/remove/${personId}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    };

    useEffect(() => {
        axios.get("/onetomanyunidirectional/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setPersons(response.data.data);
                }
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Person Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="personName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter person name" value={personName} onChange={(e) => setPersonName(e.target.value)} />
                    </Col>
                </Form.Group>
                {phoneNumbers.map((phone, index) => (
                    <Form.Group as={Row} className="mb-3" controlId={`phone-${index}`} key={index}>
                        {index === 0 && (
                            <Form.Label column md={2} className="text-md-end">
                                Phone:
                            </Form.Label>
                        )}
                        {index !== 0 && <Col md={2} />}
                        <Col md={6}>
                            <Form.Control type="text" placeholder={`Enter phone number ${index + 1}`} value={phone} onChange={(e) => handlePhoneChange(index, e.target.value)} />
                        </Col>
                        <Col md="auto" className="d-flex align-items-center">
                            {index === 0 ? (
                                <Button variant="success" size="sm" onClick={handleAddPhone} className="me-2" style={{ width: "2.5rem" }}>+</Button>
                            ) : (
                                <Button variant="danger" size="sm" onClick={() => handleRemovePhone(index)} style={{ width: "2.5rem" }}>-</Button>
                            )}
                        </Col>
                    </Form.Group>
                ))}
                <Row className="mb-3">
                    <Col md={2} />
                    <Col md={6} />
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditPerson}>
                            {selectedPerson ? 'Edit' : 'Create'}
                        </Button>
                        {selectedPerson && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditPerson}>Cancel</Button>
                        )}
                    </Col>
                </Row>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Person Name</th>
                                <th>Numbers</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {persons.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No persons</td>
                                </tr>
                            ) : (
                                persons.map((p, index) => {
                                    return (
                                        <tr key={p.id}>
                                            <td>{p.name}</td>
                                            <td>
                                                <Form.Select>
                                                    {(p.numbers || []).map((num, i) => (
                                                        <option key={i}>{num}</option>
                                                    ))}
                                                </Form.Select>
                                            </td>
                                            <td>
                                                <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                                    setSelectedPerson(p);
                                                    setPersonName(p.name);
                                                    setPhoneNumbers(p.numbers ?? ['']);
                                                }}>Edit</Button>
                                                <Button variant="danger" size="sm" onClick={() => handleDeletePerson(p.id)}>Delete</Button>
                                            </td>
                                        </tr>
                                    )
                                })
                            )}
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>
    );
};

export default PersonManager;