import React, { PropTypes } from 'react';
import { connect } from 'react-redux';

class PostRoll extends React.Component {

    componentWillMount() {
        const { dispatch } = this.props;

    }

    componentWillUpdate() {
        console.log(this.props)
    }

    render() {
        return (
            <div>
                PostRoll
            </div>
        )
    }
}

PostRoll.PropTypes = {
    dispatch: PropTypes.func.isRequired,

}

function mapStateToProps(state) {
    const { postRoll, postRollFetchFailed, postRollFetchingInProgress, postRollFetched, errorMessage, } = state.postRoll;
    const { token } = state.token;
    const { user } = state.auth;

    return {
        postRoll,
        token,
        user
    }
}

export default connect(mapStateToProps)(PostRoll);