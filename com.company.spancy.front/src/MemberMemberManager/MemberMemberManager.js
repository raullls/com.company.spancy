import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function MemberMemberManager() {
    const [memberName, setMemberName] = useState('');
    const [members, setMembers] = useState([]);
    const [memberMembers, setMemberMembers] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [assignMemberId1, setAssignMemberId1] = useState('');
    const [assignMemberId2, setAssignMemberId2] = useState('');
    const [errorMessages, setErrorMessages] = useState([]);
    const [selectedMember, setSelectedMember] = useState(null);

    // ------ Member actions ------
    const handleCreateOrEditMember = () => {
        const url = selectedMember
            ? "/manytomanyselfreference/member/edit"
            : "/manytomanyselfreference/member/create";

        const payload = selectedMember
            ? { Id: selectedMember.id, Name: memberName }
            : { Name: memberName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedMember(null);
                setMemberName("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteMember = id =>
        axios.post(`/manytomanyselfreference/member/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));

    // ------ MemberMember actions ------
    const handleAssign = () => {
        if (!assignMemberId1 || !assignMemberId2) return;

        const member1 = members.find(m => m.id === parseInt(assignMemberId1, 10));
        const member2 = members.find(g => g.id === parseInt(assignMemberId2, 10));

        if (!member1 || !member2) return;

        axios.post("/manytomanyselfreference/membermember/isPresent", {
            MemberId1: { Id: member1.id, Name: member1.name },
            MemberId2: { Id: member2.id, Name: member2.name }
        }).then(response => {
            if (response.data?.data === true) {
                setErrorMessages([`Member ${member1.name} is already assigned to member ${member2.name}.`]);
                setAssignMemberId1("");
                setAssignMemberId2("");
                return;
            }

            return axios.post("/manytomanyselfreference/membermember/create", {
                MemberId1: { Id: member1.id, Name: member1.name },
                MemberId2: { Id: member2.id, Name: member2.name }
            })
        })
            .then(createRes => {
                if (createRes) {
                    setErrorMessages([]);
                    setAssignMemberId1("");
                    setAssignMemberId2("");
                    setRefresh(prev => !prev);
                }
            })
            .catch(error => {
                setErrorMessages(error.response?.data?.errors || []);
            });
    };

    const handleDeleteAssignment = (memberId1, memberId2) => {
        if (!memberId1 || !memberId2) return;

        const member1 = members.find(m => m.id === parseInt(memberId1, 10));
        const member2 = members.find(m => m.id === parseInt(memberId2, 10));

        if (!member1 || !member2) return;

        axios.post("/manytomanyselfreference/membermember/remove", {
            MemberId1: { Id: member1.id, Name: member1.name },
            MemberId2: { Id: member2.id, Name: member2.name }
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
        axios.get("/manytomanyselfreference/member/findAll")
            .then(response => setMembers(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanyselfreference/membermember/findAll")
            .then(response => setMemberMembers(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Member Member Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Member Section ------ */}
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Enter Member Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="memberName">
                            <Form.Label column sm={1} className="text-start">Name:</Form.Label>
                            <Col sm={11}>
                                <div className="d-flex gap-3">
                                    <Form.Control type="text" placeholder="Enter member name" value={memberName} onChange={(e) => setMemberName(e.target.value)} />
                                    <Button variant="primary" onClick={handleCreateOrEditMember}>
                                        {selectedMember ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedMember && (
                                        <Button variant="secondary" className="ms-2" onClick={() => { setSelectedMember(null); setMemberName(""); }}>Cancel</Button>
                                    )}
                                </div>
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Member Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {members.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No members</td>
                                </tr>
                            ) : members.map(m => (
                                <tr key={m.id}>
                                    <td>{m.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => { setSelectedMember(m); setMemberName(m.name); }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteMember(m.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            { /* ------ MemberMember Section ------ */}
            <Row className="mt-5">
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Assign Member to Member</h4>
                    <Form className="mb-4 d-flex align-items-center">
                        <Form.Select className="me-3" value={assignMemberId1} onChange={e => setAssignMemberId1(e.target.value)}>
                            <option value="">Select Member</option>
                            {members.map(m => (
                                <option key={m.id} value={m.id}>{m.name}</option>
                            ))}
                        </Form.Select>
                        <Form.Select className="me-3" value={assignMemberId2} onChange={e => setAssignMemberId2(e.target.value)}>
                            <option value="">Select Member</option>
                            {members.map(m => (
                                <option key={m.id} value={m.id}>{m.name}</option>
                            ))}
                        </Form.Select>
                        <Button variant="primary" onClick={handleAssign}>Assign</Button>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Member Name</th>
                                <th>Member Name</th>
                                <th style={{ width: '150px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {memberMembers.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No assignments</td>
                                </tr>
                            ) : (
                                memberMembers.map(mm => (
                                    <tr key={`${mm.memberId1.id}-${mm.memberId2.id}`}>
                                        <td>{mm.memberId1.name}</td>
                                        <td>{mm.memberId2.name}</td>
                                        <td><Button variant="danger" size="sm" onClick={() => handleDeleteAssignment(mm.memberId1.id, mm.memberId2.id)}>Delete</Button></td>
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

export default MemberMemberManager;