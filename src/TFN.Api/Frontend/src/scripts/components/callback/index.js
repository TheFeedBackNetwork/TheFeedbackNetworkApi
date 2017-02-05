import React from 'react';
import { connect } from 'react-redux';
import { CallbackComponent } from 'redux-oidc';
import userManager from '../../utils/userManager';
import { withRouter } from 'react-router-dom';

/*
const Callback = () => (
    
    <CallbackComponent userManager={userManager} successCallback={successCallback} errorCallback={successCallback}>
                <div>
                    Redirecting...
                </div>
            </CallbackComponent>
)

export default Callback;
*/
class Callback extends React.Component {
    
    successCallback = (e) => { 
        //console.log(this)
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