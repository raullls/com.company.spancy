import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function BookManager() {
    const [bookName, setBookName] = useState('');
    const [cityName, setCityName] = useState('');
    const [books, setBooks] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedBook, setSelectedBook] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handleCreateOrEditBook = () => {
        if (selectedBook) {
            axios.post("/onetooneunidirectional/edit", {
                Id: selectedBook.id,
                Name: bookName,
                City: cityName
            }).then(() => {
                setErrorMessages([]);
                setSelectedBook(null);
                setBookName("");
                setCityName("");
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (bookName && cityName) {
            axios.post("/onetooneunidirectional/create", {
                Name: bookName,
                City: cityName
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setBookName("");
                setCityName("");
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditBook = () => {
        setSelectedBook(null);
        setBookName("");
        setCityName("");
    };

    const handleDeleteBook = (bookId) => {
        axios.post(`/onetooneunidirectional/remove/${bookId}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    };

    useEffect(() => {
        axios.get("/onetooneunidirectional/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setBooks(response.data.data);
                }
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Book Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="bookName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter book name" value={bookName} onChange={(e) => setBookName(e.target.value)} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="bookCity">
                    <Form.Label column md={2} className="text-md-end">
                        City:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter city name" value={cityName} onChange={(e) => setCityName(e.target.value)} />
                    </Col>
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditBook}>
                            {selectedBook ? 'Edit' : 'Create'}
                        </Button>
                        {selectedBook && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditBook}>Cancel</Button>
                        )}
                    </Col>
                </Form.Group>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Book Name</th>
                                <th>Shipping City</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {books.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No books</td>
                                </tr>
                            ) : (
                                books.map((b, index) => {
                                    return (
                                        <tr key={b.id}>
                                            <td>{b.name}</td>
                                            <td>{b.city}</td>
                                            <td>
                                                <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                                    setSelectedBook(b);
                                                    setBookName(b.name);
                                                    setCityName(b.city);
                                                }}>Edit</Button>
                                                <Button variant="danger" size="sm" onClick={() => handleDeleteBook(b.id)}>Delete</Button>
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

export default BookManager;