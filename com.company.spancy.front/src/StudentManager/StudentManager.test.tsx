import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import axios from 'axios';
import { vi } from 'vitest';
import StudentManager from './StudentManager';

vi.mock('axios');

(axios.get as any).mockResolvedValue({
    data: { data: [] }
});

beforeEach(() => {
    vi.clearAllMocks();

    (axios.get as any).mockResolvedValue({
        data: { data: [] }
    });
});

describe('StudentManager', () => {
    test('renders title', async () => {
        render(<StudentManager />);

        const title = screen.getByText(/student manager/i);

        await waitFor(() => {
            expect(title).toBeInTheDocument();
        });
    });

    test('allows typing in name and mentor inputs', async () => {
        render(<StudentManager />);

        const nameInput = screen.getByPlaceholderText(/enter student name/i);
        const mentorInput = screen.getByPlaceholderText(/enter mentor name/i);

        fireEvent.change(nameInput, { target: { value: 'Alice' } });
        fireEvent.change(mentorInput, { target: { value: 'Professor X' } });

        await waitFor(() => {
            expect(nameInput).toHaveValue('Alice');
            expect(mentorInput).toHaveValue('Professor X');
        });
    });

    test('loads students from API and renders rows', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 1, name: 'Alice', mentorName: 'Mentor Alice' },
                    { id: 2, name: 'Bob', mentorName: 'Mentor Bob' }
                ]
            }
        });

        render(<StudentManager />);

        await waitFor(() => {
            expect(screen.getByText('Alice')).toBeInTheDocument();
            expect(screen.getByText('Bob')).toBeInTheDocument();
            expect(screen.getByText('Mentor Alice')).toBeInTheDocument();
            expect(screen.getByText('Mentor Bob')).toBeInTheDocument();
        });
    });

    test('clicking edit prefills form and shows cancel', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 10, name: 'Carol', mentorName: 'Mentor Carol' }
                ]
            }
        });

        render(<StudentManager />);

        await waitFor(() => {
            expect(screen.getByText('Carol')).toBeInTheDocument();
        });

        fireEvent.click(screen.getByRole('button', { name: /^edit$/i }));

        await waitFor(() => {
            expect(screen.getByPlaceholderText(/enter student name/i)).toHaveValue('Carol');
            expect(screen.getByPlaceholderText(/enter mentor name/i)).toHaveValue('Mentor Carol');
            expect(screen.getByRole('button', { name: /^cancel$/i })).toBeInTheDocument();
        });
    });
});