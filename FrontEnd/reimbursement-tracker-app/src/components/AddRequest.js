import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './AddRequest.css'; // Import the CSS file

const AddRequest = () => {
  const initialState = {
    expenseCategory: 'Select Category',
    amount: 0,
    document: null,
    description: '',
    requestDate: new Date().toISOString().slice(0, 10), // Set default date to today
    customExpenseCategory: '',
    agreeToPolicies: false,
    showPolicies: false,
  };
  const [requestData, setRequestData] = useState({ ...initialState})

  useEffect(() => {
    // Retrieve username from local storage
    const username = localStorage.getItem('username');
    // Set the username in the requestData state
    setRequestData((prevRequestData) => ({ ...prevRequestData, username }));
  }, []);

  const handleInputChange = (e) => {
    const { name, type, checked } = e.target;
  
    if (type === 'file') {
      const fileInput = e.target;
      const file = fileInput.files[0];
      setRequestData({ ...requestData, [name]: file });
    } else {
      setRequestData({ ...requestData, [name]: type === 'checkbox' ? checked : e.target.value });
    }
  };
  
  
  
  

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    if (!requestData.agreeToPolicies) {
      alert('Please agree to the reimbursement policies before submitting.');
      return;
    }
  
    const categoryToSubmit = requestData.customExpenseCategory || requestData.expenseCategory;
  
    try {
      const formData = new FormData();
      formData.append('expenseCategory', categoryToSubmit);
      formData.append('amount', requestData.amount);
      formData.append('document', requestData.document);
      formData.append('description', requestData.description);
      formData.append('requestDate', requestData.requestDate);
      formData.append('username', requestData.username);
  
      const response = await axios.post('https://localhost:7007/api/Request', formData);
  
      console.log('Request added successfully:', response.data);
      alert('Request added successfully');
      setRequestData({ ...initialState });
    } catch (error) {
      console.error('Error adding request:', error.response?.data);
      alert('Failed to add Request. Please try again.');
    }
  };
  

  const handleCancel = () => {
    window.history.back();
    // Add logic to handle cancellation (e.g., redirect to another page)
    console.log('Operation canceled');
  };

  const handleViewPolicies = () => {
    setRequestData({ ...requestData, showPolicies: true });
  };

  return (
    <div className="addRequestContainer">
      <h2>Add Request</h2>
      <form onSubmit={handleSubmit}>

        <label>
          Expense Category:
          <select name="expenseCategory" value={requestData.expenseCategory} onChange={handleInputChange}>
            <option value="">Select Category</option>
            <option value="Travel">Travel</option>
            <option value="Meals">Meals</option>
            <option value="Supplies">Supplies</option>
            <option value="Training">Training</option>
            <option value="Health">Health</option>
            <option value="other">Other</option>ss
          </select>
        </label>
        {requestData.expenseCategory === 'other' && (
          <label>
            Custom Expense Category:
            <input
              type="text"
              name="customExpenseCategory"
              value={requestData.customExpenseCategory}
              onChange={handleInputChange}
            />
          </label>
        )}

        <label>
          Amount:
          <input type="number" name="amount" value={requestData.amount} onChange={handleInputChange} />
        </label>

        <label>
          Document:
          <input type="file" name="document" onChange={handleInputChange} />
        </label>


        <label>
          Description:
          <textarea name="description" value={requestData.description} onChange={handleInputChange} />
        </label>

        <label>
          Request Date:
          <input type="date" name="requestDate" value={requestData.requestDate} onChange={handleInputChange} />
        </label>

        <div className="policies">
          <h3>Policy Compliance:</h3>
          {requestData.showPolicies && (
            <ul>
              <li>Expense Categories: Clearly specify the category of your expense from the provided options (e.g., Travel, Meals, Supplies, Training, Health, Other).</li>
              <li>Documentation: Attach all relevant receipts and documents supporting your expenses. Ensure they are clear, legible, and itemized.</li>
              <li>Amount Limits: Adhere to the specified limits for each expense category. Any request exceeding the limit may require additional justification.</li>
              <li>Custom Expense Category: If your expense doesn't fit into the predefined categories, select "Other" and provide details in the custom expense category field.</li>
              <li>Currency: Clearly indicate the currency used for your expenses.</li>
              <li>Policy Compliance: Ensure your reimbursement request complies with company policies and guidelines. Check for any policy violations before submitting the request.</li>
              <li>Description: Provide a detailed description of each expense, explaining its necessity and relevance to company activities.</li>
              <li>Request Date: Specify the date of the reimbursement request.</li>
              <li>Approval Process: Reimbursement requests will undergo an approval process. Ensure all information is accurate to avoid delays.</li>
              <li>Agreement: By submitting a reimbursement request, you confirm that the provided information is accurate and complies with company policies.</li>
              <li>Policy Updates: Stay informed about any updates or changes to the reimbursement policies.</li>
              <li>These policies aim to ensure transparency, accuracy, and compliance with company guidelines when submitting reimbursement requests for expenses. Adjust them according to your organization's specific needs and policies.</li>
              {/* Add other policy points ... */}
            </ul>
          )}
          <button type="button" onClick={handleViewPolicies} className="viewPoliciesButton">
            Click to View Policies
          </button>
        </div>

        <label className="checkbox-label">
          <input
            type="checkbox"
            name="agreeToPolicies"
            checked={requestData.agreeToPolicies}
            onChange={handleInputChange}
          />
         <p className='para'> I agree to the reimbursement policies </p>
        </label>

        <div>
          <button type="submit">Add Request</button>
          <button type="button" onClick={handleCancel}>Cancel</button>
        </div>
      </form>
    </div>
  );
};

export default AddRequest;