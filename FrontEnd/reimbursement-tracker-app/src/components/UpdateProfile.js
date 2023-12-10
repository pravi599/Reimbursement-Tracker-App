import React, { useState, useEffect } from "react";
import axios from "axios";
import './UpdateProfile.css';

function UpdateProfile ({ userProfile, onClose })  {
    const [userId, setUserId] = useState("");
    const [username, setUsername] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [city, setCity] = useState("");
    const [contactNumber, setContactNumber] = useState("");
    const [bankAccountNumber, setBankAccountNumber] = useState("");

    useEffect(() => {
        // Set the state with the Profile details when the component mounts
        setUserId(userProfile.userId);
        setUsername(userProfile.username);
        setFirstname(userProfile.firstName);
        setLastname(userProfile.lastName);
        setCity(userProfile.city);
        setContactNumber(userProfile.contactNumber);
        setBankAccountNumber(userProfile.bankAccountNumber);
    }, [userProfile]);

    const updateProfile = () => {
        const updatedProfile = {
            "userId": userId,
            "username": username,
            "firstName": firstname,
            "lastName": lastname,
            "city": city,
            "contactNumber": contactNumber,
            "bankAccountNumber": bankAccountNumber
        };

        axios.put(`https://localhost:7007/api/UserProfile/`, updatedProfile)
            .then(response => {
                console.log(response.data);
                alert("User Profile Updated Successfully");
                // onClose(); // Close the modal after updating
            })
            .catch(error => {
                console.error("Error updating User Profile:", error);
                alert("Failed to update User Profile");
                console.log(error.response?.data);
            });
    }

    const cancelUpdate = () => {
        window.history.back();
    }

    return (
        <div className="modal-container">
            <div className="modal-content">
                <span className="close" onClick={onClose}>&times;</span>
                <h2>Update User Profile</h2>

                <label htmlFor="usrId">User Id</label>
                <input id="usrId" type="text" value={userId} readOnly />

                <label htmlFor="username">Username</label>
                <input id="username" type="text" value={username} readOnly />

                <label htmlFor="firstname">First Name</label>
                <input id="firstname" type="text" value={firstname} onChange={(e) => setFirstname(e.target.value)} />

                <label htmlFor="lastname">Last Name</label>
                <input id="lastname" type="text" value={lastname} onChange={(e) => setLastname(e.target.value)} />

                <label htmlFor="city">City</label>
                <textarea id="city" value={city} onChange={(e) => setCity(e.target.value)} />

                <label htmlFor="contactNumber">Contact Number</label>
                <input id="contactNumber" type="phone" value={contactNumber} onChange={(e) => setContactNumber(e.target.value)} />

                <label htmlFor="bankAccountNumber">Bank Account Number</label>
                <input id="bankAccountNumber" type="acc" value={bankAccountNumber} onChange={(e) => setBankAccountNumber(e.target.value)} />

                <div className="button-container">
                    <button onClick={updateProfile} className="btn btn-primary">Update Profile</button>
                    <button onClick={cancelUpdate} className="btn btn-primary">Go Back</button>
                </div>
            </div>
        </div>
    );
}

export default UpdateProfile;