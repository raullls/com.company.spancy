import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function CategoryManager() {
    const [categoryName, setCategoryName] = useState('');
    const [parentId, setParentId] = useState(0);
    const [categories, setCategories] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedCategory, setSelectedCategory] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handleCreateOrEditCategory = () => {
        if (selectedCategory) {
            axios.post("/onetomanyselfreference/edit", {
                Id: selectedCategory.id,
                Name: categoryName,
                ParentId: parentId
            }).then(() => {
                setErrorMessages([]);
                setSelectedCategory(null);
                setCategoryName("");
                setParentId(0);
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (categoryName) {
            axios.post("/onetomanyselfreference/create", {
                Name: categoryName,
                ParentId: parentId
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setCategoryName("");
                setParentId(0);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditCategory = () => {
        setSelectedCategory(null);
        setCategoryName("");
        setParentId(0);
    };

    const handleDeleteCategory = (categoryId) => {
        axios.post(`/onetomanyselfreference/remove/${categoryId}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    };

    useEffect(() => {
        axios.get("/onetomanyselfreference/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setCategories(response.data.data);
                }
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Category Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="categoryName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter category name" value={categoryName} onChange={(e) => setCategoryName(e.target.value)} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="parentId">
                    <Form.Label column md={2} className="text-md-end">
                        Parent Id:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Select
                            value={parentId}
                            onChange={(e) => setParentId(e.target.value)}
                        >
                            <option value={0}>No Parent</option>
                            {categories
                                .filter(c => !selectedCategory || c.id !== selectedCategory.id)
                                .map(c => (
                                    <option key={c.id} value={c.id}>
                                        {c.name}
                                    </option>
                                ))}
                        </Form.Select>
                    </Col>
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditCategory}>
                            {selectedCategory ? 'Edit' : 'Create'}
                        </Button>
                        {selectedCategory && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditCategory}>Cancel</Button>
                        )}
                    </Col>
                </Form.Group>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Category Name</th>
                                <th>Parent Id</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No categories</td>
                                </tr>
                            ) : (
                                categories.map((c, index) => {
                                    return (
                                        <tr key={c.id}>
                                            <td>{c.name}</td>
                                            <td>{c.parentId}</td>
                                            <td>
                                                <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                                    setSelectedCategory(c);
                                                    setCategoryName(c.name);
                                                    setParentId(c.parentId);
                                                }}>Edit</Button>
                                                <Button variant="danger" size="sm" onClick={() => handleDeleteCategory(c.id)}>Delete</Button>
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

export default CategoryManager;