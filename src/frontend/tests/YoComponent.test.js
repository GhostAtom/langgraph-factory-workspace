import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import YoComponent from '../ui/YoComponent';

it('sends a Yo on button click', () => {
    const sendYo = jest.fn();
    render(<YoComponent sendYo={sendYo} />);
    
    const input = screen.getByPlaceholderText("Enter friend's name");
    const button = screen.getByText('Send Yo');

    fireEvent.change(input, { target: { value: 'Alice' } });
    fireEvent.click(button);

    expect(sendYo).toHaveBeenCalledWith('Alice');
});
