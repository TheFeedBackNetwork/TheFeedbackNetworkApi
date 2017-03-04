import React from 'react'

class HeroBiography extends React.Component {

    render() {
        return (
            <div>
                <h1 className="username">{this.props.username} <span className="rep green">+{this.props.score}</span></h1>
                <p className="location"><i className="fa fa-map-marker"></i>{this.props.location}</p>

                <div className="about">
                    <p>{this.props.biography}</p>
                </div>
            </div>
        )
    }
}

export default HeroBiography;