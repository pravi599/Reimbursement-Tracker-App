import React, { useState } from "react";
import axios from "axios";
import './DeleteProfile.css'; // Import your CSS file for styling

const DeleteProfile = () => {
    const [usernameToDelete, setUsernameToDelete] = useState("");
    const [deleteResult, setDeleteResult] = useState(null);

    const handleChange = (e) => {
        setUsernameToDelete(e.target.value);
        setDeleteResult(null); // Clear previous delete result when username changes
    };

    const handleDelete = async () => {
        try {
            await axios.delete(`https://localhost:7007/api/UserProfile/${usernameToDelete}`);
            setDeleteResult({ success: true, message: "Profile deleted successfully." });
        } catch (error) {
            setDeleteResult({ success: false, message: "Error deleting profile. Please try again." });
            console.error(error);
        }
    };

    return (
        <div className="deleteProfileContainer">
            <div className="deleteProfileBox">
                <h2 className="deleteProfileHeader">Delete Profile</h2>
                <div className="deleteProfileForm">
                    <div className="fieldContainer">
                        <label className="fieldLabel" htmlFor="usernameToDelete">Username to Delete</label>
                        <input
                            type="text"
                            id="usernameToDelete"
                            className="form-control"
                            name="usernameToDelete"
                            value={usernameToDelete}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group mt-3">
                        <button className="btn btn-danger button" onClick={handleDelete}>
                            Delete Profile
                        </button>
                    </div>

                    {deleteResult && (
                        <div className={`alert ${deleteResult.success ? 'alert-success' : 'alert-danger'} mt-3`}>
                            {deleteResult.message}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default DeleteProfile;