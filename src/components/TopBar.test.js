import React from 'react';
import { render, screen } from '@testing-library/react';
import TopBar from './TopBar';

test('renders the top bar', () => {
  render(<TopBar />);
  const topBarElement = screen.getByText(/Application Title/i);
  expect(topBarElement).toBeInTheDocument();
});

test('top bar has correct background color', () => {
  const { container } = render(<TopBar />);
  expect(container.firstChild).toHaveStyle(`background-color: #4CAF50`);
});
