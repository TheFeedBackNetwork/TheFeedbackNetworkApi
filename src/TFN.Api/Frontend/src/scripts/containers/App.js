import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';

import Login from '../components/Login';
import Profile from '../components/Profile';
import PostRoll from '../components/PostRoll';
import Post from '../components/Post';
import TFNEditor from '../components/TFNEditor';
import WaveformContainer from './WaveformContainer';
import { changeLocation } from '../actions/location'

import { Route, withRouter } from 'react-router-dom';
import Callback from '../components/callback';
import silentrenew from '../components/silentrenew'

class App extends React.Component {

  componentWillReceiveProps() {

  }

  render() {
    return (
        <div className="page-container">
          <h1>  </h1>
          <Login />
          <Route exact path='/:userId/:postId' component={Post} />
          <Route exact path='/' component={TFNEditor} />
          <Route exact path='/' component={WaveformContainer} />
          <Route exact path='/' component={WaveformContainer} />
          <Route exact path='/' component={PostRoll} />
          <Route exact path='/profile' component={Profile} />
          <Route exact path='/:userId' component={Profile} />
          <Route exact path='/oidc-callback' component={withRouter(Callback)} />
        </div>
    )
  }
}

App.propTypes = {
  dispatch: PropTypes.func.isRequired,
  location: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  const { location } = state.location
  return {
    location: location
  }
}

export default connect(mapStateToProps)(withRouter(App));
