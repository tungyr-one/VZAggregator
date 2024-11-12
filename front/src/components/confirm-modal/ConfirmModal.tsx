import React from 'react';

interface ConfirmModalProps {
  message: string;
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
}

const ConfirmModal: React.FC<ConfirmModalProps> = ({message, isOpen, onClose, onConfirm }) => {
    if (!isOpen) return null;
  
    return (
      <div className="modal-overlay">
        <div className="modal-content">
          <h2>Are you sure?</h2>
          <p>{message}</p>
          <div className="modal-buttons">
            <button onClick={onClose}>Cancel</button>
            <button onClick={onConfirm} className="confirm-btn">Confirm</button>
          </div>
        </div>
      </div>
    );
  };
  
  export default ConfirmModal;