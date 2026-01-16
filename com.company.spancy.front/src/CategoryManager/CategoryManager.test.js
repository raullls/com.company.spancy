import { render, screen, fireEvent, waitFor, within } from '@testing-library/react';
import axios from 'axios';
import CategoryManager from './CategoryManager';

jest.mock('axios');

describe('CategoryManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Category Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<CategoryManager />);
        });

        const titleElement = screen.getByText(/Category Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a category successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'ceo', parentId: 0 }] } });

        waitFor(() => {
            render(<CategoryManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'ceo' } });
        fireEvent.change(screen.getByLabelText(/parent id/i), { target: { value: 0 } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('ceo')).toBeInTheDocument();
            expect(screen.findByText('0')).toBeInTheDocument();
        });
    });

    test('adds a duplicated category', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'ceo', parentId: 0 }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object item already found in database'] } } });

        render(<CategoryManager />);

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'ceo' } });
        fireEvent.change(screen.getByLabelText(/parent id/i), { target: { value: 0 } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete a category', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'ceo', parentId: 0 }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<CategoryManager />);

        const table = screen.getByRole('table');
        const cell = await within(table).findByText('ceo');
        expect(cell).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('ceo')).not.toBeInTheDocument();
        });
    });
});