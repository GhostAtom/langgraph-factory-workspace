import React from 'react';

const WindDirection = ({ direction }) => {
  return (
    <div className="wind-direction">
      <p>Wind Direction: {direction}</p>
      {/* Compass representation can be added here */}
    </div>
  );
};

export default WindDirection;
