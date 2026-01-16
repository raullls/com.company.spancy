import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import CustomerManager from './CustomerManager';

jest.mock('axios');

describe('CustomerManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Customer Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<CustomerManager />);
        });

        const titleElement = screen.getByText(/Customer Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a customer successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Alex', amount: 400 }] } });

        waitFor(() => {
            render(<CustomerManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Alex' } });
        fireEvent.change(screen.getByLabelText(/amount/i), { target: { value: 400 } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Alex')).toBeInTheDocument();
            expect(screen.findByText('400')).toBeInTheDocument();
        });
    });

    test('adds a duplicated customer', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Alex', amount: 400 }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object customer already found in database'] } } });

        render(<CustomerManager />);

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Alex' } });
        fireEvent.change(screen.getByLabelText(/amount/i), { target: { value: 400 } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete a customer', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Alex', amount: 400 }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<CustomerManager />);

        expect(await screen.findByText('Alex')).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('Alex')).not.toBeInTheDocument();
        });
    });
});