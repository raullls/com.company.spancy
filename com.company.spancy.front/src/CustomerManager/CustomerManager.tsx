import axios from 'axios';
import { useEffect, useState } from 'react';
import { Button, Col, Container, Form, Row, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';
import type { Response } from '../Shared/Response';
import type { Customer } from './Customer';

function CustomerManager() {
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [name, setName] = useState('');
    const [amount, setAmount] = useState('');
    const [selectedCustomer, setSelectedCustomer] = useState<Customer | null>(null);
    const [errorMessages, setErrorMessages] = useState<string[]>([]);
    const [refreshToken, setRefreshToken] = useState(0);

    useEffect(() => {
        axios.get<Response<Customer[]>>('/api/onetoonebidirectional/findAll')
            .then(response => {
                const payload = response.data?.data;
                setCustomers(Array.isArray(payload) ? payload : []);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not load customers.']);
            });
    }, [refreshToken]);

    const resetForm = () => {
        setSelectedCustomer(null);
        setName('');
        setAmount('');
    };

    const saveCustomer = () => {
        const parsedAmount = Number(amount);
        if (!name.trim() || amount.trim() === '' || Number.isNaN(parsedAmount)) {
            setErrorMessages(['Name and amount are required.']);
            return;
        }

        const endpoint = selectedCustomer ? '/api/onetoonebidirectional/edit' : '/api/onetoonebidirectional/create';
        const payload = selectedCustomer
            ? { Id: selectedCustomer.id, Name: name, Amount: parsedAmount }
            : { Name: name, Amount: parsedAmount };

        axios.post<Response<number>>(endpoint, payload)
            .then(() => {
                setErrorMessages([]);
                resetForm();
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not save the customer.']);
            });
    };

    const removeCustomer = (customerId: number) => {
        axios.post(`/api/onetoonebidirectional/remove/${customerId}`)
            .then(() => {
                setErrorMessages([]);
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not remove the customer.']);
            });
    };

    return (
        <Container fluid className='mt-4 px-4'>
            <Row className='mb-3'>
                <Col>
                    <h2>Customer Manager</h2>
                </Col>
            </Row>

            <Row className='mb-3'>
                <Col>
                    <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
                </Col>
            </Row>

            <Row className='mb-4'>
                <Col>
                    <Form>
                        <Form.Group as={Row} className='align-items-center mb-2' controlId='customerName'>
                            <Form.Label column xs={2} className='fw-semibold'>Name</Form.Label>
                            <Col xs={10}>
                                <Form.Control
                                    type='text'
                                    placeholder='Enter customer name'
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} className='align-items-center' controlId='customerAmount'>
                            <Form.Label column xs={2} className='fw-semibold'>Amount</Form.Label>
                            <Col xs={10}>
                                <div className='d-flex gap-2'>
                                    <Form.Control
                                        type='number'
                                        step='0.01'
                                        min='0'
                                        placeholder='Enter amount'
                                        value={amount}
                                        onChange={(e) => setAmount(e.target.value)}
                                    />
                                    <Button variant='primary' onClick={saveCustomer}>
                                        {selectedCustomer ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedCustomer && (
                                        <Button variant='secondary' onClick={resetForm}>
                                            Cancel
                                        </Button>
                                    )}
                                </div>
                            </Col>
                        </Form.Group>
                    </Form>
                </Col>
            </Row>

            <Row>
                <Col>
                    <Table striped bordered hover responsive>
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Amount</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {customers.length === 0 ? (
                                <tr>
                                    <td colSpan={4} className='text-center text-muted'>No customers</td>
                                </tr>
                            ) : (
                                customers.map(customer => (
                                    <tr key={customer.id}>
                                        <td>{customer.id}</td>
                                        <td>{customer.name}</td>
                                        <td>{customer.amount}</td>
                                        <td>
                                            <Button
                                                variant='warning'
                                                size='sm'
                                                className='me-2'
                                                onClick={() => {
                                                    setSelectedCustomer(customer);
                                                    setName(customer.name);
                                                    setAmount(String(customer.amount));
                                                }}
                                            >
                                                Edit
                                            </Button>
                                            <Button
                                                variant='danger'
                                                size='sm'
                                                onClick={() => removeCustomer(customer.id)}
                                            >
                                                Delete
                                            </Button>
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>
    );
}

export default CustomerManager;
