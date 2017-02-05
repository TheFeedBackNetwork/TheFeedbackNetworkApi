import React, { PropTypes } from 'react';
import Login from '../components/Login';

const App = (props) => (
  <div className="page-container">
    <h1> HIII </h1>
    <Login />
    
  </div>
);

App.propTypes = {
  //children: PropTypes.element.isRequired
};

export default App;
