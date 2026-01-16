import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import StudentManager from './StudentManager';

jest.mock('axios');

describe('StudentManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Student Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<StudentManager />);
        });

        const titleElement = screen.getByText(/Student Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a student successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kdo', mentorName: 'Alex' }] } });

        waitFor(() => {
            render(<StudentManager />);
        });

        fireEvent.change(screen.getByLabelText(/^name/i), { target: { value: 'Kdo' } });
        fireEvent.change(screen.getByLabelText(/mentor name/i), { target: { value: 'Alex' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Kdo')).toBeInTheDocument();
            expect(screen.findByText('Alex')).toBeInTheDocument();
        });
    });

    test('adds a duplicated student', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kdo', mentorName: 'Alex' }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object student already found in database'] } } });

        render(<StudentManager />);

        fireEvent.change(screen.getByLabelText(/^name/i), { target: { value: 'Kdo' } });
        fireEvent.change(screen.getByLabelText(/mentor name/i), { target: { value: 'Alex' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete a student', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kdo', mentorName: 'Alex' }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<StudentManager />);

        expect(await screen.findByText('Kdo')).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('Kdo')).not.toBeInTheDocument();
        });
    });
});