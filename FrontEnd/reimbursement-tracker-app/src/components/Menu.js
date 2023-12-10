import React, { useState, useEffect } from 'react';
import { NavLink, useLocation, useNavigate } from 'react-router-dom';
import { FaBars } from 'react-icons/fa';
import './Menu.css';

const Menu = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [isProfileDropdownOpen, setProfileDropdownOpen] = useState(false);
  const [isPaymentDropdownOpen, setPaymentDropdownOpen] = useState(false);
  const [isRequestDropdownOpen, setRequestDropdownOpen] = useState(false);
  const [isLoggedIn, setLoggedIn] = useState(false);
  const [isMenuOpen, setMenuOpen] = useState(false);
  const userRole = localStorage.getItem('role');

  useEffect(() => {
    const token = localStorage.getItem('token');
    setLoggedIn(!!token);
  }, []);

  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    setLoggedIn(false);
    navigate('/Login');
  };

  const toggleProfileDropdown = () => {
    setProfileDropdownOpen(!isProfileDropdownOpen);
    setPaymentDropdownOpen(false);
    setRequestDropdownOpen(false);
  };

  const togglePaymentDropdown = () => {
    setPaymentDropdownOpen(!isPaymentDropdownOpen);
    setProfileDropdownOpen(false);
    setRequestDropdownOpen(false);
  };

  const toggleRequestDropdown = () => {
    setRequestDropdownOpen(!isRequestDropdownOpen);
    setProfileDropdownOpen(false);
    setPaymentDropdownOpen(false);
  };

  const toggleMenu = () => {
    setMenuOpen(!isMenuOpen);
  };

  return (
    <div className="header">
      <div className="center-container">
        <div className="nav-links">
          <div className="menu-icon" onClick={toggleMenu}>
            <FaBars />
          </div>

          {isMenuOpen && (
            <>
              {!isLoggedIn ? (
                <NavLink to="/Login" className={`nav-link ${location.pathname === '/Login' ? 'active-link' : ''}`}>
                  Login
                </NavLink>
              ) : (
                <>
                  <NavLink to="/Home" className={`nav-link ${location.pathname === '/Home' ? 'active-link' : ''}`}>
                    Home
                  </NavLink>

                  {userRole === 'Employee' && (
                    <>
                      <div className="profile-dropdown">
                        <div className={`nav-link ${isProfileDropdownOpen ? 'active-link' : ''}`} onClick={toggleProfileDropdown}>
                          Profiles
                        </div>
                        {isProfileDropdownOpen && (
                          <div className="dropdown-content">
                            <NavLink
                              to="/add-profile"
                              className={`nav-link ${location.pathname === '/add-profile' ? 'active-link' : ''}`}
                            >
                              Add Profile
                            </NavLink>
                            <NavLink
                              to="/user-profile"
                              className={`nav-link ${location.pathname === '/user-profile' ? 'active-link' : ''}`}
                            >
                              User Profile
                            </NavLink>
                          </div>
                        )}
                      </div>

                      <div className="request-dropdown">
                        <div className={`nav-link ${isRequestDropdownOpen ? 'active-link' : ''}`} onClick={toggleRequestDropdown}>
                          Requests
                        </div>
                        {isRequestDropdownOpen && (
                          <div className="dropdown-content">
                            <NavLink
                              to="/add-request"
                              className={`nav-link ${location.pathname === '/add-request' ? 'active-link' : ''}`}
                            >
                              Add Request
                            </NavLink>
                            <NavLink
                              to="/UserRequests"
                              className={`nav-link ${location.pathname === '/UserRequests' ? 'active-link' : ''}`}
                            >
                              User Requests
                            </NavLink>
                          </div>
                        )}
                      </div>

                      <NavLink to="/Trackings" className={`nav-link ${location.pathname === '/Trackings' ? 'active-link' : ''}`}>
                        Trackings
                      </NavLink>
                    </>
                  )}

                  {userRole === 'HR' && (
                    <>
                      <NavLink
                        to="/user-profiles"
                        className={`nav-link ${location.pathname === '/user-profiles' ? 'active-link' : ''}`}
                      >
                        User Profiles
                      </NavLink>

                      <NavLink to="/requests" className={`nav-link ${location.pathname === '/requests' ? 'active-link' : ''}`}>
                        View All Requests
                      </NavLink>

                      <div className="payment-dropdown">
                        <div className={`nav-link ${isPaymentDropdownOpen ? 'active-link' : ''}`} onClick={togglePaymentDropdown}>
                          Payments
                        </div>
                        {isPaymentDropdownOpen && (
                          <div className="dropdown-content">
                            <NavLink
                              to="/AddPayment"
                              className={`nav-link ${location.pathname === '/AddPayment' ? 'active-link' : ''}`}
                            >
                              Make Payment
                            </NavLink>
                            <NavLink
                              to="/GetPayments"
                              className={`nav-link ${location.pathname === '/GetPayments' ? 'active-link' : ''}`}
                            >
                              View Payments
                            </NavLink>
                          </div>
                        )}
                      </div>
                    </>
                  )}

                  <div className="nav-link logout-button" onClick={logout}>
                    Logout
                  </div>
                </>
              )}
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default Menu;
