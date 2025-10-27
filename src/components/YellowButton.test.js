import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import YellowButton from './YellowButton';

test('renders Yellow Button and triggers action on click', () => {
    const handleClick = jest.fn();
    render(<YellowButton onClick={handleClick}>Click Me</YellowButton>);

    const button = screen.getByRole('button', { name: /Yellow Button/i });
    expect(button).toBeInTheDocument();
    fireEvent.click(button);
    expect(handleClick).toHaveBeenCalledTimes(1);
});
