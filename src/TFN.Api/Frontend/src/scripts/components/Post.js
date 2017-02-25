import React from 'react';

class Post extends React.Component {

    render() {
        return (
            <div>
                Post
                User : {this.props.match.params.userId} <br/>
                PostId : {this.props.match.params.postId}               
            </div>
        )
    }
}

export default Post;