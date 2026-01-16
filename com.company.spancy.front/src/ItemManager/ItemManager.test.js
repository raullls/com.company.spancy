import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import ItemManager from './ItemManager';

jest.mock('axios');

describe('ItemManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Item Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<ItemManager />);
        });

        const titleElement = screen.getByText(/Item Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds an item successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Toothpaste', featureList: ['Taste'] }] } });

        waitFor(() => {
            render(<ItemManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Toothpaste' } });
        fireEvent.change(screen.getByLabelText(/feature/i), { target: { value: 'Taste' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Toothpaste')).toBeInTheDocument();
            expect(screen.findByText('Taste')).toBeInTheDocument();
        });
    });

    test('adds a duplicated item', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Toothpaste', featureList: ['Taste'] }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object item already found in database'] } } });

        render(<ItemManager />);

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Toothpaste' } });
        fireEvent.change(screen.getByLabelText(/feature/i), { target: { value: 'Taste' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete an item', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Toothpaste', featureList: ['Taste'] }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<ItemManager />);

        expect(await screen.findByText('Toothpaste')).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('Toothpaste')).not.toBeInTheDocument();
        });
    });
});