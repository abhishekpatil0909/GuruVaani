import React from 'react';

type ButtonProps = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  label?: string;
};

export const Button: React.FC<ButtonProps> = ({ label, children, ...rest }) => {
  return (
    <button {...rest}>
      {label ?? children}
    </button>
  );
};

export default Button;
