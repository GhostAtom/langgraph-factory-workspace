import React from 'react';
import PropTypes from 'prop-types';
import './Button.css';

const Button = ({ onClick, children, size, color, disabled }) => {
  return (
    <button 
      className={`btn btn-${size} btn-${color}`} 
      onClick={onClick} 
      disabled={disabled}
    >
      {children}
    </button>
  );
};

Button.propTypes = {
  onClick: PropTypes.func.isRequired,
  children: PropTypes.node.isRequired,
  size: PropTypes.oneOf(['small', 'medium', 'large']),
  color: PropTypes.oneOf(['default', 'primary', 'secondary']),
  disabled: PropTypes.bool,
};

Button.defaultProps = {
  size: 'medium',
  color: 'default',
  disabled: false,
};

export default Button;
