import axios from 'axios';
import { useEffect, useState } from 'react';
import { Button, Col, Container, Form, Row, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';
import type { Response } from '../Shared/Response';
import type { Student } from './Student';

function StudentManager() {
    const [students, setStudents] = useState<Student[]>([]);
    const [name, setName] = useState('');
    const [mentorName, setMentorName] = useState('');
    const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
    const [errorMessages, setErrorMessages] = useState<string[]>([]);
    const [refreshToken, setRefreshToken] = useState(0);

    useEffect(() => {
        axios.get<Response<Student[]>>('/api/onetooneselfreference/findAll')
            .then(response => {
                const payload = response.data?.data;
                setStudents(Array.isArray(payload) ? payload : []);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not load students.']);
            });
    }, [refreshToken]);

    const resetForm = () => {
        setSelectedStudent(null);
        setName('');
        setMentorName('');
    };

    const saveStudent = () => {
        if (!name.trim() || !mentorName.trim()) {
            setErrorMessages(['Name and mentor name are required.']);
            return;
        }

        const endpoint = selectedStudent
            ? '/api/onetooneselfreference/edit'
            : '/api/onetooneselfreference/create';

        const payload = selectedStudent
            ? { Id: selectedStudent.id, Name: name.trim(), MentorName: mentorName.trim() }
            : { Name: name.trim(), MentorName: mentorName.trim() };

        axios.post<Response<number>>(endpoint, payload)
            .then(() => {
                setErrorMessages([]);
                resetForm();
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not save the student.']);
            });
    };

    const removeStudent = (studentId: number) => {
        axios.post(`/api/onetooneselfreference/remove/${studentId}`)
            .then(() => {
                setErrorMessages([]);
                if (selectedStudent && selectedStudent.id === studentId) {
                    resetForm();
                }
                setRefreshToken(prev => prev + 1);
            })
            .catch(error => {
                const apiErrors = error?.response?.data?.errors;
                setErrorMessages(Array.isArray(apiErrors) ? apiErrors : ['Could not remove the student.']);
            });
    };

    return (
        <Container fluid className='mt-4 px-4'>
            <Row className='mb-3'>
                <Col>
                    <h2>Student Manager</h2>
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
                        <Form.Group as={Row} className='align-items-center mb-2' controlId='studentName'>
                            <Form.Label column xs={3} className='fw-semibold text-start'>Name</Form.Label>
                            <Col xs={9}>
                                <Form.Control
                                    type='text'
                                    placeholder='Enter student name'
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} className='align-items-center' controlId='studentMentorName'>
                            <Form.Label column xs={3} className='fw-semibold text-nowrap text-start'>Mentor Name</Form.Label>
                            <Col xs={9}>
                                <div className='d-flex gap-2'>
                                    <Form.Control
                                        type='text'
                                        placeholder='Enter mentor name'
                                        value={mentorName}
                                        onChange={(e) => setMentorName(e.target.value)}
                                    />
                                    <Button variant='primary' onClick={saveStudent}>
                                        {selectedStudent ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedStudent && (
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
                                <th>Mentor Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {students.length === 0 ? (
                                <tr>
                                    <td colSpan={4} className='text-center text-muted'>No students</td>
                                </tr>
                            ) : (
                                students.map(student => (
                                    <tr key={student.id}>
                                        <td>{student.id}</td>
                                        <td>{student.name}</td>
                                        <td>{student.mentorName}</td>
                                        <td>
                                            <Button
                                                variant='warning'
                                                size='sm'
                                                className='me-2'
                                                onClick={() => {
                                                    setSelectedStudent(student);
                                                    setName(student.name);
                                                    setMentorName(student.mentorName);
                                                }}
                                            >
                                                Edit
                                            </Button>
                                            <Button
                                                variant='danger'
                                                size='sm'
                                                onClick={() => removeStudent(student.id)}
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

export default StudentManager;