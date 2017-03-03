import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { fetchIP } from '../actions/userAgent';

class UserAgentContainer extends React.Component {
    
    getIP() {
        if(this.props.fetchedToken) {
            const { dispatch, token } = this.props
            dispatch(fetchIP(token))
        }
        
    }
    
    componentWillMount() {
        this.getIP()
    }

    componentDidUpdate() {
        if(!this.props.fetchedIP && this.props.fetchedToken)
        {
            this.getIP()
        }
    }

    render() {
        return(
            <div>
                {this.props.ip}
            </div>
        )
    }
}

UserAgentContainer.PropTypes = {
    ip: PropTypes.string.isRequired,
    fetchingIP: PropTypes.bool.isRequired,
    fetchedIP: PropTypes.bool.isRequired,
    fetchedToken: PropTypes.bool.isRequired,
    errorIP: PropTypes.bool,
    token: PropTypes.string.isRequired
}

function mapStateToProps(state) {
    const {IP, fetchingIP, fetchedIP, errorIP } = state.userAgent
    const { token, fetchedToken } = state.token

    return {
        ip: IP,
        token: token,
        fetchedIP: fetchedIP,
        fetchingIP: fetchingIP,
        fetchedToken: fetchedToken,
        errorIP: errorIP
    }
}

export default connect(mapStateToProps)(UserAgentContainer);