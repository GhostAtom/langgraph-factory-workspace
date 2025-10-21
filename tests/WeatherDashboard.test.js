import React from 'react';
import { render, screen } from '@testing-library/react';
import WeatherDashboard from '../src/components/WeatherDashboard';

jest.mock('axios', () => ({
  get: jest.fn(() => Promise.resolve({ data: { speed: 10, direction: 'NE' } }))
}));

test('renders wind speed and direction', async () => {
  render(<WeatherDashboard />);
  const speedElement = await screen.findByText(/Wind Speed: 10 knots/i);
  const directionElement = await screen.findByText(/Wind Direction: NE/i);
  expect(speedElement).toBeInTheDocument();
  expect(directionElement).toBeInTheDocument();
});
