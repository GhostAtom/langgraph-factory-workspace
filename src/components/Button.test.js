import React from 'react';
import { render, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import Button from './Button';

test('Button renders correctly', () => {
  const { getByText } = render(<Button onClick={() => {}}>Click Me</Button>);
  expect(getByText('Click Me')).toBeInTheDocument();
});

test('Button triggers click event', () => {
  const onClickMock = jest.fn();
  const { getByText } = render(<Button onClick={onClickMock}>Click Me</Button>);
  fireEvent.click(getByText('Click Me'));
  expect(onClickMock).toHaveBeenCalledTimes(1);
});

test('Button states', () => {
  const { getByText, rerender } = render(<Button onClick={() => {}} disabled>Click Me</Button>);
  expect(getByText('Click Me')).toBeDisabled();

  rerender(<Button onClick={() => {}}>Click Me</Button>);
  expect(getByText('Click Me')).not.toBeDisabled();
});
