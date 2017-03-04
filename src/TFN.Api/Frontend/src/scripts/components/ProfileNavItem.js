import React from 'react'
import { Link } from 'react-router-dom';

class ProfileNavItem extends React.Component {

    render() {
        return(
        <div className='profile'>
                <img src={this.props.avatar} className='img-circle img-profile' />
                {this.props.username}
                <span className="rep green">+{this.props.score}</span>
            <b className='caret' />
        </div>    
        )
    }
}

export default ProfileNavItem;