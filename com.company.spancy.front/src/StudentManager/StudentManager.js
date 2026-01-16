import axios from 'axios';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function StudentManager() {
    const [studentName, setStudentName] = useState('');
    const [mentorName, setMentorName] = useState('');
    const [students, setStudents] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedStudent, setSelectedStudent] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);

    const handleCreateOrEditStudent = () => {
        if (selectedStudent) {
            axios.post("/onetooneselfreference/edit", {
                Id: selectedStudent.id,
                Name: studentName,
                MentorName: mentorName
            }).then(() => {
                setErrorMessages([]);
                setSelectedStudent(null);
                setStudentName("");
                setMentorName("");
                setRefresh(prev => !prev);
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        } else if (studentName) {
            axios.post("/onetooneselfreference/create", {
                Name: studentName,
                MentorName: mentorName
            }).then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
                setStudentName("");
                setMentorName("");
            }).catch(error => {
                setErrorMessages(error.response.data.errors);
            });
        }
    };

    const handleCancelEditStudent = () => {
        setSelectedStudent(null);
        setStudentName("");
        setMentorName("");
    };

    const handleDeleteStudent = (studentId) => {
        axios.post(`/onetooneselfreference/remove/${studentId}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    };

    useEffect(() => {
        axios.get("/onetooneselfreference/findAll")
            .then(response => {
                if (response.data && response.data.data) {
                    setStudents(response.data.data);
                }
            })
            .catch(error => {
                setErrorMessages(error.response.data.errors);
            });
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2>Student Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />
            <Form className="mb-4">
                <Form.Group as={Row} className="mb-3" controlId="studentName">
                    <Form.Label column md={2} className="text-md-end">
                        Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter student name" value={studentName} onChange={(e) => setStudentName(e.target.value)} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="mentorName">
                    <Form.Label column md={2} className="text-md-end">
                        Mentor Name:
                    </Form.Label>
                    <Col md={6}>
                        <Form.Control type="text" placeholder="Enter mentor name" value={mentorName} onChange={(e) => setMentorName(e.target.value)} />
                    </Col>
                    <Col md="auto">
                        <Button variant="primary" onClick={handleCreateOrEditStudent}>
                            {selectedStudent ? 'Edit' : 'Create'}
                        </Button>
                        {selectedStudent && (
                            <Button variant="secondary" className="ms-2" onClick={handleCancelEditStudent}>Cancel</Button>
                        )}
                    </Col>
                </Form.Group>
            </Form>
            <Row>
                <Col md={{ span: 8, offset: 2 }}>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Student Name</th>
                                <th>Mentor Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {students.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No students</td>
                                </tr>
                            ) : (
                                students.map((s, index) => {
                                    return (
                                        <tr key={s.id}>
                                            <td>{s.name}</td>
                                            <td>{s.mentorName}</td>
                                            <td>
                                                <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                                    setSelectedStudent(s);
                                                    setStudentName(s.name);
                                                    setMentorName(s.mentorName ?? '');
                                                }}>Edit</Button>
                                                <Button variant="danger" size="sm" onClick={() => handleDeleteStudent(s.id)}>Delete</Button>
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

export default StudentManager;