import React from 'react';
import userManager from '../utils/userManager';
import { createUserManager } from 'redux-oidc';
import config from '../config/config'

class LoginPage extends React.Component {
  onLoginButtonClick = (event) => {
    event.preventDefault();
    userManager.signinRedirect();
  };

  componentDidMount() {
          userManager.signinSilent();
  }

  render() {
    return (
      <div style={styles.root}>
        
        <button onClick={this.onLoginButtonClick}>Login with OIDC</button>
      </div>
    );
  }
}

const styles = {
  root: {
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-around',
    alignItems: 'center',
    flexShrink: 1,
  }
}

export default LoginPage;