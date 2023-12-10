import React, { useState } from 'react';
import axios from 'axios';

const UpdateTracking = ({ requestId, trackingDetails, onUpdateTracking, onClose }) => {
  const [trackingStatus, setTrackingStatus] = useState(trackingDetails.trackingStatus || '');
  const [approvalDate, setApprovalDate] = useState(trackingDetails.approvalDate || '');
  const [reimbursementDate, setReimbursementDate] = useState(trackingDetails.reimbursementDate || '');

  const handleUpdate = async () => {
    const updatedTracking = {
      trackingId: trackingDetails.trackingId,
      requestId: requestId,
      trackingStatus: trackingStatus,
      approvalDate: approvalDate,
      reimbursementDate: reimbursementDate,
    };

    try {
      // Make the API call to update the tracking status
      const response = await axios.put(`https://localhost:7007/api/Tracking`, updatedTracking);

      // Handle the response (log, show a message, etc.)
      console.log(response.data);
      alert('Tracking Status Updated Successfully');

      // Close the modal after updating
      onClose();
    } catch (error) {
      // Handle errors (log, show an error message, etc.)
      console.error('Error updating tracking:', error);
      alert('Failed to update tracking');
      console.log(error.response?.data);
    }
  };

  return (
    <div>
      <h2>Update Tracking</h2>

      <label>
        Tracking Status:
        <select
          name="trackingStatus"
          value={trackingStatus}
          onChange={(e) => setTrackingStatus(e.target.value)}
        >
          <option value="">Select Status</option>
          <option value="Rejected">Rejected</option>
          <option value="Approved">Approved</option>
          <option value="Verified">Verified</option>
        </select>
      </label>

      <label>
        Approval Date:
        <input
          type="date"
          name="approvalDate"
          value={approvalDate}
          onChange={(e) => setApprovalDate(e.target.value)}
        />
      </label>

      <label>
        Reimbursement Date:
        <input
          type="date"
          name="reimbursementDate"
          value={reimbursementDate}
          onChange={(e) => setReimbursementDate(e.target.value)}
        />
      </label>

      <div>
        <button className="btn-primary" onClick={handleUpdate}>Update Tracking</button>
        <button className="btn-danger" onClick={onClose}>Cancel</button>
      </div>
    </div>
  );
};

export default UpdateTracking;
