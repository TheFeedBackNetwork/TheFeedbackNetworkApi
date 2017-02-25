import React, { PropTypes } from 'react';
import { connect } from 'react-redux';

import Login from '../components/Login';
import Profile from '../components/Profile';
import PostRoll from '../components/PostRoll';
import Post from '../components/Post';
import { fetchToken } from '../actions/token';

import { Route, withRouter } from 'react-router-dom';
import Callback from '../components/callback';
import silentrenew from '../components/silentrenew'

class App extends React.Component {

  componentWillMount() {
     const { dispatch } = this.props;
    dispatch(fetchToken())
  }

  render() {
    return (
      <div className="page-container">
        <h1> HIII </h1>
        <Login />
        <Route exact path='/:userId/:postId' component={Post} />
        <Route exact path='/' component={PostRoll} />
        <Route exact path='/profile' component={Profile} />
        <Route exact path='/userId' component={Profile} />
        <Route exact path='/oidc-callback' component={withRouter(Callback)} />
      </div>
    )
  }
}

App.propTypes = {
  dispatch: PropTypes.func.isRequired
};

function mapStateToProps(state) {
  return {

  }
}

export default connect(mapStateToProps)(App);
