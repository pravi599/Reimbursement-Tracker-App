// AddProfile.js
import React, { useState,useEffect } from 'react';
import axios from 'axios';
import './AddProfile.css';

const AddProfile = () => {
  // Get username from local storage
  const storedUsername = localStorage.getItem('username');

  const [profileData, setProfileData] = useState({
    firstName: '',
    lastName: '',
    city: '',
    contactNumber: '',
    bankAccountNumber: '',
    // Remove the 'username' field
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setProfileData({ ...profileData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('https://localhost:7007/api/UserProfile', profileData);

      console.log('Profile added successfully:', response.data);
      alert('Profile added successfully');
    } catch (error) {
      console.error('Error adding profile:', error.response.data);
      alert('Failed to add profile. Please try again.');
    }
  };

  const handleCancel = () => {
    window.history.back();
    // Add logic to handle cancellation (e.g., redirect to another page)
    console.log('Operation canceled');
  };

  // Set the 'username' field using the value retrieved from local storage
  useEffect(() => {
    setProfileData((prevData) => ({
      ...prevData,
      username: storedUsername || '',
    }));
  }, [storedUsername]);
  return (
    <div className="addProfileContainer">
        <h2 className="addProfileHeader">Add Profile</h2>
      <div className="addProfileBox">
      
        <form className="addProfileForm" onSubmit={handleSubmit}>
          <div className="fieldContainer">
            <label className="fieldLabel" htmlFor="username">
              Username:
            </label>
            <input
              type="text"
              id="username"
              name="username"
              value={profileData.username}
              onChange={handleInputChange}
              className="form-control"
              readOnly
            />
          </div>

          <div className="fieldContainer">
            <label className="fieldLabel" htmlFor="firstName">
              First Name:
            </label>
            <input
              type="text"
              id="firstName"
              name="firstName"
              value={profileData.firstName}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>

          <div className="fieldContainer">
            <label className="fieldLabel" htmlFor="lastName">
              Last Name:
            </label>
            <input
              type="text"
              id="lastName"
              name="lastName"
              value={profileData.lastName}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>

          <div className="fieldContainer">
            <label className="fieldLabel" htmlFor="city">
              City:
            </label>
            <input
              type="text"
              id="city"
              name="city"
              value={profileData.city}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>

          <div className="fieldContainer">
            <label className="fieldLabel" htmlFor="contactNumber">
              Contact Number:
            </label>
            <input
              type="text"
              id="contactNumber"
              name="contactNumber"
              value={profileData.contactNumber}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>

          <div className="fieldContainer">
            <label className="fieldLabel" htmlFor="bankAccountNumber">
              Bank Account Number:
            </label>
            <input
              type="text"
              id="bankAccountNumber"
              name="bankAccountNumber"
              value={profileData.bankAccountNumber}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>

          <div className="buttonContainer">
            <button type="submit" className="button btn-primary">
              Add Profile
            </button>
            <button type="button" onClick={handleCancel} className="button btn-danger">
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AddProfile;