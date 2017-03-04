import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import userManager from '../utils/userManager';
import { createUserManager } from 'redux-oidc';
import config from '../config/config'
import { fetchToken } from '../actions/token';

class Login extends React.Component {
  
  onLoginButtonClick = (event) => {
    event.preventDefault();
    userManager.signinRedirect();
  };

  onLogoutButtonClick = (event) => {
    event.preventDefault();
    userManager.signoutRedirect();
  };

  componentDidMount() {
    const { dispatch } = this.props;
    userManager.signinRedirectCallback()
        .then(e => {
          console.log('user token fetched sucessfully')
        })
       .catch(e => {
          dispatch(fetchToken())
          console.log('could not log in user fetching basic token')
       })
  }

  render() {
    var button = <button onClick={this.onLoginButtonClick}>Login</button>
    if(this.props.user != null)
    {
      button = <button onClick={this.onLogoutButtonClick}>Logout</button>  
    }
    return (
        <div/>             
    );
  }
}

Login.PropTypes = {
  user: PropTypes.object.isRequired
}

function mapStateToProps(state) {
  const { user } = state.auth;

  return {
    user
  }
}

export default connect(mapStateToProps)(Login);