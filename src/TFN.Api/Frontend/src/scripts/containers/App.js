import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';

import Login from '../components/Login';
import UserAgentContainer from './UserAgentContainer'
import Profile from '../components/Profile';
import MyProfileContainer from './MyProfileContainer';
import ProfileContainer from './ProfileContainer';
import UploadContainer from './UploadContainer';
import PostRoll from '../components/PostRoll';
import Post from '../components/Post';
import TFNEditor from '../components/TFNEditor';
import WaveformContainer from './WaveformContainer';
import { changeLocation } from '../actions/location'
import NavigationContainer from './NavigationContainer'

import { Route, withRouter, Switch } from 'react-router-dom';
import Callback from '../components/callback';
import silentrenew from '../components/silentrenew'

class App extends React.Component {

  render() {
    return (
        <div className="page-container">
          <NavigationContainer />
          <Login />
          <UserAgentContainer />           
          <Route exact path='/:userId/:postId' component={Post} />
          <Route exact path='/' component={TFNEditor} />
          <Route exact path='/' component={WaveformContainer} />
          <Route exact path='/' component={WaveformContainer} />
          <Route exact path='/' component={PostRoll} />
          <Switch>
            <Route exact path='/profile' component={MyProfileContainer} />
            <Route exact path='/upload' component={UploadContainer} />
            <Route exact path='/:userId' component={ProfileContainer} />
          </Switch>
          <Route exact path='/oidc-callback' component={withRouter(Callback)} /> 
        </div>
    )
  }
}

App.propTypes = {
  location: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  const { location } = state.location
  return {
    location: location
  }
}

export default connect(mapStateToProps)(withRouter(App));
