import { Container, ListGroup } from 'react-bootstrap';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css'
import ProductManager from './ProductManager/ProductManager';
import BookManager from './BookManager/BookManager';
import CustomerManager from './CustomerManager/CustomerManager';
import StudentManager from './StudentManager/StudentManager';
import PersonManager from './PersonManager/PersonManager';

function App() {
  return (
    <Router>
      <div className='App'>
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/productmanager' element={<ProductManager />} />
          <Route path='/bookmanager' element={<BookManager />} />
          <Route path='/customermanager' element={<CustomerManager />} />
          <Route path='/studentmanager' element={<StudentManager />} />
          <Route path='/personmanager' element={<PersonManager />} />
        </Routes>
      </div>
    </Router>
  )
}

function Home() {
  return (
    <Container className='py-4'>
      <h2>Welcome</h2>
      <p>Choose a manager:</p>
      <ListGroup as='ul'>
        <ListGroup.Item as='li'>
          <Link to='/productmanager'>Product Manager</Link>
        </ListGroup.Item>
        <ListGroup.Item as='li'>
          <Link to='/bookmanager'>Book Manager</Link>
        </ListGroup.Item>
        <ListGroup.Item as='li'>
          <Link to='/customermanager'>Customer Manager</Link>
        </ListGroup.Item>
        <ListGroup.Item as='li'>
          <Link to='/studentmanager'>Student Manager</Link>
        </ListGroup.Item>
        <ListGroup.Item as='li'>
          <Link to='/personmanager'>Person Manager</Link>
        </ListGroup.Item>
      </ListGroup>
    </Container>
  );
}

export default App
