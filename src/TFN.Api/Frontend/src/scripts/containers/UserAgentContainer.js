import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { fetchIP } from '../actions/userAgent';
import * as principleTypes from '../constants/PrincipleTypes'

class UserAgentContainer extends React.Component {
    
    getIP() {
        const { dispatch, token, principleType } = this.props
        dispatch(fetchIP(token, principleType))
    }

    componentWillMount() {
        if(this.props.principleType !== principleTypes.UNAUTHORIZED && !this.props.fetchedIP && !this.props.fetchingIP && this.props.fetchedToken) 
        {
            this.getIP()
        }
    }

    render() {
        return(
            <div>
            </div>
        )
    }
}

UserAgentContainer.PropTypes = {
    fetchingIP: PropTypes.bool.isRequired,
    fetchedIP: PropTypes.bool.isRequired,
    fetchedToken: PropTypes.bool.isRequired,
    errorIP: PropTypes.bool,
    principleType: PropTypes.string.isRequired,
    token: PropTypes.string.isRequired
}

function mapStateToProps(state) {
    const { fetchingIP, fetchedIP, errorIP } = state.userAgent
    const { token, fetchedToken, principleType } = state.token

    return {
        token: token,
        fetchedIP: fetchedIP,
        fetchingIP: fetchingIP,
        fetchedToken: fetchedToken,
        principleType: principleType,
        errorIP: errorIP
    }
}

export default connect(mapStateToProps)(UserAgentContainer);