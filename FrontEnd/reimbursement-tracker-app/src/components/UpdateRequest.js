import React, { useState, useEffect } from "react";
import axios from "axios";
import './UpdateRequest.css';

function UpdateRequest({ request, onUpdateSuccess, onClose }) {
  const [updatedRequest, setUpdatedRequest] = useState({ ...request });

  useEffect(() => {
    setUpdatedRequest({ ...request });
  }, [request]);

  const handleInputChange = (e) => {
    const { name, type } = e.target;
  
    if (type === 'file') {
      const fileInput = e.target;
      const file = fileInput.files[0];
      setUpdatedRequest((prev) => ({
        ...prev,
        [name]: file,
      }));
    } else {
      setUpdatedRequest((prev) => ({
        ...prev,
        [name]: e.target.value,
      }));
    }
  };
  
  
  const updateRequest = async () => {
    try {
      const formData = new FormData();
      formData.append('requestId', updatedRequest.requestId);
      formData.append('username', updatedRequest.username);
      formData.append('expenseCategory', updatedRequest.expenseCategory);
      formData.append('amount', updatedRequest.amount);
      formData.append('document', updatedRequest.document);
      formData.append('description', updatedRequest.description);
      formData.append('requestDate', updatedRequest.requestDate);

      await axios.put("https://localhost:7007/api/Request", formData);
      alert('Request Updated Successfully');
      onUpdateSuccess();
      onClose();
    } catch (error) {
      console.error('Error updating request:', error);

      alert('Failed to update request');
      console.log(error.response?.data);
      console.log(updatedRequest);
    }
  };
  // const handleFileChange = (e) => {
  //   // Update the state with the selected file
  //   setUpdatedRequest({ ...updatedRequest, document: e.target.files[0] });
  // };

  return (
    <div className="modal-container">
      <div className="modal-content">
        <span className="close" onClick={onClose}>&times;</span>
        <h2>Update Request</h2>

        <label htmlFor="requestId">Request ID</label>
        <input id="requestId" type="text" value={updatedRequest.requestId} readOnly />

        <label htmlFor="username">Username</label>
        <input id="username" type="text" value={updatedRequest.username} readOnly />

        <label htmlFor="expenseCategory">Expense Category</label>
        <select id="expenseCategory" value={updatedRequest.expenseCategory} onChange={(e) => setUpdatedRequest({ ...updatedRequest, expenseCategory: e.target.value })}>
          <option value="Travel">Travel</option>
          <option value="Meals">Meals</option>
          <option value="Supplies">Supplies</option>
          <option value="Training">Training</option>
          <option value="Health">Health</option>
          <option value="other">Other</option>
        </select>




        <label htmlFor="amount">Amount</label>
        <input id="amount" type="number" value={updatedRequest.amount} onChange={(e) => setUpdatedRequest({ ...updatedRequest, amount: e.target.value })} />

        <label htmlFor="document">Document</label>
        <input id="document" type="file" onChange={handleInputChange} />

        <label htmlFor="description">Description</label>
        <textarea id="description" value={updatedRequest.description} onChange={(e) => setUpdatedRequest({ ...updatedRequest, description: e.target.value })} />

        <label htmlFor="requestDate">Request Date</label>
        <input id="requestDate" type="date" value={updatedRequest.requestDate} onChange={(e) => setUpdatedRequest({ ...updatedRequest, requestDate: e.target.value })} />

        <div className="button-container">
          <button onClick={updateRequest} className="btn btn-primary">Update Request</button>
          <button onClick={onClose} className="btn btn-secondary">Cancel</button>
        </div>
      </div>
    </div>
  );
}

export default UpdateRequest;