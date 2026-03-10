import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import axios from 'axios';
import { vi } from 'vitest';
import CustomerManager from './CustomerManager';

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

describe('CustomerManager', () => {
    test('renders title', async () => {
        render(<CustomerManager />);

        const title = screen.getByText(/customer manager/i);

        await waitFor(() => {
            expect(title).toBeInTheDocument();
        });
    });

    test('allows typing in name and amount inputs', async () => {
        render(<CustomerManager />);

        const nameInput = screen.getByPlaceholderText(/enter customer name/i);
        const amountInput = screen.getByPlaceholderText(/enter amount/i);

        fireEvent.change(nameInput, {
            target: { value: 'Alice' }
        });
        fireEvent.change(amountInput, {
            target: { value: '250' }
        });

        await waitFor(() => {
            expect(nameInput).toHaveValue('Alice');
            expect(amountInput).toHaveValue(250);
        });
    });

    test('loads customers from API and renders rows', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 1, name: 'Alice', amount: 100 },
                    { id: 2, name: 'Bob', amount: 250 }
                ]
            }
        });

        render(<CustomerManager />);

        await waitFor(() => {
            expect(screen.getByText('Alice')).toBeInTheDocument();
            expect(screen.getByText('Bob')).toBeInTheDocument();
            expect(screen.getByText('100')).toBeInTheDocument();
            expect(screen.getByText('250')).toBeInTheDocument();
        });
    });

    test('clicking edit prefills form and shows cancel', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 10, name: 'Carol', amount: 400 }
                ]
            }
        });

        render(<CustomerManager />);

        await waitFor(() => {
            expect(screen.getByText('Carol')).toBeInTheDocument();
        });

        fireEvent.click(screen.getByRole('button', { name: /^edit$/i }));

        await waitFor(() => {
            expect(screen.getByPlaceholderText(/enter customer name/i)).toHaveValue('Carol');
            expect(screen.getByPlaceholderText(/enter amount/i)).toHaveValue(400);
            expect(screen.getByRole('button', { name: /^cancel$/i })).toBeInTheDocument();
        });
    });
});
