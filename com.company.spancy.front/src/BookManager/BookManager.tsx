import axios from 'axios';
import { useEffect, useState } from 'react';
import { Button, Col, Container, Form, Row, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';
import type { Response } from '../Shared/Response';
import type { Book } from './Book';

function BookManager() {
    const [books, setBooks] = useState<Book[]>([]);
    const [name, setName] = useState('');
    const [city, setCity] = useState('');
    const [selectedBook, setSelectedBook] = useState<Book | null>(null);
    const [errorMessages, setErrorMessages] = useState<string[]>([]);
    const [refreshToken, setRefreshToken] = useState(0);

    useEffect(() => {
        axios.get<Response<Book[]>>('/api/onetooneunidirectional/findAll')
            .then(response => {
                const payload = response.data?.data;
                setBooks(Array.isArray(payload) ? payload : []);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not load books.']);
            });
    }, [refreshToken]);

    const resetForm = () => {
        setSelectedBook(null);
        setName('');
        setCity('');
    };

    const saveBook = () => {
        if (!name.trim() || !city.trim()) {
            setErrorMessages(['Name and city are required.']);
            return;
        }

        const endpoint = selectedBook ? '/api/onetooneunidirectional/edit' : '/api/onetooneunidirectional/create';
        const payload = selectedBook
            ? { Id: selectedBook.id, Name: name, City: city }
            : { Name: name, City: city };

        axios.post<Response<number>>(endpoint, payload)
            .then(() => {
                setErrorMessages([]);
                resetForm();
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not save the book.']);
            });
    };

    const removeBook = (bookId: number) => {
        axios.post(`/api/onetooneunidirectional/remove/${bookId}`)
            .then(() => {
                setErrorMessages([]);
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not remove the book.']);
            });
    };

    return (
        <Container fluid className='mt-4 px-4'>
            <Row className='mb-3'>
                <Col>
                    <h2>Book Manager</h2>
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
                        <Form.Group as={Row} className='align-items-center mb-2' controlId='bookName'>
                            <Form.Label column xs={2} className='fw-semibold'>Name</Form.Label>
                            <Col xs={10}>
                                <Form.Control
                                    type='text'
                                    placeholder='Enter book name'
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} className='align-items-center' controlId='bookCity'>
                            <Form.Label column xs={2} className='fw-semibold'>City</Form.Label>
                            <Col xs={10}>
                                <div className='d-flex gap-2'>
                                    <Form.Control
                                        type='text'
                                        placeholder='Enter city'
                                        value={city}
                                        onChange={(e) => setCity(e.target.value)}
                                    />
                                    <Button variant='primary' onClick={saveBook}>
                                        {selectedBook ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedBook && (
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
                                <th>City</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {books.length === 0 ? (
                                <tr>
                                    <td colSpan={4} className='text-center text-muted'>No books</td>
                                </tr>
                            ) : (
                                books.map(book => (
                                    <tr key={book.id}>
                                        <td>{book.id}</td>
                                        <td>{book.name}</td>
                                        <td>{book.city}</td>
                                        <td>
                                            <Button
                                                variant='warning'
                                                size='sm'
                                                className='me-2'
                                                onClick={() => {
                                                    setSelectedBook(book);
                                                    setName(book.name);
                                                    setCity(book.city);
                                                }}
                                            >
                                                Edit
                                            </Button>
                                            <Button
                                                variant='danger'
                                                size='sm'
                                                onClick={() => removeBook(book.id)}
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

export default BookManager;
