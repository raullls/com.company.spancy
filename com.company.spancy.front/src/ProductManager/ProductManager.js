import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function ProductManager() {
    const [productName, setProductName] = useState('');
    const [products, setProducts] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handleCreateOrEditProduct = () => {
        if (selectedProduct) {
            axios.post("/standalone/edit", {
                Id: selectedProduct.id,
                Name: productName
            }).then(() => {
                setSelectedProduct(null);
                setProductName("");
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (productName) {
            axios.post("/standalone/create", {
                Name: productName
            })
            .then(response => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setProductName('');
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditProduct = () => {
        setSelectedProduct(null);
        setProductName("");
    };

    const handleDeleteProduct = (productId) => {
        axios.post(`/standalone/remove/${productId}`)
            .then(response => {
                setRefresh(prev => !prev);
            })
            .catch(error => {
                console.error("Error deleting product:", error);
            });
    };

    useEffect(() => {
        axios.get("/standalone/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setProducts(response.data.data);
                }
            })
            .catch(error => {
                console.error("Error fetching products:", error);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Product Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="productName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter product name" value={productName} onChange={(e) => setProductName(e.target.value)} />
                    </Col>
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditProduct}>
                            {selectedProduct ? 'Edit' : 'Create'}
                        </Button>
                        {selectedProduct && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditProduct}>Cancel</Button>
                        )}
                    </Col>
                </Form.Group>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th style={{ width: '200px' }}>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {products.length === 0 ? (
                        <tr>
                            <td colSpan="3" className="text-center text-muted">No products</td>
                        </tr>
                    ) : (
                        products.map((p, index) => {
                            return (
                                <tr key={p.id}>
                                    <td>{p.id}</td>
                                    <td>{p.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                            setSelectedProduct(p);
                                            setProductName(p.name);
                                        }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteProduct(p.id)}>Delete</Button>
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

export default ProductManager;