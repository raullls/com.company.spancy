import { render, screen, fireEvent, waitFor, within, act } from '@testing-library/react';
import axios from 'axios';
import ProtocolManager from './ProtocolManager';

jest.mock('axios');

describe('ProtocolManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Protocol Manager title', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<ProtocolManager />);
        });

        const titleElement = screen.getByRole('heading', { name: /protocol manager/i });
        expect(titleElement).toBeInTheDocument();
    });

    test('add protocol successfully', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'myserver1', type: 'tcp' }] } });

        await act(async () => {
            render(<ProtocolManager />);
        });

        fireEvent.change(screen.getByLabelText(/choose protocol/i), { target: { value: 'tcp' } } );
        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'myserver1' } });
        fireEvent.click(screen.getByRole('button', { name: /^create$/i }));

        expect(await screen.findByText('tcp')).toBeInTheDocument();
        expect(await screen.findByText('myserver1')).toBeInTheDocument();
    });

    test('remove a protocol', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'myserver1', type: 'tcp' }] } });
        axios.post.mockResolvedValueOnce({ data: null, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<ProtocolManager />);
        });

        const row = (await screen.findByText('myserver1')).closest('tr');

        const deleteButton = within(row).getByRole('button', { name: /delete/i });
        fireEvent.click(deleteButton);

        await waitFor(() =>
            expect(screen.queryByText('myserver1')).not.toBeInTheDocument()
        );
    });
});