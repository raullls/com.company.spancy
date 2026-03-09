import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import axios from 'axios';
import { vi } from 'vitest';
import BookManager from './BookManager';

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

describe('BookManager', () => {
    test('renders title', async () => {
        render(<BookManager />);

        const title = screen.getByText(/book manager/i);

        await waitFor(() => {
            expect(title).toBeInTheDocument();
        });
    });

    test('allows typing in name and city inputs', async () => {
        render(<BookManager />);

        const nameInput = screen.getByPlaceholderText(/enter book name/i);
        const cityInput = screen.getByPlaceholderText(/enter city/i);

        fireEvent.change(nameInput, {
            target: { value: 'Clean Code' }
        });
        fireEvent.change(cityInput, {
            target: { value: 'Madrid' }
        });

        await waitFor(() => {
            expect(nameInput).toHaveValue('Clean Code');
            expect(cityInput).toHaveValue('Madrid');
        });
    });

    test('loads books from API and renders rows', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 1, name: 'Book A', city: 'Barcelona' },
                    { id: 2, name: 'Book B', city: 'Valencia' }
                ]
            }
        });

        render(<BookManager />);

        await waitFor(() => {
            expect(screen.getByText('Book A')).toBeInTheDocument();
            expect(screen.getByText('Book B')).toBeInTheDocument();
            expect(screen.getByText('Barcelona')).toBeInTheDocument();
            expect(screen.getByText('Valencia')).toBeInTheDocument();
        });
    });

    test('clicking edit prefills form and shows cancel', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 10, name: 'DDD', city: 'Seville' }
                ]
            }
        });

        render(<BookManager />);

        await waitFor(() => {
            expect(screen.getByText('DDD')).toBeInTheDocument();
        });

        fireEvent.click(screen.getByRole('button', { name: /^edit$/i }));

        await waitFor(() => {
            expect(screen.getByPlaceholderText(/enter book name/i)).toHaveValue('DDD');
            expect(screen.getByPlaceholderText(/enter city/i)).toHaveValue('Seville');
            expect(screen.getByRole('button', { name: /^cancel$/i })).toBeInTheDocument();
        });
    });
});
