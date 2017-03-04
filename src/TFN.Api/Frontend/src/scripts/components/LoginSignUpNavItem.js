import React from 'react'
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import config from '../config/config';
import { connect } from 'react-redux';
import userManager from '../utils/userManager';
import { createUserManager } from 'redux-oidc';
import { fetchToken } from '../actions/token';

class LoginSignUpNavItem extends React.Component {

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

    getSignUpUrl() {
        const configuration = config().config;
        return `${configuration.basicClient.authority}/register`
    }
    
    render() {
        return(
        <div className='login-signup'>
            <Button className='login' bsStyle='link'>Log In </Button>
            <Button bsStyle='primary' href={this.getSignUpUrl()}>Sign Up</Button>
        </div>    
        )
    }
}

export default LoginSignUpNavItem;