import { Alert } from 'react-bootstrap';

function AlertMessage({ messages, onClose }) {
    if (!messages || messages.length === 0) return null;

    return (
        <Alert variant='danger' onClose={onClose} dismissible>
            <ul className='mb-0'>
                {messages.map((msg, idx) => (
                    <li key={idx}>{msg}</li>
                ))}
            </ul>
        </Alert>
    );
};

export default AlertMessage;