// Home.js
import './Home.css'
import React from 'react';
import { Link } from 'react-router-dom';

import './Home.css';

const Home = () => {
  return (
    <div className="home-container">
      <header>
        <h1>Welcome to Reimbursement Tracker</h1>
        <p>Efficiently manage and track employee reimbursements with our app.</p>
      </header>

      <section className="features">
        <div className="feature">
          <h2>Easy Tracking</h2>
          <p>Effortlessly track and manage employee reimbursements in one place.</p>
        </div>
        <div className="feature">
          <h2>Quick Submission</h2>
          <p>Submit reimbursement requests with just a few clicks.</p>
        </div>
        <div className="feature">
          <h2>Transparent Process</h2>
          <p>Keep the reimbursement process transparent with real-time updates.</p>
        </div>
      </section>
    </div>
  );
};

export default Home;