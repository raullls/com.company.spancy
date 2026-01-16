import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Container, ListGroup } from 'react-bootstrap';
import './App.css';
import ProductManager from './ProductManager/ProductManager';
import BookManager from './BookManager/BookManager';
import CustomerManager from './CustomerManager/CustomerManager';
import StudentManager from './StudentManager/StudentManager';
import PersonManager from './PersonManager/PersonManager';
import ItemManager from './ItemManager/ItemManager';
import CategoryManager from './CategoryManager/CategoryManager';
import UserGroupManager from './UserGroupManager/UserGroupManager';
import ClientAccountManager from './ClientAccountManager/ClientAccountManager';
import ManuscriptAuthorManager from './ManuscriptAuthorManager/ManuscriptAuthorManager';
import MemberMemberManager from './MemberMemberManager/MemberMemberManager';
import WorkerWorkerManager from './WorkerWorkerManager/WorkerWorkerManager';
import ProtocolManager from './ProtocolManager/ProtocolManager';
import EstateManager from './EstateManager/EstateManager';
import EmployeeManager from './EmployeeManager/EmployeeManager';

function App() {
    return (
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/productmanager" element={<ProductManager />} />
                    <Route path="/bookmanager" element={<BookManager />} />
                    <Route path="/customermanager" element={<CustomerManager />} />
                    <Route path="/studentmanager" element={<StudentManager />} />
                    <Route path="/personmanager" element={<PersonManager />} />
                    <Route path="/itemmanager" element={<ItemManager />} />
                    <Route path="/categorymanager" element={<CategoryManager />} />
                    <Route path="/usergroupmanager" element={<UserGroupManager />} />
                    <Route path="/clientaccountmanager" element={<ClientAccountManager />} />
                    <Route path="/manuscriptauthormanager" element={<ManuscriptAuthorManager />} />
                    <Route path="/membermembermanager" element={<MemberMemberManager />} />
                    <Route path="/workerworkermanager" element={<WorkerWorkerManager />} />
                    <Route path="/protocolmanager" element={<ProtocolManager />} />
                    <Route path="/estatemanager" element={<EstateManager />} />
                    <Route path="/employeemanager" element={<EmployeeManager />} />
                </Routes>
            </div>
        </Router>
    );
};

function Home() {
    return (
        <Container className="py-4">
            <h2>Welcome</h2>
            <p>Choose a manager:</p>
            <ListGroup as="ul">
                <ListGroup.Item as="li">
                    <Link to="/productmanager">Product Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/bookmanager">Book Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/customermanager">Customer Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/studentmanager">Student Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/personmanager">Person Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/itemmanager">Item Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/categorymanager">Category Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/usergroupmanager">User Group Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/clientaccountmanager">Client Account Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/manuscriptauthormanager">Manuscript Author Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/membermembermanager">Member Member Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/workerworkermanager">Worker Worker Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/protocolmanager">Protocol Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/estatemanager">Estate Manager</Link>
                </ListGroup.Item>
                <ListGroup.Item as="li">
                    <Link to="/employeemanager">Employee Manager</Link>
                </ListGroup.Item>
            </ListGroup>
        </Container>
    );
};

export default App;
