import React from 'react'
import { Link } from 'react-router-dom';

class HeroAvatar extends React.Component {

    getSoundCloud() {
        if(this.props.soundcloud.includes('soundcloud'))
        {
            return <a href="#"><i className="fa fa-soundcloud"></i></a>
        }

        return null;
    }

    getTwitter() {
        if(this.props.twitter.includes('twitter'))
        {
            return <a href="#"><i className="fa fa-twitter"></i></a>
        }

        return null;
    }

    getInstagram() {
        if(this.props.instagram.includes('instagram'))
        {
            return <a href="#"><i className="fa fa-instagram"></i></a>
        }

        return null;
    }

    getYouTube() {
        if(this.props.youtube.includes('youtube'))
        {
            return <a href="#"><i className="fa fa-youtube"></i></a>
        }
        return null;
    }

    getFacebook() {
        if(this.props.facebook.includes('facebook'))
        {
            return <a href="#"><i className="fa fa-facebook"></i></a>
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
                <img src={this.props.avatar} className='img-circle img-profile' />
                <div className='social'>
                    {soundcloud}
                    {twitter}
                    {instagram}
                    {youtube}
                    {facebook}
                </div>
            </div>
        )
    }

}

export default HeroAvatar;