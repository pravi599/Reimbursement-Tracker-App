import React, { useState } from 'react';
import axios from 'axios';
import './AddPayments.css';

const AddPayment = () => {
  const [paymentData, setPaymentData] = useState({
    RequestId: 0,
    CardNumber: '',
    ExpiryDate: '',
    CVV: '',
    PaymentAmount: 0,
    PaymentDate: new Date().toISOString().split('T')[0], // Set the current date as the default
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setPaymentData({ ...paymentData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('https://localhost:7007/api/PaymentDetails', paymentData);

      console.log('Payment added successfully:', response.data);
      alert('Payment added successfully');
    } catch (error) {
      console.error('Error adding payment:', error.response.data);
      alert('Failed to add payment. Please try again.');
    }
  };

 // ...

return (
  <div className='container'>
      <h2 className='heading'>Add Payment</h2>
    <div className='form-wrapper'>
    
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="RequestId">Request ID:</label>
          <input
            type="number"
            id="RequestId"
            name="RequestId"
            value={paymentData.RequestId}
            onChange={handleInputChange}
          />
        </div>

        <div>
          <label htmlFor="CardNumber">Card Number:</label>
          <input
            type="text"
            id="CardNumber"
            name="CardNumber"
            value={paymentData.CardNumber}
            onChange={handleInputChange}
          />
        </div>

        <div>
          <label htmlFor="ExpiryDate">Expiry Date:</label>
          <input
            type="text"
            id="ExpiryDate"
            name="ExpiryDate"
            value={paymentData.ExpiryDate}
            onChange={handleInputChange}
          />
        </div>

        <div>
          <label htmlFor="CVV">CVV:</label>
          <input
            type="text"
            id="CVV"
            name="CVV"
            value={paymentData.CVV}
            onChange={handleInputChange}
          />
        </div>

        <div>
          <label htmlFor="PaymentAmount">Payment Amount:</label>
          <input
            type="number"
            id="PaymentAmount"
            name="PaymentAmount"
            value={paymentData.PaymentAmount}
            onChange={handleInputChange}
          />
        </div>

        <div>
          <label htmlFor="PaymentDate">Payment Date:</label>
          <input
            type="date"
            id="PaymentDate"
            name="PaymentDate"
            value={paymentData.PaymentDate}
            onChange={handleInputChange}
          />
        </div>

        <button type="submit">Add Payment</button>
      </form>
    </div>
  </div>
);

};

export default AddPayment;