import { render, screen } from '@testing-library/react';
import App from './index';

test('renders welcome message', () => {
    render(<App />);
    const linkElement = screen.getByText(/Welcome to Our Website/i);
    expect(linkElement).toBeInTheDocument();
});