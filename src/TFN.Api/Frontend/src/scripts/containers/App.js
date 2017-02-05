import React, { PropTypes } from 'react';
import Login from '../components/Login';
import { Route, withRouter } from 'react-router-dom';
import Callback from '../components/callback';
import silentrenew from '../components/silentrenew'

const App = (props) => (
  <div className="page-container">
    <h1> HIII </h1>
    <Login />
    <Route exact path='/oidc-callback' component={withRouter(Callback)} />
    <Route exact path='/silentrenew' component={silentrenew} />

  </div>
);

App.propTypes = {
  //children: PropTypes.element.isRequired
};

export default App;
