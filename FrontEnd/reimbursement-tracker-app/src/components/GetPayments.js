import React, { useState } from 'react';
import axios from 'axios';
import './GetPayments.css'
const GetPayments = () => {
  // State variables
  const [paymentData, setPaymentData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // Function to fetch payment details
  const fetchPaymentDetails = async () => {
    try {
      setLoading(true);
      setError(null);

      // API request to get payment details
      const response = await axios.get('https://localhost:7007/api/PaymentDetails');
      console.log('Response:', response.data);

      // Check if the response has a 'data' property
      if (response && response.data) {
        setPaymentData(response.data);
      } else {
        setError('Invalid response format');
      }
    } catch (error) {
      // Handle errors during the API request
      console.error('Error fetching payment details:', error.response ? error.response.data : error.message);
      setError('Failed to fetch payment details. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  // JSX for rendering the component
  return (
    <div>
      <h2>Payment Details</h2>

      <button onClick={fetchPaymentDetails} disabled={loading} className='btnn'>
        Get All Payments
      </button>

      {loading && <p>Loading payment details...</p>}

      {error && <p style={{ color: 'red' }}>{error}</p>}

      <table>
        <thead>
          <tr>
            <th>Payment ID</th>
            <th>Request ID</th>
            <th>Card Number</th>
            <th>Expiry Date</th>
            <th>CVV</th>
            <th>Payment Amount</th>
            <th>Payment Date</th>
          </tr>
        </thead>
        <tbody>
          {paymentData.map((payment) => (
            <tr key={payment.paymentId}>
              <td>{payment.paymentId}</td>
              <td>{payment.requestId}</td>
              <td>{payment.cardNumber}</td>
              <td>{payment.expiryDate}</td>
              <td>{payment.cvv}</td>
              <td>{payment.paymentAmount}</td>
              <td>{payment.paymentDate}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default GetPayments;