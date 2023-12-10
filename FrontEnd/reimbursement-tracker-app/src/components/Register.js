// Register.js
import React, { useState } from "react";
import axios from "axios";
import './Register.css';
import { Link,NavLink} from "react-router-dom";

const roles = ["Select Role", "HR", "Employee"];

const Register = () => {
    const initialFormData = {
        username: "",
        password: "",
        confirmPassword: "",
        role: "Select Role"
    };

    const [formData, setFormData] = useState(initialFormData);
    const [errors, setErrors] = useState({});
    const [registrationResult, setRegistrationResult] = useState(null);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
        setErrors({ ...errors, [e.target.name]: "" });
    };

    const checkUserData = () => {
        const { username, password, confirmPassword, role } = formData;
        const newErrors = {};

        if (!username) newErrors.username = "Username cannot be empty";
        if (!password) newErrors.password = "Password cannot be empty";
        if (!confirmPassword) newErrors.confirmPassword = "Confirm Password cannot be empty";
        if (password !== confirmPassword) {
            newErrors.password = "Passwords do not match";
            newErrors.confirmPassword = "Passwords do not match";
        }
        if (role === "Select Role") newErrors.role = "Please select a role";

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const signUp = async (event) => {
        event.preventDefault();

        if (!checkUserData()) return;

        try {
            await axios.post("https://localhost:7007/api/User", formData);
            setRegistrationResult({ success: true, message: "Registration successful!" });

        } catch (err) {
            setRegistrationResult({ success: false, message: "Registration failed. Please try again." });

            console.log(err);
        } finally {
            setFormData(initialFormData);
        }
    };

    return (
        <div className="registerContainer">
            <div className="registerBox">
                <h2 className="registerHeader">Registration</h2>
                <form className="registerForm" noValidate>
                    <div className="fieldContainer">
                        <label className="fieldLabel" htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            className="form-control"
                            name="username"
                            value={formData.username}
                            onChange={handleChange}
                        />
                        {errors.username && <span className="error">{errors.username}</span>}
                    </div>

                    <div className="fieldContainer">
                        <label className="fieldLabel" htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            className="form-control passwordInput"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                        />
                        {errors.password && <span className="error">{errors.password}</span>}
                    </div>

                    <div className="fieldContainer">
                        <label className="fieldLabel" htmlFor="confirmPassword">Confirm Password</label>
                        <input
                            type="password"
                            id="confirmPassword"
                            className="form-control passwordInput"
                            name="confirmPassword"
                            value={formData.confirmPassword}
                            onChange={handleChange}
                        />
                        {errors.confirmPassword && <span className="error">{errors.confirmPassword}</span>}
                    </div>

                    <div className="fieldContainer">
                        <label className="fieldLabel" htmlFor="role">Role</label>
                        <select
                            id="role"
                            className="form-control"
                            name="role"
                            value={formData.role}
                            onChange={handleChange}
                        >
                            {roles.map((r) => (
                                <option value={r} key={r}>
                                    {r}
                                </option>
                            ))}
                        </select>
                        {errors.role && <span className="error">{errors.role}</span>}
                    </div>

                    {registrationResult && (
                        <div className={`alert ${registrationResult.success ? 'alert-success' : 'alert-danger'} mt-3`}>
                            {registrationResult.message}
                        </div>
                    )}

                    <div className="form-group mt-3">
                        <button className="btn btn-primary button" onClick={signUp}>
                            Sign Up
                        </button>
                        <button className="btn btn-danger button ml-2">Cancel</button>
                    </div>

                    <div className="login-link">
                        <p>
                            Already have an account? 
                            <Link to="/login"> Login here</Link>.
                        </p>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Register;