import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { fetchPost } from '../actions/post';
import * as types from '../constants/PrincipleTypes';

class Post extends React.Component {

    getPost(postId) {

    }

    componentWillMount() {
        console.log(this.props)
        const { dispatch, user, token } = this.props;
        const {userId, postId } = this.props.match.params;
        if(!this.props.postFetched) {
            if(this.props.principleType == types.UNAUTHORIZED) {
            //no token display something 
            }
            if(this.props.principleType == types.STANDARD_USER) {
                dispatch(fetchPost(postId, user.access_token))
            }
            if(this.props.principleType == types.BASIC) { 
                dispatch(fetchPost(postId, token))
            }
        }
    }

    render() {
        //console.log(this.props)
        var component = <div> Loading </div>
        //console.log(this.props)
        if(!this.props.postFetched)
        {
            //get enumeration of posts
            component = <div> post not fetched yet </div>
        }
        if(this.props.postFetchFailed)
        {
            component = <div> Failed to fetch post </div>
        }
        if(this.props.postFetchInProgress)
        {
            component = <div> Loading </div>
        }
        if(this.props.postFetched)
        {
            component = <div> {JSON.stringify(this.props.post)} </div>
        }

        return (
            <div>
                {component}           
            </div>
        )
    }
}

Post.PropTypes = {
    dispatch: PropTypes.func.isRequired,
    post: PropTypes.array.isRequired,
    postFetchFailed: PropTypes.bool.isRequired,
    postFetchInProgress: PropTypes.bool.isRequired,
    postFetched: PropTypes.bool.isRequired,
    post: PropTypes.array,
    user: PropTypes.object.isRequired,
    principleType: PropTypes.string.isRequired,
    token: PropTypes.string.isRequired
}


function mapStateToProps(state) {
    const { post, postFetchFailed, 
            postFetchInProgress, 
            postFetched, errorMessage, } = state.post;
    const { token, principleType } = state.token;
    const { user } = state.auth;

    return {
        post,
        postFetchFailed,
        postFetchInProgress,
        postFetched,
        post,
        errorMessage,
        token,
        principleType,
        user
    }
}

export default connect(mapStateToProps)(Post);