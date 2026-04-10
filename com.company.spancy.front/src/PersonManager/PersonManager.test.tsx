import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import axios from 'axios';
import { vi } from 'vitest';
import PersonManager from './PersonManager';

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

describe('PersonManager', () => {
    test('renders title', async () => {
        render(<PersonManager />);

        const title = screen.getByText(/person manager/i);

        await waitFor(() => {
            expect(title).toBeInTheDocument();
        });
    });

    test('allows typing in name input', async () => {
        render(<PersonManager />);

        const nameInput = screen.getByPlaceholderText(/enter person name/i);

        fireEvent.change(nameInput, { target: { value: 'Alice' } });

        await waitFor(() => {
            expect(nameInput).toHaveValue('Alice');
        });
    });

    test('allows adding phone numbers', async () => {
        render(<PersonManager />);

        const numberInput = screen.getByPlaceholderText(/enter phone number/i);

        fireEvent.change(numberInput, { target: { value: '111-111-1111' } });
        fireEvent.click(screen.getByRole('button', { name: /^add$/i }));

        fireEvent.change(numberInput, { target: { value: '222-222-2222' } });
        fireEvent.click(screen.getByRole('button', { name: /^add$/i }));

        await waitFor(() => {
            expect(screen.getByText('111-111-1111')).toBeInTheDocument();
            expect(screen.getByText('222-222-2222')).toBeInTheDocument();
        });
    });

    test('loads persons from API and renders rows', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 1, name: 'Alice', numbers: ['111-111-1111', '222-222-2222'] },
                    { id: 2, name: 'Bob', numbers: ['333-333-3333'] }
                ]
            }
        });

        render(<PersonManager />);

        await waitFor(() => {
            expect(screen.getByText('Alice')).toBeInTheDocument();
            expect(screen.getByText('Bob')).toBeInTheDocument();
            expect(screen.getByText('111-111-1111, 222-222-2222')).toBeInTheDocument();
            expect(screen.getByText('333-333-3333')).toBeInTheDocument();
        });
    });

    test('clicking edit prefills form and shows cancel', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 10, name: 'Carol', numbers: ['444-444-4444'] }
                ]
            }
        });

        render(<PersonManager />);

        await waitFor(() => {
            expect(screen.getByText('Carol')).toBeInTheDocument();
        });

        fireEvent.click(screen.getByRole('button', { name: /^edit$/i }));

        await waitFor(() => {
            expect(screen.getByPlaceholderText(/enter person name/i)).toHaveValue('Carol');
            expect(screen.getAllByText('444-444-4444').length).toBeGreaterThanOrEqual(1);
            expect(screen.getByRole('button', { name: /^cancel$/i })).toBeInTheDocument();
        });
    });
});