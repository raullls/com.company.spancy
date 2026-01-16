import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function CustomerManager() {
    const [customerName, setCustomerName] = useState('');
    const [amount, setAmount] = useState(0);
    const [customers, setCustomers] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedCustomer, setSelectedCustomer] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handleCreateOrEditCustomer = () => {
        if (selectedCustomer) {
            axios.post("/onetoonebidirectional/edit", {
                Id: selectedCustomer.id,
                Name: customerName,
                Amount: amount
            }).then(() => {
                setErrorMessages([]);
                setSelectedCustomer(null);
                setCustomerName("");
                setAmount(0);
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (customerName && amount) {
            axios.post("/onetoonebidirectional/create", {
                Name: customerName,
                Amount: amount
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setCustomerName("");
                setAmount(0);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditCustomer = () => {
        setSelectedCustomer(null);
        setCustomerName("");
        setAmount(0);
    };

    const handleDeleteCustomer = (customerId) => {
        axios.post(`/onetoonebidirectional/remove/${customerId}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    };

    useEffect(() => {
        axios.get("/onetoonebidirectional/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setCustomers(response.data.data);
                }
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Customer Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="customerName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter customer name" value={customerName} onChange={(e) => setCustomerName(e.target.value)} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="amount">
                    <Form.Label column md={2} className="text-md-end">
                        Amount:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="number" placeholder="Enter amount" value={amount} onChange={(e) => setAmount(parseFloat(e.target.value))} />
                    </Col>
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditCustomer}>
                            {selectedCustomer ? 'Edit' : 'Create'}
                        </Button>
                        {selectedCustomer && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditCustomer}>Cancel</Button>
                        )}
                    </Col>
                </Form.Group>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Customer Name</th>
                                <th>Amount</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {customers.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No customers</td>
                                </tr>
                            ) : (
                                customers.map((c, index) => {
                                    return (
                                        <tr key={c.id}>
                                            <td>{c.name}</td>
                                            <td>{c.amount}</td>
                                            <td>
                                                <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                                    setSelectedCustomer(c);
                                                    setCustomerName(c.name);
                                                    setAmount(c.amount);
                                                }}>Edit</Button>
                                                <Button variant="danger" size="sm" onClick={() => handleDeleteCustomer(c.id)}>Delete</Button>
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

export default CustomerManager;