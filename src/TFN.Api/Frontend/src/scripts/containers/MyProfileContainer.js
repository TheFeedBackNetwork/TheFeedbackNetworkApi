import React from 'react'
import { Col, Grid } from 'react-bootstrap'
import HeroAvatar from '../components/HeroAvatar'
import avatar from 'images/avatar-placeholder.png'

class MyProfileContainer extends React.Component {

    render() {
        return (
            <div className='profile-header'>
                <Grid>
                    <Col md={1}>
                        <HeroAvatar
                            soundcloud={'www.soundcloud.com/test'}
                            twitter={'www.twitter.com/test'}
                            instagram={'www.instagram.com/test'}
                            youtube={'www.youtube.com/test'}
                            facebook={'www.facebook.com/test'}
                            avatar={avatar}
                        />
                    </Col>
                </Grid>
            </div>
        )
    }
}

export default MyProfileContainer;