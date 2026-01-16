import { render, screen, fireEvent, waitFor, within } from '@testing-library/react';
import axios from 'axios';
import UserGroupManager from './UserGroupManager';

jest.mock('axios');

describe('UserGroupManager Component', () => {
    beforeEach(() => {
        axios.post.mockClear();
        axios.get.mockClear();
    });

    test('renders the User Group Manager title', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        waitFor(() => {
            render(<UserGroupManager />);
        });

        const titleElement = screen.getByText(/User Group Manager/i);
        expect(titleElement).toBeInTheDocument();
    });

    test('adds an user, group and assignment successfully', () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{id:1, name:'Prasad'}] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Prasad' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Developer' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: false, errors: null });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Prasad' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Developer' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ userDto: { id: 1, name: 'Prasad' }, groupDto: {id: 1, name: 'Developer'}}] } });

        waitFor(() => {
            render(<UserGroupManager />);
        });

        const userSection = screen.getByRole('heading', { name: /enter user name/i }).closest('div');
        const groupSection = screen.getByRole('heading', { name: /enter group name/i }).closest('div');
        fireEvent.change(within(userSection).getByLabelText(/^name/i), { target: { value: 'Prasad' } });
        fireEvent.click(within(userSection).getByRole('button', { name: /create/i }));
        fireEvent.change(within(groupSection).getByLabelText(/^name/i), { target: { value: 'Developer' } });
        fireEvent.click(within(groupSection).getByRole('button', { name: /create/i }));

        waitFor(() => {
            expect(screen.findByText('Prasad')).toBeInTheDocument();
            expect(screen.findByText('Developer')).toBeInTheDocument();
        });
    });

    test('remove a group and an assignment automatically', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Prasad' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Developer' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ userDto: { id: 1, name: 'Prasad' }, groupDto: { id: 1, name: 'Developer' } }] } });
        axios.post.mockResolvedValueOnce({ data: null, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Prasad' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<UserGroupManager />);

        const groupSection = await screen.getByRole('heading', { name: /enter group name/i }).closest('div');
        const deleteButton = await within(groupSection.closest("div")).findByRole('button', { name: /delete/i });
        fireEvent.click(deleteButton);

        await screen.findAllByText('Prasad');
        await waitFor(() =>
            expect(screen.queryByText('Developer')).not.toBeInTheDocument()
        );
    });
});