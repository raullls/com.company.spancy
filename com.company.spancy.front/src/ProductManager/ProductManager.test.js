import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import ProductManager from './ProductManager';

jest.mock('axios');

describe('ProductManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Product Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<ProductManager />);
        });

        const titleElement = screen.getByText(/Product Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a product successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mouse' }] } });

        waitFor(() => {
            render(<ProductManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Mouse' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Mouse')).toBeInTheDocument();
        });
    });

    test('adds a duplicated product', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mouse' }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object Mouse already found in database'] } } });
        
        render(<ProductManager />);

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Mouse' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete a product', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mouse' }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        
        render(<ProductManager />);

        expect(await screen.findByText('Mouse')).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('Mouse')).not.toBeInTheDocument();
        });
    });
});