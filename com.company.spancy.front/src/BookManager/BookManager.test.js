import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import BookManager from './BookManager';

jest.mock('axios');

describe('BookManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Book Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<BookManager />);
        });

        const titleElement = screen.getByText(/Book Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a book successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Design Patterns', city: 'Chicago' }] } });

        waitFor(() => {
            render(<BookManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Design Patterns' } });
        fireEvent.change(screen.getByLabelText(/city/i), { target: { value: 'Chicago' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Design Patterns')).toBeInTheDocument();
            expect(screen.findByText('Chicago')).toBeInTheDocument();
        });
    });

    test('adds a duplicated book', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Design Patterns', city: 'Chicago' }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object book already found in database'] } } });

        render(<BookManager />);

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Design Patterns' } });
        fireEvent.change(screen.getByLabelText(/city/i), { target: { value: 'Chicago' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete a book', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Design Patterns', city: 'Chicago' }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<BookManager />);

        expect(await screen.findByText('Design Patterns')).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('Design Patterns')).not.toBeInTheDocument();
        });
    });
});