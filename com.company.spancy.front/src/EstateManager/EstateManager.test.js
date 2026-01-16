import { render, screen, fireEvent, waitFor, within, act } from '@testing-library/react';
import axios from 'axios';
import EstateManager from './EstateManager';

jest.mock('axios');

describe('EstateManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Estate Manager title', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<EstateManager />);
        });

        const titleElement = screen.getByRole('heading', { name: /estate manager/i });
        expect(titleElement).toBeInTheDocument();
    });

    test('add estate successfully', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'b1', type: 'building', floors: 3 }] } });

        await act(async () => {
            render(<EstateManager />);
        });

        fireEvent.change(screen.getByLabelText(/choose estate/i), { target: { value: 'building' } } );
        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'b1' } });
        fireEvent.change(screen.getByLabelText(/floors/i), { target: { value: 3 } });
        fireEvent.click(screen.getByRole('button', { name: /^create$/i }));

        expect(await screen.findByText('building')).toBeInTheDocument();
        expect(await screen.findByText('b1')).toBeInTheDocument();
    });

    test('remove a estate', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'b1', type: 'building', floors: 3 }] } });
        axios.post.mockResolvedValueOnce({ data: null, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<EstateManager />);
        });

        const row = (await screen.findByText('b1')).closest('tr');

        const deleteButton = within(row).getByRole('button', { name: /delete/i });
        fireEvent.click(deleteButton);

        await waitFor(() =>
            expect(screen.queryByText('b1')).not.toBeInTheDocument()
        );
    });
});