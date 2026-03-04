import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { vi } from 'vitest';
import axios from 'axios';
import ProductManager from './ProductManager';

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

describe('ProductManager', () => {

    test('renders title', async () => {
        render(<ProductManager/>);

        const title = screen.getByText(/product manager/i);

        await waitFor(() => {
            expect(title).toBeInTheDocument();
        });
    });

    test('allows typing in input', async () => {
        render(<ProductManager/>);

        const input = screen.getByPlaceholderText(/enter product name/i);
        fireEvent.change(input, {
            target: {value:'Test Product'}
        });

        await waitFor(() => {
            expect(input).toHaveValue('Test Product');
        });
    });

    test('loads products from API', async () => {
        (axios.get as any).mockResolvedValue({
            data: {
                data: [
                    { id: 1, name: 'Product A' },
                    { id: 2, name: 'Product B' }
                ]
            }
        });

        render(<ProductManager/>);

        await waitFor(() => {
            expect(screen.getByText('Product A')).toBeInTheDocument();
            expect(screen.getByText('Product B')).toBeInTheDocument();
        });
    });
});