import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import PersonManager from './PersonManager';

jest.mock('axios');

describe('PersonManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Person Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<PersonManager />);
        });

        const titleElement = screen.getByText(/Person Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a person successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Ajay', numbers: ['111'] }] } });

        waitFor(() => {
            render(<PersonManager />);
        });

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Ajay' } });
        fireEvent.change(screen.getByLabelText(/phone/i), { target: { value: '111' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Ajay')).toBeInTheDocument();
            expect(screen.findByText('111')).toBeInTheDocument();
        });
    });

    test('adds a duplicated person', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Ajay', numbers: ['111'] }] } });
        axios.post.mockRejectedValueOnce({ response: { data: { errors: ['object student already found in database'] } } });

        render(<PersonManager />);

        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'Ajay' } });
        fireEvent.change(screen.getByLabelText(/phone/i), { target: { value: '111' } });
        fireEvent.click(screen.getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.getByRole('alert')).toBeInTheDocument();
        });
    });

    test('delete a person', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Ajay', numbers: ['111'] }] } });
        axios.post.mockResolvedValueOnce({ data: { data: 0, errors: null } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<PersonManager />);

        expect(await screen.findByText('Ajay')).toBeInTheDocument();

        fireEvent.click(screen.getByRole('button', { name: /delete/i }));

        await waitFor(() => {
            expect(screen.queryByText('Ajay')).not.toBeInTheDocument();
        });
    });
});