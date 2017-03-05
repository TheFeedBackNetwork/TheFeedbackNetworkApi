import React, { PropTypes } from 'react'

class FavouritesContainer extends React.Component {

    render() {
        return(
            <div>
                Favourites Placeholder {this.props.username}
            </div>
        )
    }
}

FavouritesContainer.PropTypes ={ 
    username: PropTypes.string.isRequired,
    userId: PropTypes.string.isRequired
}

export default FavouritesContainer;