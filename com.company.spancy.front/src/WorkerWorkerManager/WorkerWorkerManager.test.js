import { render, screen, fireEvent, waitFor, within } from '@testing-library/react';
import axios from 'axios';
import WorkerWorkerManager from './WorkerWorkerManager';

jest.mock('axios');

describe('WorkerWorkerManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Worker Worker Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<WorkerWorkerManager />);
        });

        const titleElement = screen.getByText(/Worker Worker Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds two workers and an assignment successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mao' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 2, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mao' },{ id: 2, name: 'Jit' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: false, errors: null });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mao' },{ id: 2, name: 'Jit' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ workerId1: { id: 1, name: 'Mao' }, workerId2: { id: 2, name: 'Jit' }, relationshipType: 'Sister' }] } });

        waitFor(() => {
            render(<WorkerWorkerManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Mao' } });
        fireEvent.click(screen.getByRole('button', { name: /^create$/i }));
        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Jit' } });
        fireEvent.click(screen.getByRole('button', { name: /^create$/i }));

        fireEvent.change(screen.getByLabelText(/worker 1/i), { target: { value: '1' } } );
        fireEvent.change(screen.getByLabelText(/worker 2/i), { target: { value: '2' } } );
        fireEvent.change(screen.getByLabelText(/relationship type/i), { target: { value: 'Sister' } } );
        fireEvent.click(screen.getByRole('button', { name: /^assign$/i }) );

        waitFor(() => {
            expect(screen.findByText('Mao')).toBeInTheDocument();
            expect(screen.findByText('Jit')).toBeInTheDocument();
            expect(screen.findByText('Sister')).toBeInTheDocument();
        });
    });

    test('remove a worker and an assignment automatically', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mao' },{ id: 2, name: 'Jit' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ workerId1: { id: 1, name: 'Mao' }, workerId2: { id: 2, name: 'Jit' }, relationshipType: 'Sister' }] } });
        axios.post.mockResolvedValueOnce({ data: null, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Mao' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<WorkerWorkerManager />);

        const workerSection = await screen.getByRole('heading', { name: /enter worker name/i }).closest('div');
        const jitRow = (await within(workerSection).findByText('Jit')).closest('tr');
        const deleteButton = within(jitRow).getByRole('button', { name: /delete/i, });
        fireEvent.click(deleteButton);

        await screen.findAllByText('Mao');
        await waitFor(() =>
            expect(screen.queryByText('Jit')).not.toBeInTheDocument()
        );
    });
});