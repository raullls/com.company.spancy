import axios from 'axios';
import { useEffect, useState } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import type { Product } from './Product';
import AlertMessage from '../Shared/AlertMessage';

function ProductManager() {
    const [productName, setProductName] = useState('');
    const [products, setProducts] = useState<Product[]>([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<Product|null>(null);
    const [errorMessages, setErrorMessages] = useState<string[]>([]);

    const handleCreateOrEditProduct = () => {
        if (selectedProduct) {
            axios.post("/api/standalone/edit", {
                Id: selectedProduct.id,
                Name: productName
            }).then(() => {
                setSelectedProduct(null);
                setProductName("");
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (productName) {
            axios.post("/api/standalone/create", {
                Name: productName
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setProductName("");
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditProduct = () => {
        setSelectedProduct(null);
        setProductName("");
    };

    const handleDeleteProduct = (productId: number) => {
        axios.post(`/api/standalone/remove/${productId}`).then(() => {
            setRefresh(prev => !prev);
        }).catch(error => {
            console.error("Error fetching products:", error);
        });
    };

    useEffect(() => {
        axios.get("/api/standalone/findAll").then(response => {
            if (response.data && response.data.data) {
                setProducts(response.data.data);
            }
        }).catch(error => {
            console.error("Error fetching products", error);
        });
    }, [refresh]);

    return (
        <Container fluid className='mt-4 px-4'>
            <Row className='mb-3'>
                <Col>
                    <h2>Product Manager</h2>
                </Col>
            </Row>
            <Row className='mb-3'>
                <Col>
                    <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])}/>
                </Col>
            </Row>
            <Row className='mb-4'>
                <Col>
                    <Form>
                        <Form.Group as={Row} className='align-items-center' controlId='productName'>
                            <Form.Label column xs={2} className='fw-semibold'>Name</Form.Label>
                            <Col xs={10}>
                                <div className='d-flex gap-2'>
                                    <Form.Control
                                        type='text'
                                        placeholder='Enter product name'
                                        value={productName}
                                        onChange={(e) => setProductName(e.target.value)}
                                    />
                                    <Button
                                        variant='primary'
                                        onClick={handleCreateOrEditProduct}
                                    >
                                        {selectedProduct ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedProduct && (
                                        <Button
                                            variant='secondary'
                                            onClick={handleCancelEditProduct}
                                        >
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
                                <th style={{ width: '200px'}}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {products.length === 0 ? (
                                <tr>
                                    <td colSpan={3} className='text-center text-muted'>
                                        No products
                                    </td>
                                </tr>
                            ) : (
                                products.map((p: Product) => (
                                    <tr key={p.id}>
                                        <td>{p.id}</td>
                                        <td>{p.name}</td>
                                        <td>
                                            <Button
                                                variant='warning'
                                                size='sm'
                                                className='me-2'
                                                onClick={() => {
                                                    setSelectedProduct(p);
                                                    setProductName(p.name);
                                                }}
                                            >
                                                Edit
                                            </Button>
                                            <Button
                                                variant='danger'
                                                size='sm'
                                                onClick={() => handleDeleteProduct(p.id)}
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

export default ProductManager;