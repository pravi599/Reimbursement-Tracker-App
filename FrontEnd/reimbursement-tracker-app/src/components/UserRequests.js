import React, { useState, useEffect } from 'react';
import axios from 'axios';
import UpdateRequest from './UpdateRequest';
import './UserRequests.css';

const UserRequests = () => {
  const [userRequests, setUserRequests] = useState([]);
  const [selectedRequest, setSelectedRequest] = useState(null);
  const [isUpdateModalOpen, setUpdateModalOpen] = useState(false);
  const [documentModal, setDocumentModal] = useState({ isOpen: false, documentUrl: '' });

  useEffect(() => {
    const fetchUserRequests = async () => {
      try {
        // Get the username from localStorage
        const username = localStorage.getItem('username');

        if (username) {
          // Send a GET request to fetch user-specific requests
          const response = await axios.get(`https://localhost:7007/api/Request/user/${username}`);
          setUserRequests(response.data);
          console.log(response);
        } else {
          console.log('Username not found in localStorage. Please log in.');
        }
      } catch (error) {
        console.error('Error fetching user requests:', error);
      }
    };

    fetchUserRequests();
  }, []);

  const handleDeleteRequest = async (requestId) => {
    // Show a confirmation alert before deleting
    const isConfirmed = window.confirm("Do you really want to delete the request? Once deleted, it cannot be undone.");
  
    if (isConfirmed) {
      try {
        // Send a DELETE request to delete the request on the server
         await axios.delete(`https://localhost:7007/api/Request/${requestId}`);
      
        
        // Update the user requests after deleting
        const updatedRequests = userRequests.filter((request) => request.requestId !== requestId);
        setUserRequests(updatedRequests);
  
        // Notify the user after successful deletion
        window.alert(`Request with ID ${requestId} deleted successfully.`);
      } catch (error) {
        console.error('Error deleting request:', error);
      }
    } else {
      // Notify the user that the request was not deleted
      window.alert('Deletion canceled. The request was not deleted.');
    }
  }

  const handleUpdateRequest = (request) => {
    setSelectedRequest(request);
    setUpdateModalOpen(true);
  };

  const handleUpdateSuccess = () => {
    setUpdateModalOpen(false);
    setSelectedRequest(null);
    // Optionally, update the user requests after a successful update
  };


  const handleCloseUpdateModal = () => {
    setUpdateModalOpen(false);
    setSelectedRequest(null);
  };
  const handleViewDocument = (documentUrl) => {
    // // Open the document in a new tab or window
    // window.open(documentUrl, '_blank');
    // Open the document in the modal
    setDocumentModal({ isOpen: true, documentUrl });
  };
  const handleCloseDocumentModal = () => {
    // Close the document modal
    setDocumentModal({ isOpen: false, documentUrl: '' });
  };

  return (
    <div>
      <h2>User Requests</h2>
      <div className="request-container">
        {userRequests.map((request) => (
          <div key={request.requestId} className="request-box">
            <h3>Request ID: {request.requestId}</h3>
            <p>Expense Category: {request.expenseCategory}</p>
            <p>Amount: {request.amount}</p>
            <p>
              Document: 
              {/* Add a View button for the document */}
              
              <button onClick={() => handleViewDocument(request.document)} className="Button">View Document</button>
            </p>
            <p>Description: {request.description}</p>
            <p>Request Date: {new Date(request.requestDate).toLocaleString()}</p>
            <div className="actions">
              <button onClick={() => handleDeleteRequest(request.requestId)} className="btn btn-danger">Delete</button>
              <button onClick={() => handleUpdateRequest(request)} className="btn btn-primary">Update</button>
            </div>
          </div>
        ))}
      </div>
      
      {selectedRequest && isUpdateModalOpen && (
        <UpdateRequest
          request={selectedRequest}
          onUpdateSuccess={handleUpdateSuccess}
          onClose={handleCloseUpdateModal}
        />
      )}
      {documentModal.isOpen && (
  <div className="document-modal">
    <div className="document-content">
      <button className="close-btn" onClick={handleCloseDocumentModal}>
        <span>&times;</span>
      </button>
      {/* Render the document content here */}
      <iframe src={documentModal.documentUrl} title="Document Viewer" width="100%" height="100%" />
    </div>
  </div>
)}
    </div>
  );
};

export default UserRequests;