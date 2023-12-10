import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './TrackingComponent.css'; // Import the CSS file

const TrackingComponent = () => {
  const [trackings, setTrackings] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const storedUsername = localStorage.getItem('username');

    if (!storedUsername) {
      console.error('Username not found in localStorage');
      return;
    }

    const fetchData = async () => {
      try {
        const response = await axios.get(`https://localhost:7007/api/Tracking/user/${storedUsername}`);
        setTrackings(response.data);
      } catch (error) {
        console.error('Error fetching tracking data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="trackingContainer">
      <h2>Trackings</h2>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="tableContainer">
          <table className="trackingTable">
            <thead>
              <tr>
                <th>Tracking ID</th>
                <th>Request ID</th>
                <th>Status</th>
                <th>Approval Date</th>
                <th>Reimbursement Date</th>
              </tr>
            </thead>
            <tbody>
              {trackings.map((tracking) => (
                <tr key={tracking.trackingId}>
                  <td>{tracking.trackingId}</td>
                  <td>{tracking.requestId}</td>
                  <td>{tracking.trackingStatus}</td>
                  <td>{tracking.approvalDate || 'N/A'}</td>
                  <td>{tracking.reimbursementDate || 'N/A'}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default TrackingComponent;
