import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function ManuscriptAuthorManager() {
    const [manuscriptName, setManuscriptName] = useState('');
    const [authorName, setAuthorName] = useState('');
    const [publisherName, setPublisherName] = useState('');
    const [manuscripts, setManuscripts] = useState([]);
    const [authors, setAuthors] = useState([]);
    const [manuscriptAuthors, setManuscriptAuthors] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedManuscript, setSelectedManuscript] = useState(null);
    const [selectedAuthor, setSelectedAuthor] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);
    const [assignManuscriptId, setAssignManuscriptId] = useState('');
    const [assignAuthorId, setAssignAuthorId] = useState('');

    // ------ Author actions ------
    const handleCreateOrEditAuthor = () => {
        const url = selectedAuthor
            ? "/manytomanybidirectionalwithjoinattribute/author/edit"
            : "/manytomanybidirectionalwithjoinattribute/author/create";

        const payload = selectedAuthor
            ? { Id: selectedAuthor.id, Name: authorName }
            : { Name: authorName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedAuthor(null);
                setAuthorName("");
                setRefresh(prev => !prev);
            })
            .catch(err => {
                setErrorMessages(err.response?.data?.errors || []);
                setSelectedAuthor(null);
                setAuthorName("");
            });
    };

    const handleDeleteAuthor = id =>
        axios.post(`/manytomanybidirectionalwithjoinattribute/author/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));

    // ------ Manuscript actions ------
    const handleCreateOrEditManuscript = () => {
        const url = selectedManuscript
            ? "/manytomanybidirectionalwithjoinattribute/manuscript/edit"
            : "/manytomanybidirectionalwithjoinattribute/manuscript/create";

        const payload = selectedManuscript
            ? { Id: selectedManuscript.id, Name: manuscriptName }
            : { Name: manuscriptName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedManuscript(null);
                setManuscriptName("");
                setRefresh(prev => !prev);
            })
            .catch(err => {
                setErrorMessages(err.response?.data?.errors || []);
                setSelectedManuscript(null);
                setManuscriptName("");
            });
    };

    const handleDeleteManuscript = id => 
        axios.post(`/manytomanybidirectionalwithjoinattribute/manuscript/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

    // ------ ManuscriptAuthor actions ------
    const handleAssign = () => {
        if (!assignManuscriptId || !assignAuthorId) return;

        const manuscript = manuscripts.find(m => m.id === parseInt(assignManuscriptId, 10));
        const author = authors.find(a => a.id === parseInt(assignAuthorId, 10));

        if (!manuscript || !author) return;

        axios.post("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/isPresent", {
            ManuscriptDto: { Id: manuscript.id, Name: manuscript.name },
            AuthorDto: { Id: author.id, Name: author.name }
        }).then(response => {
            if (response.data?.data === true) {
                setErrorMessages([`Manuscript ${manuscript.name} is already assigned to author ${author.name}.`]);
                setAssignAuthorId("");
                setAssignManuscriptId("");
                setPublisherName("");
                return;
            }

            return axios.post("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/create", {
                ManuscriptDto: { Id: manuscript.id, Name: manuscript.name },
                AuthorDto: { Id: author.id, Name: author.name },
                Publisher: publisherName
            })
        })
        .then(createRes => {
            if (createRes) {
                setErrorMessages([]);
                setAssignAuthorId("");
                setAssignManuscriptId("");
                setPublisherName("");
                setRefresh(prev => !prev);
            }
        })
        .catch(error => {
            setErrorMessages(error.response?.data?.errors || []);
        });
    };

    const handleDeleteAssignment = (manuscriptId, authorId) => {
        if (!authorId || !manuscriptId) return;

        const author = authors.find(a => a.id === parseInt(authorId, 10));
        const manuscript = manuscripts.find(m => m.id === parseInt(manuscriptId, 10));

        if (!author || !manuscript) return;

        axios.post("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/remove", {
            AuthorDto: { Id: author.id, Name: author.name },
            ManuscriptDto: { Id: manuscript.id, Name: manuscript.name }
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
        axios.get("/manytomanybidirectionalwithjoinattribute/author/findAll")
            .then(response => setAuthors(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanybidirectionalwithjoinattribute/manuscript/findAll")
            .then(response => setManuscripts(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/findAll")
            .then(response => setManuscriptAuthors(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Manuscript Author Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Manuscript Section ------ */}
                <Col md={6}>
                    <h4 className="mb-3">Enter Manuscript Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3" controlId="manuscriptName">
                            <Form.Label column sm={3} className="text-sm-end">Name:</Form.Label>
                            <Col sm={6}>
                                <Form.Control type="text" placeholder="Enter manuscript name" value={manuscriptName} onChange={(e) => setManuscriptName(e.target.value)} />
                            </Col>
                            <Col sm="auto">
                                <Button variant="primary" onClick={handleCreateOrEditManuscript}>
                                    {selectedManuscript ? 'Edit' : 'Create'}
                                </Button>
                                {selectedManuscript && (
                                    <Button variant="secondary" className="ms-2" onClick={() => { setSelectedManuscript(null); setManuscriptName(""); }}>Cancel</Button>
                                )}
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Manuscript Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {manuscripts.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No manuscripts</td>
                                </tr>
                            ) : manuscripts.map( m => (
                                <tr key={m.id}>
                                    <td>{m.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => { setSelectedManuscript(m); setManuscriptName(m.name); }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteManuscript(m.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
                { /* ------ Author Section ------ */}
                <Col md={6}>
                    <h4 className="mb-3">Enter Author Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3" controlId="authorName">
                            <Form.Label column sm={3} className="text-sm-end">Name:</Form.Label>
                            <Col sm={6}>
                                <Form.Control type="text" placeholder="Enter author name" value={authorName} onChange={e => setAuthorName(e.target.value)}/>
                            </Col>
                            <Col sm="auto">
                                <Button variant="primary" onClick={handleCreateOrEditAuthor}>
                                    {selectedAuthor ? "Edit" : "Create"}
                                </Button>
                                {selectedAuthor && (
                                    <Button variant="secondary" className="ms-2" onClick={() => { setSelectedAuthor(null); setAuthorName(""); }}>Cancel</Button>
                                )}
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Author Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {authors.length === 0 ? (
                                <tr><td colSpan="2" className="text-center text-muted">No authors</td></tr>
                            ) : authors.map(a => (
                                <tr key={a.id}>
                                    <td>{a.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2"
                                            onClick={() => { setSelectedAuthor(a); setAuthorName(a.name); }}>
                                            Edit
                                        </Button>
                                        <Button variant="danger" size="sm"
                                            onClick={() => handleDeleteAuthor(a.id)}>
                                            Delete
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            { /* ------ ManuscriptAuthors Section ------ */}
            <Row className="mt-5">
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Assign Author to Manuscript</h4>
                    <Form className="mb-4">
                        <Row className="mb-3">
                            <Col>
                                <Form.Select value={assignManuscriptId} onChange={e => setAssignManuscriptId(e.target.value)}>
                                    <option value="">Select Manuscript</option>
                                    {manuscripts.map(m => (
                                        <option key={m.id} value={m.id}>{m.name}</option>
                                    ))}
                                </Form.Select>
                            </Col>
                        </Row>
                        <Row className="mb-3">
                            <Col>
                                <Form.Select value={assignAuthorId} onChange={e => setAssignAuthorId(e.target.value)}>
                                    <option value="">Select Author</option>
                                    {authors.map(a => (
                                        <option key={a.id} value={a.id}>{a.name}</option>
                                    ))}
                                </Form.Select>
                            </Col>
                        </Row>
                        <Row className="align-items-center mb-3">
                            <Col>
                                <Form.Control type="text" placeholder="Enter Publisher Name" value={publisherName} onChange={e => setPublisherName(e.target.value)}/>
                            </Col>
                            <Col xs="auto">
                                <Button variant="primary" onClick={handleAssign}>Assign</Button>
                            </Col>
                        </Row>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Manuscript Name</th>
                                <th>Author Name</th>
                                <th>Publisher Name</th>
                                <th style={{ width: '150px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {manuscriptAuthors.length === 0 ? (
                                <tr>
                                    <td colSpan="4" className="text-center text-muted">No assignments</td>
                                </tr>
                            ) : (
                                manuscriptAuthors.map(ma => (
                                    <tr key={`${ma.manuscriptDto.id}-${ma.authorDto.id}`}>
                                        <td>{ma.manuscriptDto.name}</td>
                                        <td>{ma.authorDto.name}</td>
                                        <td>{ma.publisher || '-'}</td>
                                        <td><Button variant="danger" size="sm" onClick={() => handleDeleteAssignment(ma.manuscriptDto.id, ma.authorDto.id)}>Delete</Button></td>
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

export default ManuscriptAuthorManager;
