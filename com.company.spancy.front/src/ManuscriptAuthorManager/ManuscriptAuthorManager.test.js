import { render, screen, fireEvent, waitFor, within, act } from '@testing-library/react';
import axios from 'axios';
import ManuscriptAuthorManager from './ManuscriptAuthorManager';

jest.mock('axios');

describe('ManuscriptAuthorManager Component', () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    test('renders the Manuscript Author Manager title', async () => {
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });
        axios.get.mockResolvedValueOnce({ data: { data: [] } });

        render(<ManuscriptAuthorManager />);

        expect(
            await screen.findByText(/Manuscript Author Manager/i)
        ).toBeInTheDocument();
    });

    test('adds a manuscript, author and an assignment successfully', async () => {
        // ---- Initial load (3 GETs in useEffect) ----
        axios.get
            .mockResolvedValueOnce({ data: { data: [] } }) // authors
            .mockResolvedValueOnce({ data: { data: [] } }) // manuscripts
            .mockResolvedValueOnce({ data: { data: [] } }); // manuscriptAuthors

        // ---- Create manuscript ----
        axios.post.mockResolvedValueOnce({ data: 1 });

        // ---- Reload after manuscript creation ----
        axios.get
            .mockResolvedValueOnce({ data: { data: [] } }) // authors
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'TDD' }] } }) // manuscripts
            .mockResolvedValueOnce({ data: { data: [] } }); // manuscriptAuthors

        // ---- Create author ----
        axios.post.mockResolvedValueOnce({ data: 1 });

        // ---- Reload after author creation ----
        axios.get
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'James' }] } }) // authors
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'TDD' }] } }) // manuscripts
            .mockResolvedValueOnce({ data: { data: [] } }); // manuscriptAuthors

        // ---- Assignment: isPresent ----
        axios.post.mockResolvedValueOnce({ data: { data: false } });

        // ---- Assignment: create ----
        axios.post.mockResolvedValueOnce({ data: 1 });

        // ---- Reload after assignment ----
        axios.get
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'James' }] } }) // authors
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'TDD' }] } }) // manuscripts
            .mockResolvedValueOnce({
                data: {
                    data: [{
                        manuscriptDto: { id: 1, name: 'TDD' },
                        authorDto: { id: 1, name: 'James' },
                        publisher: 'Apress'
                    }]
                }
            });

        // ---- Render ----
        render(<ManuscriptAuthorManager />);

        // =============================
        // Create manuscript
        // =============================
        const manuscriptSection = screen
            .getByRole('heading', { name: /enter manuscript name/i })
            .closest('div');

        fireEvent.change(
            within(manuscriptSection).getByLabelText(/^name/i),
            { target: { value: 'TDD' } }
        );

        fireEvent.click(
            within(manuscriptSection).getByRole('button', { name: /create/i })
        );

        await screen.findAllByText('TDD');

        // =============================
        // Create author
        // =============================
        const authorSection = screen
            .getByRole('heading', { name: /enter author name/i })
            .closest('div');

        fireEvent.change(
            within(authorSection).getByLabelText(/^name/i),
            { target: { value: 'James' } }
        );

        fireEvent.click(
            within(authorSection).getByRole('button', { name: /create/i })
        );

        await screen.findAllByText('James');

        // =============================
        // Assign author to manuscript
        // =============================
        fireEvent.change(
            screen.getAllByRole('combobox')[0],
            { target: { value: '1' } }
        );

        fireEvent.change(
            screen.getAllByRole('combobox')[1],
            { target: { value: '1' } }
        );

        fireEvent.change(
            screen.getByPlaceholderText(/enter publisher name/i),
            { target: { value: 'Apress' } }
        );

        fireEvent.click(
            screen.getByRole('button', { name: /assign/i })
        );

        // =============================
        // Assert assignment table
        // =============================
        await waitFor(() => {
            expect(screen.getAllByText('TDD').length).toBeGreaterThan(0);
            expect(screen.getAllByText('James').length).toBeGreaterThan(0);
            expect(screen.getAllByText('Apress').length).toBeGreaterThan(0);
        });
    });

    test('remove an author and an assignment automatically', async () => {
        axios.get
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'James' }] } })
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'TDD' }] } })
            .mockResolvedValueOnce({ data: { data: [{ manuscriptDto: { id: 1, name: 'TDD' }, authorDto: { id: 1, name: 'James' }, publisher: 'Apress' }] } });

        axios.post.mockResolvedValueOnce({ data: null, errors: null });

        axios.get
            .mockResolvedValueOnce({ data: { data: [{ id: 1, name: 'James' }] } })
            .mockResolvedValueOnce({ data: { data: [] } })
            .mockResolvedValueOnce({ data: { data: [] } });

        await act(async () => {
            render(<ManuscriptAuthorManager />);
        });

        // Wait until manuscript appears in the table
        const manuscriptsTable = screen
            .getByRole('heading', { name: /Enter Manuscript Name/i })
            .closest('div')
            .querySelector('table');

        const manuscriptRow = within(manuscriptsTable)
            .getByText('TDD')
            .closest('tr');

        await act(async () => {
            fireEvent.click(within(manuscriptRow).getByRole('button', { name: /delete/i }));
        });

        await screen.findAllByText('James');

        // Assignment should disappear from assignments table only
        const assignmentsTable = screen
            .getByRole('heading', { name: /Assign Author to Manuscript/i })
            .closest('div')
            .querySelector('table');

        await waitFor(() => {
            expect(
                within(assignmentsTable).queryByText('James')
            ).not.toBeInTheDocument();
        });
    });
});