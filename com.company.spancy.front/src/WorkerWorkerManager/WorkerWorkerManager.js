import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function WorkerWorkerManager() {
    const [workerName, setWorkerName] = useState('');
    const [workers, setWorkers] = useState([]);
    const [workerWorkers, setWorkerWorkers] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [assignWorkerId1, setAssignWorkerId1] = useState('');
    const [assignWorkerId2, setAssignWorkerId2] = useState('');
    const [relationshipType, setRelationshipType] = useState('');
    const [errorMessages, setErrorMessages] = useState([]);
    const [selectedWorker, setSelectedWorker] = useState(null);

    // ------ Worker actions ------
    const handleCreateOrEditWorker = () => {
        const url = selectedWorker
            ? "/manytomanyselfreferencewithjoinattribute/worker/edit"
            : "/manytomanyselfreferencewithjoinattribute/worker/create";

        const payload = selectedWorker
            ? { Id: selectedWorker.id, Name: workerName }
            : { Name: workerName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedWorker(null);
                setWorkerName("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteWorker = id =>
        axios.post(`/manytomanyselfreferencewithjoinattribute/worker/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));

    // ------ WorkerWorker actions ------
    const handleAssign = () => {
        if (!assignWorkerId1 || !assignWorkerId2) return;

        const worker1 = workers.find(m => m.id === parseInt(assignWorkerId1, 10));
        const worker2 = workers.find(g => g.id === parseInt(assignWorkerId2, 10));

        if (!worker1 || !worker2) return;

        axios.post("/manytomanyselfreferencewithjoinattribute/workerworker/isPresent", {
            WorkerId1: { Id: worker1.id, Name: worker1.name },
            WorkerId2: { Id: worker2.id, Name: worker2.name },
            RelationshipType: relationshipType
        }).then(response => {
            if (response.data?.data === true) {
                setErrorMessages([`Worker ${worker1.name} is already assigned to worker ${worker2.name}.`]);
                setAssignWorkerId1("");
                setAssignWorkerId2("");
                setRelationshipType("");
                return;
            }

            return axios.post("/manytomanyselfreferencewithjoinattribute/workerworker/create", {
                WorkerId1: { Id: worker1.id, Name: worker1.name },
                WorkerId2: { Id: worker2.id, Name: worker2.name },
                RelationshipType: relationshipType
            })
        })
            .then(createRes => {
                if (createRes) {
                    setErrorMessages([]);
                    setAssignWorkerId1("");
                    setAssignWorkerId2("");
                    setRelationshipType("");
                    setRefresh(prev => !prev);
                }
            })
            .catch(error => {
                setErrorMessages(error.response?.data?.errors || []);
            });
    };

    const handleDeleteAssignment = (workerId1, workerId2, relationshipType) => {
        if (!workerId1 || !workerId2 || !relationshipType) return;

        const worker1 = workers.find(m => m.id === parseInt(workerId1, 10));
        const worker2 = workers.find(m => m.id === parseInt(workerId2, 10));

        if (!worker1 || !worker2) return;

        axios.post("/manytomanyselfreferencewithjoinattribute/workerworker/remove", {
            WorkerId1: { Id: worker1.id, Name: worker1.name },
            WorkerId2: { Id: worker2.id, Name: worker2.name },
            RelationshipType: relationshipType
        })
            .then(() => {
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response?.data?.errors || []);
            });
    };

    // ------ Data fetch ------
    useEffect(() => {
        axios.get("/manytomanyselfreferencewithjoinattribute/worker/findAll")
            .then(response => setWorkers(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanyselfreferencewithjoinattribute/workerworker/findAll")
            .then(response => setWorkerWorkers(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Worker Worker Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Worker Section ------ */}
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Enter Worker Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="workerName">
                            <Form.Label column sm={3} className="text-start">Name:</Form.Label>
                            <Col sm={9}>
                                <div className="d-flex gap-2">
                                    <Form.Control type="text" placeholder="Enter worker name" value={workerName} onChange={(e) => setWorkerName(e.target.value)} />
                                    <Button variant="primary" onClick={handleCreateOrEditWorker}>
                                        {selectedWorker ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedWorker && (
                                        <Button variant="secondary" onClick={() => { setSelectedWorker(null); setWorkerName(""); }}>Cancel</Button>
                                    )}
                                </div>
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Worker Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {workers.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No workers</td>
                                </tr>
                            ) : workers.map(m => (
                                <tr key={m.id}>
                                    <td>{m.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => { setSelectedWorker(m); setWorkerName(m.name); }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteWorker(m.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            { /* ------ WorkerWorker Section ------ */}
            <Row className="mt-5">
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Assign Worker to Worker</h4>
                    <Form className="mb-4">
                        {/* Worker 1 */}
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="worker1">
                            <Form.Label column sm={3} className="text-start">
                                Worker 1:
                            </Form.Label>
                            <Col sm={9}>
                                <Form.Select
                                    value={assignWorkerId1}
                                    onChange={e => setAssignWorkerId1(e.target.value)}
                                >
                                    <option value="">Select Worker</option>
                                    {workers.map(w => (
                                        <option key={w.id} value={w.id}>{w.name}</option>
                                    ))}
                                </Form.Select>
                            </Col>
                        </Form.Group>
                        {/* Worker 2 */}
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="worker2">
                            <Form.Label column sm={3} className="text-start">
                                Worker 2:
                            </Form.Label>
                            <Col sm={9}>
                                <Form.Select
                                    value={assignWorkerId2}
                                    onChange={e => setAssignWorkerId2(e.target.value)}
                                >
                                    <option value="">Select Worker</option>
                                    {workers.map(w => (
                                        <option key={w.id} value={w.id}>{w.name}</option>
                                    ))}
                                </Form.Select>
                            </Col>
                        </Form.Group>
                        {/* Relationship Type + button */}
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="relationshipType">
                            <Form.Label column sm={3} className="text-start">
                                Relationship Type:
                            </Form.Label>
                            <Col sm={9}>
                                <div className="d-flex gap-2">
                                    <Form.Control
                                        type="text"
                                        placeholder="Enter relationship type"
                                        value={relationshipType}
                                        onChange={e => setRelationshipType(e.target.value)}
                                    />
                                    <Button variant="primary" onClick={handleAssign}>
                                        Assign
                                    </Button>
                                </div>
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Worker Name</th>
                                <th>Worker Name</th>
                                <th>Relationship</th>
                                <th style={{ width: '150px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {workerWorkers.length === 0 ? (
                                <tr>
                                    <td colSpan="4" className="text-center text-muted">No assignments</td>
                                </tr>
                            ) : (
                                workerWorkers.map(mm => (
                                    <tr key={`${mm.workerId1.id}-${mm.workerId2.id}-${mm.relationshipType}`}>
                                        <td>{mm.workerId1.name}</td>
                                        <td>{mm.workerId2.name}</td>
                                        <td>{mm.relationshipType}</td>
                                        <td><Button variant="danger" size="sm" onClick={() => handleDeleteAssignment(mm.workerId1.id, mm.workerId2.id, mm.relationshipType)}>Delete</Button></td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>
    );
};

export default WorkerWorkerManager;