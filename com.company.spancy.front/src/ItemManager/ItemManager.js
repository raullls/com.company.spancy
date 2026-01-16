import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function ItemManager() {
    const [itemName, setItemName] = useState('');
    const [featureList, setFeatureList] = useState(['']);
    const [items, setItems] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedItem, setSelectedItem] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handleFeatureChange = (index, value) => {
        const newFeatures = [...featureList];
        newFeatures[index] = value;
        setFeatureList(newFeatures);
    };

    const handleAddFeature = () => {
        setFeatureList([...featureList, '']);
    };

    const handleRemoveFeature = (index) => {
        if (featureList.length > 1) {
            const newFeatures = featureList.filter((_, i) => i !== index);
            setFeatureList(newFeatures);
        }
    };

    const handleCreateOrEditItem = () => {
        if (selectedItem) {
            axios.post("/onetomanybidirectional/edit", {
                Id: selectedItem.id,
                Name: itemName,
                featureList: featureList
            }).then(() => {
                setErrorMessages([]);
                setSelectedItem(null);
                setItemName("");
                setFeatureList([""]);
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (itemName) {
            axios.post("/onetomanybidirectional/create", {
                Name: itemName,
                FeatureList: featureList
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setItemName("");
                setFeatureList([""]);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditItem = () => {
        setSelectedItem(null);
        setItemName("");
        setFeatureList([""]);
    };

    const handleDeleteItem = (itemId) => {
        axios.post(`/onetomanybidirectional/remove/${itemId}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    };

    useEffect(() => {
        axios.get("/onetomanybidirectional/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setItems(response.data.data);
                }
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Item Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="itemName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter item name" value={itemName} onChange={(e) => setItemName(e.target.value)} />
                    </Col>
                </Form.Group>
                {featureList.map((feature, index) => (
                    <Form.Group as={Row} className="mb-3" controlId={`feature-${index}`} key={index}>
                        {index === 0 && (
                            <Form.Label column md={2} className="text-md-end">
                                Feature:
                            </Form.Label>
                        )}
                        {index !== 0 && <Col md={2} />}
                        <Col md={6}>
                            <Form.Control type="text" placeholder={`Enter feature ${index + 1}`} value={feature} onChange={(e) => handleFeatureChange(index, e.target.value)} />
                        </Col>
                        <Col md="auto" className="d-flex align-items-center">
                            {index === 0 ? (
                                <Button variant="success" size="sm" onClick={handleAddFeature} className="me-2" style={{ width: "2.5rem" }}>+</Button>
                            ) : (
                                <Button variant="danger" size="sm" onClick={() => handleRemoveFeature(index)} style={{ width: "2.5rem" }}>-</Button>
                            )}
                        </Col>
                    </Form.Group>
                ))}
                <Row className="mb-3">
                    <Col md={2} />
                    <Col md={6} />
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditItem}>
                            {selectedItem ? 'Edit' : 'Create'}
                        </Button>
                        {selectedItem && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditItem}>Cancel</Button>
                        )}
                    </Col>
                </Row>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Item Name</th>
                                <th>Feature</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {items.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No items</td>
                                </tr>
                            ) : (
                                items.map((i, index) => {
                                    return (
                                        <tr key={i.id}>
                                            <td>{i.name}</td>
                                            <td>
                                                <Form.Select>
                                                    {(i.featureList || []).map((feature, i) => (
                                                        <option key={i}>{feature}</option>
                                                    ))}
                                                </Form.Select>
                                            </td>
                                            <td>
                                                <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                                    setSelectedItem(i);
                                                    setItemName(i.name);
                                                    setFeatureList(i.featureList ?? ['']);
                                                }}>Edit</Button>
                                                <Button variant="danger" size="sm" onClick={() => handleDeleteItem(i.id)}>Delete</Button>
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

export default ItemManager;