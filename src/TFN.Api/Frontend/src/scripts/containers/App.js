import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';

import Login from '../components/Login';
import Profile from '../components/Profile';
import PostRoll from '../components/PostRoll';
import Post from '../components/Post';
import { changeLocation } from '../actions/location'

import { Route, withRouter } from 'react-router-dom';
import Callback from '../components/callback';
import silentrenew from '../components/silentrenew'

class App extends React.Component {

  onLocationChange(location) {
    const { dispatch } = this.props;
    console.log(location)
    dispatch(changeLocation(location))
  }

  render() {
    var router = <Router> </Router>
    console.log(router)
    return (
      <Router
      location={this.props.location}
      onUpdate={this.onLocationChange}
      >
        <div className="page-container">
          <h1> HIII </h1>
          <Login />
          <Route exact path='/:userId/:postId' component={Post} />
          <Route exact path='/' component={PostRoll} />
          <Route exact path='/profile' component={Profile} />
          <Route exact path='/userId' component={Profile} />
          <Route exact path='/oidc-callback' component={withRouter(Callback)} />
        </div>
      </Router>
    )
  }
}

App.propTypes = {
  dispatch: PropTypes.func.isRequired,
  location: PropTypes.string.isRequired
};

function mapStateToProps(state) {
  const { location } = state.location
  return {
    location: location
  }
}

export default connect(mapStateToProps)(App);
