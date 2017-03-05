import React from 'react'
import { Link } from 'react-router-dom';
import { Row } from 'react-bootstrap'

class HeroAvatar extends React.Component {

    getSoundCloud() {
        if(this.props.soundcloud.includes('soundcloud'))
        {
            return <a href={this.props.soundcloud}><i className="fa fa-soundcloud"></i></a>
        }

        return null;
    }

    getTwitter() {
        if(this.props.twitter.includes('twitter'))
        {
            return <a target='_blank' href={this.props.twitter}><i className="fa fa-twitter"></i></a>
        }

        return null;
    }

    getInstagram() {
        if(this.props.instagram.includes('instagram'))
        {
            return <a target='_blank' href={this.props.instagram}><i className="fa fa-instagram"></i></a>
        }

        return null;
    }

    getYouTube() {
        if(this.props.youtube.includes('youtube'))
        {
            return <a target='_blank' href={this.props.youtube}><i className="fa fa-youtube"></i></a>
        }
        return null;
    }

    getFacebook() {
        if(this.props.facebook.includes('facebook'))
        {
            return <a target='_blank' href={this.props.facebook}><i className="fa fa-facebook"></i></a>
        }
        
        return null;
    }

    render() {
        const soundcloud = this.getSoundCloud()
        const twitter = this.getTwitter()
        const instagram = this.getInstagram()
        const youtube = this.getYouTube()
        const facebook = this.getFacebook()
        return (
            <div>
                <Row>
                    <img src={this.props.avatar} className='img-circle img-profile' />
                </Row>
                <Row>
                    <div className='social'>
                        {soundcloud}
                        {twitter}
                        {instagram}
                        {youtube}
                        {facebook}
                     </div>
                </Row>
            </div>
        )
    }

}

export default HeroAvatar;