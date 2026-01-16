import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function UserGroupManager() {
    const [userName, setUserName] = useState('');
    const [groupName, setGroupName] = useState('');
    const [users, setUsers] = useState([]);
    const [groups, setGroups] = useState([]);
    const [userGroups, setUserGroups] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);
    const [selectedGroup, setSelectedGroup] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);
    const [assignUserId, setAssignUserId] = useState('');
    const [assignGroupId, setAssignGroupId] = useState('');

    // ------ User actions ------
    const handleCreateOrEditUser = () => {
        const url = selectedUser
            ? "/manytomanyunidirectional/user/edit"
            : "/manytomanyunidirectional/user/create";

        const payload = selectedUser
            ? { Id: selectedUser.id, Name: userName }
            : { Name: userName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedUser(null);
                setUserName("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteUser = id =>
        axios.post(`/manytomanyunidirectional/user/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));

    // ------ Group actions ------
    const handleCreateOrEditGroup = () => {
        const url = selectedGroup
            ? "/manytomanyunidirectional/group/edit"
            : "/manytomanyunidirectional/group/create";

        const payload = selectedGroup
            ? { Id: selectedGroup.id, Name: groupName }
            : { Name: groupName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedGroup(null);
                setGroupName("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteGroup = id => 
        axios.post(`/manytomanyunidirectional/group/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

    // ------ UserGroup actions ------
    const handleAssign = () => {
        if (!assignUserId || !assignGroupId) return;

        const user = users.find(u => u.id === parseInt(assignUserId, 10));
        const group = groups.find(g => g.id === parseInt(assignGroupId, 10));

        if (!user || !group) return;

        axios.post("/manytomanyunidirectional/usergroup/isPresent", {
            UserDto: { Id: user.id, Name: user.name },
            GroupDto: { Id: group.id, Name: group.name }
        }).then(response => {
            if (response.data?.data === true) {
                setErrorMessages([`User ${user.name} is already assigned to group ${group.name}.`]);
                return;
            }

            return axios.post("/manytomanyunidirectional/usergroup/create", {
                UserDto: { Id: user.id, Name: user.name },
                GroupDto: { Id: group.id, Name: group.name }
            })
        })
        .then(createRes => {
            if (createRes) {
                setErrorMessages([]);
                setAssignUserId("");
                setAssignGroupId("");
                setRefresh(prev => !prev);
            }
        })
        .catch(error => {
            setErrorMessages(error.response?.data?.errors || []);
        });
    };

    const handleDeleteAssignment = (userId, groupId) => {
        if (!userId || !groupId) return;

        const user = users.find(u => u.id === parseInt(userId, 10));
        const group = groups.find(g => g.id === parseInt(groupId, 10));

        if (!user || !group) return;

        axios.post("/manytomanyunidirectional/usergroup/remove", {
            UserDto: { Id: user.id, Name: user.name },
            GroupDto: { Id: group.id, Name: group.name }
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
        axios.get("/manytomanyunidirectional/user/findAll")
            .then(response => setUsers(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanyunidirectional/group/findAll")
            .then(response => setGroups(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanyunidirectional/usergroup/findAll")
            .then(response => setUserGroups(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">User Group Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ User Section ------ */}
                <Col md={6}>
                    <h4 className="mb-3">Enter User Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3" controlId="userName">
                            <Form.Label column sm={3} className="text-sm-end">Name:</Form.Label>
                            <Col sm={6}>
                                <Form.Control type="text" placeholder="Enter user name" value={userName} onChange={(e) => setUserName(e.target.value)} />
                            </Col>
                            <Col sm="auto">
                                <Button variant="primary" onClick={handleCreateOrEditUser}>
                                    {selectedUser ? 'Edit' : 'Create'}
                                </Button>
                                {selectedUser && (
                                    <Button variant="secondary" className="ms-2" onClick={() => { setSelectedUser(null); setUserName(""); }}>Cancel</Button>
                                )}
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>User Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No users</td>
                                </tr>
                            ) : users.map( u => (
                                <tr key={u.id}>
                                    <td>{u.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => { setSelectedUser(u); setUserName(u.name); }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteUser(u.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
                { /* ------ Group Section ------ */}
                <Col md={6}>
                    <h4 className="mb-3">Enter Group Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3" controlId="groupName">
                            <Form.Label column sm={3} className="text-sm-end">Name:</Form.Label>
                            <Col sm={6}>
                                <Form.Control type="text" placeholder="Enter group name" value={groupName} onChange={e => setGroupName(e.target.value)}/>
                            </Col>
                            <Col sm="auto">
                                <Button variant="primary" onClick={handleCreateOrEditGroup}>
                                    {selectedGroup ? "Edit" : "Create"}
                                </Button>
                                {selectedGroup && (
                                    <Button variant="secondary" className="ms-2" onClick={() => { setSelectedGroup(null); setGroupName(""); }}>Cancel</Button>
                                )}
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Group Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {groups.length === 0 ? (
                                <tr><td colSpan="2" className="text-center text-muted">No groups</td></tr>
                            ) : groups.map(g => (
                                <tr key={g.id}>
                                    <td>{g.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2"
                                            onClick={() => { setSelectedGroup(g); setGroupName(g.name); }}>
                                            Edit
                                        </Button>
                                        <Button variant="danger" size="sm"
                                            onClick={() => handleDeleteGroup(g.id)}>
                                            Delete
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            { /* ------ UserGroup Section ------ */}
            <Row className="mt-5">
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Assign Users to Groups</h4>
                    <Form className="mb-4 d-flex align-items-center">
                        <Form.Select className="me-3" value={assignUserId} onChange={e => setAssignUserId(e.target.value)}>
                            <option value="">Select User</option>
                            {users.map(u => (
                                <option key={u.id} value={u.id}>{u.name}</option>
                            ))}
                        </Form.Select>
                        <Form.Select className="me-3" value={assignGroupId} onChange={e => setAssignGroupId(e.target.value)}>
                            <option value="">Select Group</option>
                            {groups.map(g => (
                                <option key={g.id} value={g.id}>{g.name}</option>
                            ))}
                        </Form.Select>
                        <Button variant="primary" onClick={handleAssign}>Assign</Button>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>User Name</th>
                                <th>Group Name</th>
                                <th style={{ width: '150px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {userGroups.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No assignments</td>
                                </tr>
                            ) : (
                                userGroups.map(ug => (
                                    <tr key={`${ug.userDto.id}-${ug.groupDto.id}`}>
                                        <td>{ug.userDto.name}</td>
                                        <td>{ug.groupDto.name}</td>
                                        <td><Button variant="danger" size="sm" onClick={() => handleDeleteAssignment(ug.userDto.id, ug.groupDto.id)}>Delete</Button></td>
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

export default UserGroupManager;