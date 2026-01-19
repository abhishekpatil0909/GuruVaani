import React from 'react';

type ModalProps = {
  isOpen: boolean;
  onClose: () => void;
  title?: string;
  children?: React.ReactNode;
};

export const Modal: React.FC<ModalProps> = ({ isOpen, onClose, title, children }) => {
  if (!isOpen) return null;
  return (
    <div role="dialog" aria-modal>
      <div>
        <header>
          <h3>{title}</h3>
          <button onClick={onClose}>Close</button>
        </header>
        <div>{children}</div>
      </div>
    </div>
  );
};

export default Modal;
