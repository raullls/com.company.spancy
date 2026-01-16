import { render, screen, fireEvent, waitFor, within, act } from '@testing-library/react';
import axios from 'axios';
import EmployeeManager from './EmployeeManager';

jest.mock('axios');

describe('EmployeeManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Employee Manager title', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<EmployeeManager />);
        });

        const titleElement = screen.getByRole('heading', { name: /employee manager/i });
        expect(titleElement).toBeInTheDocument();
    });

    test('add employee successfully', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'alex', type: 'permanentEmployee', leaves: 14, salary: 100 }] } });

        await act(async () => {
            render(<EmployeeManager />);
        });

        fireEvent.change(screen.getByLabelText(/choose employee/i), { target: { value: 'permanentEmployee' } });
        fireEvent.change(screen.getByLabelText(/name/i), { target: { value: 'alex' } });
        fireEvent.change(screen.getByLabelText(/leaves/i), { target: { value: 14 } });
        fireEvent.change(screen.getByLabelText(/salary/i), { target: { value: 100 } });
        fireEvent.click(screen.getByRole('button', { name: /^create$/i }));

        expect(await screen.findByText('permanentEmployee')).toBeInTheDocument();
        expect(await screen.findByText('alex')).toBeInTheDocument();
    });

    test('remove an employee', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'alex', type: 'permanentEmployee', leaves: 14, salary: 100 }] } });
        axios.post.mockResolvedValueOnce({ data: null, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<EmployeeManager />);
        });

        const row = (await screen.findByText('alex')).closest('tr');

        const deleteButton = within(row).getByRole('button', { name: /delete/i });
        fireEvent.click(deleteButton);

        await waitFor(() =>
            expect(screen.queryByText('alex')).not.toBeInTheDocument()
        );
    });
});