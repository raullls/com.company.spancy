import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function ClientAccountManager() {
    const [clientName, setClientName] = useState('');
    const [accountNumber, setAccountNumber] = useState('');
    const [clients, setClients] = useState([]);
    const [accounts, setAccounts] = useState([]);
    const [clientAccounts, setClientAccounts] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [selectedClient, setSelectedClient] = useState(null);
    const [selectedAccount, setSelectedAccount] = useState(null);
    const [errorMessages, setErrorMessages] = useState([]);
    const [assignClientId, setAssignClientId] = useState('');
    const [assignAccountId, setAssignAccountId] = useState('');

    // ------ Client actions ------
    const handleCreateOrEditClient = () => {
        const url = selectedClient
            ? "/manytomanybidirectional/client/edit"
            : "/manytomanybidirectional/client/create";

        const payload = selectedClient
            ? { Id: selectedClient.id, Name: clientName }
            : { Name: clientName };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedClient(null);
                setClientName("");
                setRefresh(prev => !prev);
            })
            .catch(err => {
                setErrorMessages(err.response?.data?.errors || []);
                setSelectedClient(null);
                setClientName("");
            });
    };

    const handleDeleteClient = id =>
        axios.post(`/manytomanybidirectional/client/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));

    // ------ Account actions ------
    const handleCreateOrEditAccount = () => {
        const url = selectedAccount
            ? "/manytomanybidirectional/account/edit"
            : "/manytomanybidirectional/account/create";

        const payload = selectedAccount
            ? { Id: selectedAccount.id, Number: accountNumber }
            : { Number: accountNumber };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedAccount(null);
                setAccountNumber("");
                setRefresh(prev => !prev);
            })
            .catch(err => {
                setErrorMessages(err.response?.data?.errors || []);
                setSelectedAccount(null);
                setAccountNumber("");
            });
    };

    const handleDeleteAccount = id =>
        axios.post(`/manytomanybidirectional/account/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

    // ------ ClientAccount actions ------
    const handleAssign = () => {
        if (!assignClientId || !assignAccountId) return;

        const client = clients.find(c => c.id === parseInt(assignClientId, 10));
        const account = accounts.find(a => a.id === parseInt(assignAccountId, 10));

        if (!client || !account) return;

        axios.post("/manytomanybidirectional/clientaccount/isPresent", {
            ClientDto: { Id: client.id, Name: client.name },
            AccountDto: { Id: account.id, Number: account.number }
        }).then(response => {
            if (response.data?.data === true) {
                setErrorMessages([`Client ${client.name} is already assigned to account ${account.name}.`]);
                return;
            }

            return axios.post("/manytomanybidirectional/clientaccount/create", {
                ClientDto: { Id: client.id, Name: client.name },
                AccountDto: { Id: account.id, Number: account.number }
            })
        })
        .then(createRes => {
            if (createRes) {
                setErrorMessages([]);
                setAssignClientId("");
                setAssignAccountId("");
                setRefresh(prev => !prev);
            }
        })
        .catch(error => {
            setErrorMessages(error.response?.data?.errors || []);
        });
    };

    const handleDeleteAssignment = (clientId, accountId) => {
        if (!clientId || !accountId) return;

        const client = clients.find(c => c.id === parseInt(clientId, 10));
        const account = accounts.find(a => a.id === parseInt(accountId, 10));

        if (!client || !account) return;

        axios.post("/manytomanybidirectional/clientaccount/remove", {
            ClientDto: { Id: client.id, Name: client.name },
            AccountDto: { Id: account.id, Name: account.number }
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
        axios.get("/manytomanybidirectional/client/findAll")
            .then(response => setClients(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanybidirectional/account/findAll")
            .then(response => setAccounts(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));

        axios.get("/manytomanybidirectional/clientaccount/findAll")
            .then(response => setClientAccounts(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Client Account Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Client Section ------ */}
                <Col md={6}>
                    <h4 className="mb-3">Enter Client Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3" controlId="clientName">
                            <Form.Label column sm={3} className="text-sm-end">Name:</Form.Label>
                            <Col sm={6}>
                                <Form.Control type="text" placeholder="Enter client name" value={clientName} onChange={(e) => setClientName(e.target.value)} />
                            </Col>
                            <Col sm="auto">
                                <Button variant="primary" onClick={handleCreateOrEditClient}>
                                    {selectedClient ? 'Edit' : 'Create'}
                                </Button>
                                {selectedClient && (
                                    <Button variant="secondary" className="ms-2" onClick={() => { setSelectedClient(null); setClientName(""); }}>Cancel</Button>
                                )}
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Client Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {clients.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No clients</td>
                                </tr>
                            ) : clients.map(c => (
                                <tr key={c.id}>
                                    <td>{c.name}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => { setSelectedClient(c); setClientName(c.name); }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteClient(c.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
                { /* ------ Account Section ------ */}
                <Col md={6}>
                    <h4 className="mb-3">Enter Account Name</h4>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3" controlId="accountName">
                            <Form.Label column sm={3} className="text-sm-end">Name:</Form.Label>
                            <Col sm={6}>
                                <Form.Control type="text" placeholder="Enter account name" value={accountNumber} onChange={e => setAccountNumber(e.target.value)} />
                            </Col>
                            <Col sm="auto">
                                <Button variant="primary" onClick={handleCreateOrEditAccount}>
                                    {selectedAccount ? "Edit" : "Create"}
                                </Button>
                                {selectedAccount && (
                                    <Button variant="secondary" className="ms-2" onClick={() => { setSelectedAccount(null); setAccountNumber(""); }}>Cancel</Button>
                                )}
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Account Name</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {accounts.length === 0 ? (
                                <tr><td colSpan="2" className="text-center text-muted">No accounts</td></tr>
                            ) : accounts.map(a => (
                                <tr key={a.id}>
                                    <td>{a.number}</td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2"
                                            onClick={() => { setSelectedAccount(a); setAccountNumber(a.number); }}>
                                            Edit
                                        </Button>
                                        <Button variant="danger" size="sm"
                                            onClick={() => handleDeleteAccount(a.id)}>
                                            Delete
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            { /* ------ ClientAccount Section ------ */}
            <Row className="mt-5">
                <Col md={{ span: 8, offset: 2 }}>
                    <h4 className="mb-3">Assign Clients to Accounts</h4>
                    <Form className="mb-4 d-flex align-items-center">
                        <Form.Select className="me-3" value={assignClientId} onChange={e => setAssignClientId(e.target.value)}>
                            <option value="">Select Client</option>
                            {clients.map(c => (
                                <option key={c.id} value={c.id}>{c.name}</option>
                            ))}
                        </Form.Select>
                        <Form.Select className="me-3" value={assignAccountId} onChange={e => setAssignAccountId(e.target.value)}>
                            <option value="">Select Account</option>
                            {accounts.map(a => (
                                <option key={a.id} value={a.id}>{a.number}</option>
                            ))}
                        </Form.Select>
                        <Button variant="primary" onClick={handleAssign}>Assign</Button>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Client Name</th>
                                <th>Account Name</th>
                                <th style={{ width: '150px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {clientAccounts.length === 0 ? (
                                <tr>
                                    <td colSpan="3" className="text-center text-muted">No assignments</td>
                                </tr>
                            ) : (
                                clientAccounts.map(ca => (
                                    <tr key={`${ca.clientDto.id}-${ca.accountDto.id}`}>
                                        <td>{ca.clientDto.name}</td>
                                        <td>{ca.accountDto.number}</td>
                                        <td><Button variant="danger" size="sm" onClick={() => handleDeleteAssignment(ca.clientDto.id, ca.accountDto.id)}>Delete</Button></td>
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

export default ClientAccountManager;