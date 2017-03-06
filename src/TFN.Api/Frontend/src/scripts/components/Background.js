import React, { PropTypes } from 'react';
import image from 'images/background.jpg'

export default class Background extends React.Component {

    

    render() {
        return (
            <div className="bg"> 
                <img src={image} />
            </div>
        )
    }
}