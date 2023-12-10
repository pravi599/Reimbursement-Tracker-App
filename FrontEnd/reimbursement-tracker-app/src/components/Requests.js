import React, { useState, useEffect } from 'react';
import axios from 'axios';
import UpdateTracking from './UpdateTracking';
import './Requests.css';

const ViewTracking = ({ trackingDetails, onClose }) => {
  return (
    <div>
      <h2>View Tracking</h2>
      <label>
        Request ID:
        <input type="text" name="requestId" value={trackingDetails.requestId} readOnly />
      </label>
      <label>
        Tracking ID:
        <input type="text" name="trackingId" value={trackingDetails.trackingId} readOnly />
      </label>
      <label>
        Tracking Status:
        <input type="text" name="trackingStatus" value={trackingDetails.trackingStatus} readOnly />
      </label>

      <label>
        Approval Date:
        <input name="approvalDate" value={trackingDetails.approvalDate || ''} readOnly />
      </label>
      <label>
        Reimbursement Date:
        <input name="reimbursementDate" value={trackingDetails.reimbursementDate || ''} readOnly />
      </label>
      <div>
        <button onClick={onClose}>Close</button>
      </div>
    </div>
  );
};

const Requests = () => {
  const [requests, setRequests] = useState([]);
  const [selectedRequest, setSelectedRequest] = useState(null);
  const [viewTrackingDetails, setViewTrackingDetails] = useState(null);
  const [updateTrackingDetails, setUpdateTrackingDetails] = useState(null);
  const [documentModal, setDocumentModal] = useState({ isOpen: false, documentUrl: '' });
  const [searchQuery, setSearchQuery] = useState('');
  const [filteredRequests, setFilteredRequests] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('https://localhost:7007/api/Request');
        setRequests(response.data);
        setFilteredRequests(response.data);
      } catch (error) {
        console.error('Error fetching requests:', error);
      }
    };

    fetchData();
  }, []);

  const handleViewTrackingClick = async (requestId) => {
    try {
      const response = await axios.get(`https://localhost:7007/api/Tracking/request/${requestId}`);
      setViewTrackingDetails(response.data);
    } catch (error) {
      console.error('Error fetching tracking details:', error);
    }
  };

  const handleUpdateTrackingClick = async (requestId) => {
    try {
      const response = await axios.get(`https://localhost:7007/api/Tracking/request/${requestId}`);
      setUpdateTrackingDetails(response.data);
    } catch (error) {
      console.error('Error fetching tracking details:', error);
    }
  };

  const handleCloseViewTrackingModal = () => {
    setViewTrackingDetails(null);
  };

  const handleCloseUpdateTrackingModal = () => {
    setUpdateTrackingDetails(null);
  };

  const handleViewDocument = (documentUrl) => {
    setDocumentModal({ isOpen: true, documentUrl });
  };

  const handleCloseDocumentModal = () => {
    setDocumentModal({ isOpen: false, documentUrl: '' });
  };

  const handleSearchChange = (event) => {
    setSearchQuery(event.target.value.toLowerCase());
  };

  const handleSearchButtonClick = () => {
    const query = searchQuery.trim().toLowerCase();

    const filteredRequests = requests.filter((request) =>
      request.expenseCategory.toLowerCase().includes(query) ||
      request.username.toLowerCase().includes(query)
    );

    setFilteredRequests(filteredRequests);
  };

  return (
    <div>
      <h2>Requests</h2>
      <div className='container'>
        <input
          className="search-bar"
          type="text"
          placeholder="Search..."
          value={searchQuery}
          onChange={handleSearchChange}
        />
        <button className="search-button" onClick={handleSearchButtonClick}>
          Search
        </button>
      </div>
      <table>
        <thead>
          <tr>
            <th>Request ID</th>
            <th>Username</th>
            <th>Expense Category</th>
            <th>Amount</th>
            <th>Document</th>
            <th>Description</th>
            <th>Request Date</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {filteredRequests.map((request) => (
            <tr key={request.requestId}>
              <td>{request.requestId}</td>
              <td>{request.username}</td>
              <td>{request.expenseCategory}</td>
              <td>{request.amount}</td>
              <td><button onClick={() => handleViewDocument(request.document)} className="Button">View Document</button></td>
              <td>{request.description}</td>
              <td>{new Date(request.requestDate).toLocaleString()}</td>
              <td>
                <button
                  onClick={() => {
                    setSelectedRequest(request);
                    handleViewTrackingClick(request.requestId);
                  }}
                  className="Button"
                >
                  View Tracking
                </button>
                <button
                  onClick={() => {
                    setSelectedRequest(request);
                    handleUpdateTrackingClick(request.requestId);
                  }}
                  className="Button-warning"
                >
                  Update Tracking
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {selectedRequest && viewTrackingDetails && (
        <div className="modal">
          <div className="modal-content">
            <ViewTracking trackingDetails={viewTrackingDetails} onClose={handleCloseViewTrackingModal} />
          </div>
        </div>
      )}

      {selectedRequest && updateTrackingDetails && (
        <div className="modal">
          <div className="modal-content">
            <UpdateTracking
              requestId={selectedRequest.requestId}
              trackingDetails={updateTrackingDetails}
              onUpdateTracking={() => {
                handleCloseUpdateTrackingModal();
              }}
              onClose={handleCloseUpdateTrackingModal}
            />
          </div>
        </div>
      )}

      {documentModal.isOpen && (
        <div className="document-modal">
          <div className="document-content">
            <button className="close-btn" onClick={handleCloseDocumentModal}>
              <span>&times;</span>
            </button>
            <iframe src={documentModal.documentUrl} title="Document Viewer" width="100%" height="100%" />
          </div>
        </div>
      )}
    </div>
  );
};

export default Requests;