import React from 'react';
import './YellowButton.css';

const YellowButton = ({ onClick, children }) => {
    return (
        <button className="yellow-button" onClick={onClick} aria-label="Yellow Button">
            {children}
        </button>
    );
};

export default YellowButton;
