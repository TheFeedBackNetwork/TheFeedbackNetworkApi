import React, { PropTypes } from 'react'
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import config from '../config/config';
import { connect } from 'react-redux';
import { NavItem } from 'react-bootstrap';
import userManager from '../utils/userManager';
import { createUserManager } from 'redux-oidc';
import { fetchToken } from '../actions/token';
import { fetchMe } from '../actions/user';

class LoginSignUpNavItem extends React.Component {

onLoginButtonClick = (event) => {
    console.log('clicked')
    event.preventDefault();
    userManager.signinRedirect();
  };

  onLogoutButtonClick = (event) => {
    event.preventDefault();
    userManager.signoutRedirect();
  };

  componentWillMount() {
    const { dispatch } = this.props;
    //dispatch(fetchToken())
    userManager.signinSilent()
        .then(e => {
            dispatch(fetchMe(e.access_token))
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
            <NavItem className='login btn btn-link' onClick={this.onLoginButtonClick}> Log In </NavItem> 
            <Button bsStyle='primary' href={this.getSignUpUrl()}>Sign Up</Button>
        </div>    
        )
    }
}

LoginSignUpNavItem.PropTypes = {
    dispatch: PropTypes.func.isRequired
}

function mapStateToProps(state) {
    return {
        dispatch: state.dispatch
    }
}

export default connect(mapStateToProps)(LoginSignUpNavItem);