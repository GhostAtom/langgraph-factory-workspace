import React from 'react';
import { render } from '@testing-library/react';
import RedSquare from './RedSquare';

test('renders a red square with default size', () => {
  const { getByRole } = render(<RedSquare />);
  const square = getByRole('presentation');
  expect(square).toHaveStyle({ width: '100px', height: '100px', backgroundColor: 'red' });
});

test('renders a red square with specified size', () => {
  const { getByRole } = render(<RedSquare size={150} />);
  const square = getByRole('presentation');
  expect(square).toHaveStyle({ width: '150px', height: '150px' });
});
