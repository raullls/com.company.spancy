import axios from 'axios';
import { useEffect, useState } from 'react';
import { Badge, Button, Col, Container, Form, Row, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';
import type { Response } from '../Shared/Response';
import type { Person } from './Person';

function PersonManager() {
    const [persons, setPersons] = useState<Person[]>([]);
    const [name, setName] = useState('');
    const [numberInput, setNumberInput] = useState('');
    const [numbers, setNumbers] = useState<string[]>([]);
    const [selectedPerson, setSelectedPerson] = useState<Person | null>(null);
    const [errorMessages, setErrorMessages] = useState<string[]>([]);
    const [refreshToken, setRefreshToken] = useState(0);

    useEffect(() => {
        axios.get<Response<Person[]>>('/api/onetomanyunidirectional/findAll')
            .then(response => {
                const payload = response.data?.data;
                setPersons(Array.isArray(payload) ? payload : []);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not load persons.']);
            });
    }, [refreshToken]);

    const resetForm = () => {
        setSelectedPerson(null);
        setName('');
        setNumberInput('');
        setNumbers([]);
    };

    const addNumber = () => {
        const trimmed = numberInput.trim();
        if (trimmed && !numbers.includes(trimmed)) {
            setNumbers(prev => [...prev, trimmed]);
            setNumberInput('');
        }
    };

    const removeNumber = (index: number) => {
        setNumbers(prev => prev.filter((_, i) => i !== index));
    };

    const savePerson = () => {
        if (!name.trim()) {
            setErrorMessages(['Name is required.']);
            return;
        }

        const endpoint = selectedPerson
            ? '/api/onetomanyunidirectional/edit'
            : '/api/onetomanyunidirectional/create';

        const payload = selectedPerson
            ? { Id: selectedPerson.id, Name: name.trim(), Numbers: numbers }
            : { Name: name.trim(), Numbers: numbers };

        axios.post<Response<number>>(endpoint, payload)
            .then(() => {
                setErrorMessages([]);
                resetForm();
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not save the person.']);
            });
    };

    const removePerson = (personId: number) => {
        axios.post(`/api/onetomanyunidirectional/remove/${personId}`)
            .then(() => {
                setErrorMessages([]);
                if (selectedPerson && selectedPerson.id === personId) {
                    resetForm();
                }
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not remove the person.']);
            });
    };

    return (
        <Container fluid className='mt-4 px-4'>
            <Row className='mb-3'>
                <Col>
                    <h2>Person Manager</h2>
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
                        <Form.Group as={Row} className='align-items-center mb-2' controlId='personName'>
                            <Form.Label column xs='auto' className='fw-semibold text-start' style={{ minWidth: '140px' }}>Name</Form.Label>
                            <Col>
                                <Form.Control
                                    type='text'
                                    placeholder='Enter person name'
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} className='align-items-center mb-2' controlId='personNumber'>
                            <Form.Label column xs='auto' className='fw-semibold text-nowrap text-start' style={{ minWidth: '140px' }}>Phone Numbers</Form.Label>
                            <Col>
                                <div className='d-flex gap-2'>
                                    <Form.Control
                                        type='text'
                                        placeholder='Enter phone number'
                                        value={numberInput}
                                        onChange={(e) => setNumberInput(e.target.value)}
                                        onKeyDown={(e) => {
                                            if (e.key === 'Enter') {
                                                e.preventDefault();
                                                addNumber();
                                            }
                                        }}
                                    />
                                    <Button variant='outline-primary' onClick={addNumber}>
                                        Add
                                    </Button>
                                </div>
                                {numbers.length > 0 && (
                                    <div className='mt-2 d-flex flex-wrap gap-2'>
                                        {numbers.map((num, idx) => (
                                            <Badge key={idx} bg='secondary' className='d-flex align-items-center gap-1 px-2 py-1'>
                                                {num}
                                                <span
                                                    role='button'
                                                    style={{ cursor: 'pointer', marginLeft: '4px' }}
                                                    onClick={() => removeNumber(idx)}
                                                >
                                                    &times;
                                                </span>
                                            </Badge>
                                        ))}
                                    </div>
                                )}
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} className='align-items-center' controlId='personActions'>
                            <Form.Label column xs='auto' style={{ minWidth: '140px' }}></Form.Label>
                            <Col>
                                <div className='d-flex gap-2 justify-content-end'>
                                    {selectedPerson && (
                                        <Button variant='secondary' onClick={resetForm}>
                                            Cancel
                                        </Button>
                                    )}
                                    <Button variant='primary' onClick={savePerson}>
                                        {selectedPerson ? 'Edit' : 'Create'}
                                    </Button>
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
                                <th>Phone Numbers</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {persons.length === 0 ? (
                                <tr>
                                    <td colSpan={4} className='text-center text-muted'>No persons</td>
                                </tr>
                            ) : (
                                persons.map(person => (
                                    <tr key={person.id}>
                                        <td>{person.id}</td>
                                        <td>{person.name}</td>
                                        <td>{person.numbers?.join(', ') || ''}</td>
                                        <td>
                                            <Button
                                                variant='warning'
                                                size='sm'
                                                className='me-2'
                                                onClick={() => {
                                                    setSelectedPerson(person);
                                                    setName(person.name);
                                                    setNumbers(person.numbers || []);
                                                    setNumberInput('');
                                                }}
                                            >
                                                Edit
                                            </Button>
                                            <Button
                                                variant='danger'
                                                size='sm'
                                                onClick={() => removePerson(person.id)}
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

export default PersonManager;