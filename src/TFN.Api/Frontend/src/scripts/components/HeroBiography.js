import React from 'react'
import { Row } from 'react-bootstrap'

class HeroBiography extends React.Component {

    render() {
        return (
            <div>
                <Row>
                <h1 className="username">{this.props.username} <span className="rep green">+{this.props.score}</span></h1>
                </Row>
                <Row>
                <p className="location"><i className="fa fa-map-marker"></i>{this.props.location}</p>
                </Row>
                <Row>
                <div className="about">
                    <p>{this.props.biography}</p>
                </div>
                </Row>
            </div>
        )
    }
}

export default HeroBiography;