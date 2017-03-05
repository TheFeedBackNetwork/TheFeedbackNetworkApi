import React from 'react';
import { connect } from 'react-redux';
import { CallbackComponent } from 'redux-oidc';
import userManager from '../../utils/userManager';
import { withRouter } from 'react-router-dom';

class Callback extends React.Component {
    
    successCallback = (e) => { 
        this.props.push('/')
    }

    render() {
    return (
            <CallbackComponent userManager={userManager} successCallback={this.successCallback} errorCallback={this.successCallback}>
                <div>
                    Redirecting...
                </div>
            </CallbackComponent>
        );
    }
}

export default Callback;