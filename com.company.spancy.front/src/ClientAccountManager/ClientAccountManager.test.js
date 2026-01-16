import { render, screen, fireEvent, waitFor, within } from '@testing-library/react';
import axios from 'axios';
import ClientAccountManager from './ClientAccountManager';

jest.mock('axios');

describe('ClientAccountManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the Client Account Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<ClientAccountManager />);
        });

        const titleElement = screen.getByText(/Client Account Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds a client, account and an assignment successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kaka' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kaka' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, number: '123' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: false, errors: null });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kaka' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, number: '123' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ clientDto: { id: 1, name: 'Kaka' }, accountDto: { id: 1, number: '123' } }] } });

        waitFor(() => {
            render(<ClientAccountManager />);
        });

        const clientSection = screen.getByRole('heading', { name: /enter client name/i }).closest('div');
        const accountSection = screen.getByRole('heading', { name: /enter account name/i }).closest('div');
        fireEvent.change(within(clientSection).getByLabelText(/^name/i), { target: { value: 'Kaka' } });
        fireEvent.click(within(clientSection).getByRole('button', { name: /create/i }));
        fireEvent.change(within(accountSection).getByLabelText(/^name/i), { target: { value: '123' } });
        fireEvent.click(within(accountSection).getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Kaka')).toBeInTheDocument();
            expect(screen.findByText('123')).toBeInTheDocument();
        });
    });

    test('remove an account and an assignment automatically', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kaka' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: '123' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ clientDto: { id: 1, name: 'Kaka' }, accountDto: { id: 1, number: '123' } }] } });
        axios.post.mockResolvedValueOnce({ data: null, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Kaka' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<ClientAccountManager />);

        const accountSection = await screen.getByRole('heading', { name: /enter account name/i }).closest('div');
        const deleteButton = await within(accountSection.closest("div")).findByRole('button', { name: /delete/i });
        fireEvent.click(deleteButton);

        await screen.findAllByText('Kaka');
        await waitFor(() =>
            expect(screen.queryByText('123')).not.toBeInTheDocument()
        );
    });
});