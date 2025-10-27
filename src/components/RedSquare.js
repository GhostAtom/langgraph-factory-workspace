import React from 'react';
import './RedSquare.css';

const RedSquare = ({ size = 100 }) => {
  return (
    <div className="red-square" style={{ width: size, height: size }}></div>
  );
};

export default RedSquare;
