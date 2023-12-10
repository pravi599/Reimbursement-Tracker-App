import React, { useState, useEffect } from "react";
import axios from "axios";
import UpdateProfile from "./UpdateProfile";
import DeleteProfile from "./DeleteProfile";
import './UserProfile.css';

const UserProfile = ({ match }) => {
    const [userProfile, setUserProfile] = useState({
        userId: null,
        username: "",
        firstName: "",
        lastName: "",
        city: "",
        contactNumber: "",
        bankAccountNumber: ""
    });

    const [isEditing, setIsEditing] = useState(false);
    const [isDeleting, setIsDeleting] = useState(false);

    const [updateResult, setUpdateResult] = useState(null);
    const [deleteResult, setDeleteResult] = useState(null);

    useEffect(() => {
        const fetchUserProfile = async () => {
            try {
                const username = localStorage.getItem('username');
                const response = await axios.get(`https://localhost:7007/api/UserProfile/username/${username}`);
                setUserProfile(response.data);
            } catch (error) {
                console.error("Error fetching user profile:", error);
            }
        };

        fetchUserProfile();
    }, []); // Empty dependency array to run the effect only once

    const handleEditToggle = () => {
        setIsEditing(!isEditing);
        setUpdateResult(null);
    };

    const handleDeleteToggle = () => {
        setIsDeleting(!isDeleting);
        setDeleteResult(null);
    };

    const handleUpdate = async (updatedProfile) => {
        try {
            await axios.put("https://localhost:7007/api/UserProfile", updatedProfile);
            setUpdateResult({ success: true, message: "Profile updated successfully." });
            setIsEditing(false);
        } catch (error) {
            setUpdateResult({ success: false, message: "Error updating profile. Please try again." });
            console.error(error);
        }
    };

    const handleDelete = async () => {
        try {
            const username = localStorage.getItem('username');
            await axios.delete(`https://localhost:7007/api/UserProfile/${userProfile.username}`);
            setDeleteResult({ success: true, message: "Profile deleted successfully." });
        } catch (error) {
            setDeleteResult({ success: false, message: "Error deleting profile. Please try again." });
            console.error(error);
        }
    };

    return (
        <div className="userProfileContainer">
            <h2 className="userProfileHeader">User Profile</h2>
            <div className="userProfileBox">
                <div className="userProfileDetails">
                    <p>
                        <strong>User ID:</strong> {userProfile.userId}
                    </p>
                    <p>
                        <strong>Username:</strong> {userProfile.username}
                    </p>
                    <p>
                        <strong>First Name:</strong> {isEditing ? (
                            <input
                                type="text"
                                name="firstName"
                                value={userProfile.firstName}
                                onChange={(e) => setUserProfile({ ...userProfile, firstName: e.target.value })}
                            />
                        ) : (
                            userProfile.firstName
                        )}
                    </p>
                    <p>
                        <strong>Last Name:</strong> {isEditing ? (
                            <input
                                type="text"
                                name="lastName"
                                value={userProfile.lastName}
                                onChange={(e) => setUserProfile({ ...userProfile, lastName: e.target.value })}
                            />
                        ) : (
                            userProfile.lastName
                        )}
                    </p>
                    <p>
                        <strong>City:</strong> {isEditing ? (
                            <input
                                type="text"
                                name="city"
                                value={userProfile.city}
                                onChange={(e) => setUserProfile({ ...userProfile, city: e.target.value })}
                            />
                        ) : (
                            userProfile.city
                        )}
                    </p>
                    <p>
                        <strong>Contact Number:</strong> {isEditing ? (
                            <input
                                type="text"
                                name="contactNumber"
                                value={userProfile.contactNumber}
                                onChange={(e) => setUserProfile({ ...userProfile, contactNumber: e.target.value })}
                            />
                        ) : (
                            userProfile.contactNumber
                        )}
                    </p>
                    <p>
                        <strong>Bank Account Number:</strong> {isEditing ? (
                            <input
                                type="text"
                                name="bankAccountNumber"
                                value={userProfile.bankAccountNumber}
                                onChange={(e) => setUserProfile({ ...userProfile, bankAccountNumber: e.target.value })}
                            />
                        ) : (
                            userProfile.bankAccountNumber
                        )}
                    </p>
                </div>

                <div className="form-group mt-3">
                    {isEditing ? (
                        <>
                            <button className="btn btn-primary button" onClick={() => handleUpdate(userProfile)}>
                                Save Changes
                            </button>
                            <button className="btn btn-danger button ml-2" onClick={handleEditToggle}>
                                Cancel
                            </button>
                        </>
                    ) : (
                        <>
                            <button className="btn btn-primary button" onClick={handleEditToggle}>
                                Update Profile
                            </button>
                            <button className="btn btn-danger button ml-2" onClick={handleDeleteToggle}>
                                Delete Profile
                            </button>
                        </>
                    )}
                </div>

                {updateResult && (
                    <div className={`alert ${updateResult.success ? 'alert-success' : 'alert-danger'} mt-3`}>
                        {updateResult.message}
                    </div>
                )}

                {isDeleting && (
                    <DeleteProfile
                        username={userProfile.username}
                        onDelete={handleDelete}
                        onCancel={handleDeleteToggle}
                    />
                )}

                {deleteResult && (
                    <div className={`alert ${deleteResult.success ? 'alert-success' : 'alert-danger'} mt-3`}>
                        {deleteResult.message}
                    </div>
                )}
            </div>

            {isEditing && <UpdateProfile userProfile={userProfile} onUpdate={handleUpdate} />}
        </div>
    );
};

export default UserProfile;