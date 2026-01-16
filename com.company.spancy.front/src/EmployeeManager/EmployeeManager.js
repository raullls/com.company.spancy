import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Table } from 'react-bootstrap';
import AlertMessage from '../Shared/AlertMessage';

function EmployeeManager() {
    const [employeeName, setEmployeeName] = useState('');
    const [employeeType, setEmployeeType] = useState('');
    const [leaves, setLeaves] = useState('');
    const [salary, setSalary] = useState('');
    const [hourleyRate, setHourleyRate] = useState('');
    const [overtimeRate, setOvertimeRate] = useState('');
    const [employees, setEmployees] = useState([]);
    const [refresh, setRefresh] = useState(false);
    const [errorMessages, setErrorMessages] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState(null);

    // ------ Employee actions ------
    const handleCreateOrEditEmployee = () => {
        const url = selectedEmployee
            ? "/classtableinheritance/edit"
            : "/classtableinheritance/create";

        const basePayload = {
            Name: employeeName,
            Type: employeeType
        };

        const typePayload =
            employeeType === 'permanentEmployee'
                ? { Leaves : leaves, Salary : salary }
                : employeeType === 'contractorEmployee'
                    ? { HourleyRate : hourleyRate, OvertimeRate : overtimeRate }
                    : {};

        const payload = selectedEmployee
            ? { Id: selectedEmployee.id, ...basePayload, ...typePayload }
            : { ...basePayload, ...typePayload };

        axios.post(url, payload)
            .then(() => {
                setErrorMessages([]);
                setSelectedEmployee(null);
                setEmployeeName("");
                setEmployeeType("");
                setLeaves("");
                setSalary("");
                setHourleyRate("");
                setOvertimeRate("");
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    };

    const handleDeleteEmployee = id =>
        axios.post(`/classtableinheritance/remove/${id}`)
            .then(() => {
                setErrorMessages([]);
                setRefresh(prev => !prev);
            })
            .catch(err => setErrorMessages(err.response?.data?.errors || []));
    
    // ------ Data fetch ------
    useEffect(() => {
        axios.get("/classtableinheritance/findAll")
            .then(response => setEmployees(response.data?.data || []))
            .catch(error => setErrorMessages(error.response?.data?.errors || []));
    }, [refresh]);

    return (
        <Container className="mt-4">
            <h2 className="mb-4">Employee Manager</h2>
            <AlertMessage messages={errorMessages} onClose={() => setErrorMessages([])} />

            <Row>
                { /* ------ Employee Section ------ */}
                <Col md={{ span: 8, offset: 2 }}>
                    <Form className="mb-4">
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="employeeType">
                            <Form.Label column sm={3} className="text-start">
                                Choose Employee:
                            </Form.Label>
                            <Col sm={9}>
                                <Form.Select
                                    value={employeeType}
                                    onChange={e => setEmployeeType(e.target.value)}
                                >
                                    <option value="">Select Employee Type</option>
                                    <option value="permanentEmployee">PermanentEmployee</option>
                                    <option value="contractorEmployee">ContractorEmployee</option>
                                </Form.Select>
                            </Col>
                        </Form.Group>
                        {employeeType === 'permanentEmployee' && (
                        <>
                            <Form.Group as={Row} className="mb-3 align-items-center" controlId="leaves">
                                <Form.Label column sm={3} className="text-start">
                                    Leaves:
                                </Form.Label>
                                <Col sm={9}>
                                    <Form.Control
                                        type="number"
                                        placeholder="Enter number of leaves"
                                        value={leaves}
                                        onChange={e => setLeaves(e.target.value)}
                                    />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row} className="mb-3 align-items-center" controlId="salary">
                                <Form.Label column sm={3} className="text-start">
                                    Salary:
                                </Form.Label>
                                <Col sm={9}>
                                    <Form.Control
                                        type="number"
                                        placeholder="Enter salary"
                                        value={salary}
                                        onChange={e => setSalary(e.target.value)}
                                    />
                                </Col>
                            </Form.Group>
                        </>
                        )}
                        {employeeType === 'contractorEmployee' && (
                        <>
                            <Form.Group as={Row} className="mb-3 align-items-center" controlId="hourleyRate">
                                <Form.Label column sm={3} className="text-start">
                                    HourleyRate:
                                </Form.Label>
                                <Col sm={9}>
                                    <Form.Control
                                        type="number"
                                        placeholder="Enter hourly rate"
                                        value={hourleyRate}
                                        onChange={e => setHourleyRate(e.target.value)}
                                    />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row} className="mb-3 align-items-center" controlId="overtimeRate">
                                <Form.Label column sm={3} className="text-start">
                                    OvertimeRate:
                                </Form.Label>
                                <Col sm={9}>
                                    <Form.Control
                                        type="number"
                                        placeholder="Enter overtime rate"
                                        value={overtimeRate}
                                        onChange={e => setOvertimeRate(e.target.value)}
                                    />
                                </Col>
                            </Form.Group>
                        </>
                        )}
                        <Form.Group as={Row} className="mb-3 align-items-center" controlId="employeeName">
                            <Form.Label column sm={3} className="text-start">Name:</Form.Label>
                            <Col sm={9}>
                                <div className="d-flex gap-2">
                                    <Form.Control type="text" placeholder="Enter employee name" value={employeeName} onChange={(e) => setEmployeeName(e.target.value)} />
                                    <Button variant="primary" onClick={handleCreateOrEditEmployee}>
                                        {selectedEmployee ? 'Edit' : 'Create'}
                                    </Button>
                                    {selectedEmployee && (
                                        <Button variant="secondary" onClick={() => { setSelectedEmployee(null); setEmployeeName(""); setEmployeeType(""); setLeaves(""); setSalary(""); setHourleyRate(""); setOvertimeRate(""); }}>Cancel</Button>
                                    )}
                                </div>
                            </Col>
                        </Form.Group>
                    </Form>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Type</th>
                                <th>Name</th>
                                <th>Details</th>
                                <th style={{ width: '200px' }}>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {employees.length === 0 ? (
                                <tr>
                                    <td colSpan="4" className="text-center text-muted">No employees</td>
                                </tr>
                            ) : employees.map(e => (
                                <tr key={e.id}>
                                    <td>{e.type}</td>
                                    <td>{e.name}</td>
                                    <td>
                                        {e.type === 'permanentEmployee' && `Leaves: ${e.leaves}, Salary: ${e.salary}`}
                                        {e.type === 'contractorEmployee' && `HourleyRate: ${e.hourleyRate}, OvertimeRate: ${e.overtimeRate}`}
                                    </td>
                                    <td>
                                        <Button variant="warning" size="sm" className="me-2" onClick={() => {
                                            setSelectedEmployee(e);
                                            setEmployeeName(e.name);
                                            setEmployeeType(e.type);
                                            if (e.type === 'permanentEmployee') {
                                                setLeaves(e.leaves || '');
                                                setSalary(e.salary || '');
                                                setHourleyRate('');
                                                setOvertimeRate('');
                                            } else if (e.type === 'contractorEmployee') {
                                                setHourleyRate(e.hourleyRate || '');
                                                setOvertimeRate(e.overtimeRate || '');
                                                setLeaves('');
                                                setSalary('');
                                            }
                                        }}>Edit</Button>
                                        <Button variant="danger" size="sm" onClick={() => handleDeleteEmployee(e.id)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>
    );
};

export default EmployeeManager;