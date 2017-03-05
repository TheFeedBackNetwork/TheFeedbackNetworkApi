import React, { PropTypes } from 'react'

class ActivityContainer extends React.Component {

    render() {
        return(
            <div>
                Activity Placeholder {this.props.username}
            </div>
        )
    }
}

ActivityContainer.PropTypes ={ 
    username: PropTypes.string.isRequired,
    userId: PropTypes.string.isRequired
}

export default ActivityContainer;