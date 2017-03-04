import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';

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
import Callback from '../components/Callback';
import silentrenew from '../components/silentrenew'

class App extends React.Component {

  componentWillUpdate() {
    console.log('up')
    document.body.style.background= '#19192d url(/images/background.jpg) no-repeat center center fixed;'
  }

  render() {
    return (
        <div className="page-container">
          <NavigationContainer />
          <UserAgentContainer />           
          <Route exact path='/:userId/:postId' component={Post} />
          <Route exact path='/' component={TFNEditor} />
          <Route exact path='/' component={WaveformContainer} />
          <Route exact path='/' component={WaveformContainer} />
          {/*<Route exact path='/' component={PostRoll} />*/}
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
  
  location: PropTypes.object.isRequired,
  isLoadingUser: PropTypes.bool.isRequired,
  fetchingToken: PropTypes.bool.isRequired,
  principleType: PropTypes.string.isRequired
  
};

function mapStateToProps(state) {
  const { location } = state.location
  const { isLoadingUser } = state.auth
  const { fetchingToken } = state.token
  const { principleType } = state.token
  return {
    location: location,
    isLoadingUser: isLoadingUser,
    principleType: principleType,
    fetchingToken: fetchingToken
  }
}

export default connect(mapStateToProps)(withRouter(App));
