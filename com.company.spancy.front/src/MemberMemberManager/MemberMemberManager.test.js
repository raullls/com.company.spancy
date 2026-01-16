import { render, screen, fireEvent, waitFor, within, act } from '@testing-library/react';
import axios from 'axios';
import MemberMemberManager from './MemberMemberManager';

jest.mock('axios');

describe('MemberMemberManager Component', () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    test('renders the Member Member Manager title', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<MemberMemberManager />);

        expect(
            await screen.findByText(/Member Member Manager/i)
        ).toBeInTheDocument();
    });

    test('adds two members and an assignment successfully', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Harry' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: 2, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Harry' }, { id: 2, name: 'Potter' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.post.mockResolvedValueOnce({ data: false, errors: null });
        axios.post.mockResolvedValueOnce({ data: 1, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Harry' }, { id: 2, name: 'Potter' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ memberId1: { id: 1, name: 'Harry' }, memberId2: { id: 2, name: 'Potter' } }] } });

        render(<MemberMemberManager />);

        const nameInput = screen.getByLabelText(/name:/i);
        const createButton = screen.getByRole('button', { name: /create/i });

        // ---- Create Harry ----
        fireEvent.change(nameInput, { target: { value: 'Harry' } });
        fireEvent.click(createButton);

        await waitFor(() => {
            expect(screen.queryByText('No members')).not.toBeInTheDocument();
        });

        // ---- Create Potter ----
        fireEvent.change(nameInput, { target: { value: 'Potter' } });
        fireEvent.click(createButton);

        await waitFor(() => {
            expect(screen.getAllByText('Harry').length).toBeGreaterThan(0);
            expect(screen.getAllByText('Potter').length).toBeGreaterThan(0);
        });

        // ---- Assign Harry -> Potter ----
        const selects = screen.getAllByRole('combobox');
        fireEvent.change(selects[0], { target: { value: '1' } });
        fireEvent.change(selects[1], { target: { value: '2' } });

        fireEvent.click(screen.getByRole('button', { name: /assign/i }));

        const assignmentsTable = screen.getAllByRole('table')[1];

        await waitFor(() => {
            const rows = within(assignmentsTable).getAllByRole('row');
            expect(
                rows.some(row =>
                    row.textContent.includes('Harry') &&
                    row.textContent.includes('Potter')
                )
            ).toBe(true);
        });
    });

    test('remove a member and an assignment automatically', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Harry' }, { id: 2, name: 'Potter' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [{ memberId1: { id: 1, name: 'Harry' }, memberId2: { id: 2, name: 'Potter' } }] } });
        axios.post.mockResolvedValueOnce({ data: 0, errors: null });
        axios.get.mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'Harry' }] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<MemberMemberManager />);

        const memberSection = (await screen.findByRole('heading', { name: /Enter Member Name/i })).closest('div');
        const memberScope = within(memberSection);
        const potterCell = await memberScope.findByText('Potter');
        const potterRow = potterCell.closest('tr');
        const deleteButton = await within(potterRow).findByRole('button', { name: /delete/i });
        fireEvent.click(deleteButton);

        await screen.findAllByText('Harry');
        await waitFor(() =>
            expect(screen.queryByText('Potter')).not.toBeInTheDocument()
        );
    });
});