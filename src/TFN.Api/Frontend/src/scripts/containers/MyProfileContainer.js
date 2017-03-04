import React from 'react'
import { Col, Grid, Tabs, Tab, TabContent } from 'react-bootstrap'
import HeroAvatar from '../components/HeroAvatar'
import HeroBiography from '../components/HeroBiography'
import avatar from 'images/avatar-placeholder.png'
import ActivityContainer from './ActivityContainer'
import TracksContainer from './TracksContainer'
import FavouritesContainer from './FavouritesContainer'
import ActivityLogContainer from './ActivityLogContainer'

class MyProfileContainer extends React.Component {

    render() {
        return (
            <div>
                <div className='profile-header'>
                    <Grid>
                        <Col md={2}>
                            <HeroAvatar
                                soundcloud={'https://www.soundcloud.com/test'}
                                twitter={'https://www.twitter.com/test'}
                                instagram={'https://www.instagram.com/test'}
                                youtube={'https://www.youtube.com/test'}
                                facebook={'https://www.facebook.com/test'}
                                avatar={avatar}
                            />
                        </Col>
                        <Col md={3}>
                            <HeroBiography
                                username={'crzymonkey'}
                                score={1000}
                                location={'Cape Town, South Africa'}
                                biography={'Lorem Ipsum'}
                            />
                        </Col>
                    </Grid>
                </div>
                    <div className='profile-main'>
                        <Grid>
                            <Col sm={8} className='col list'>
                                <div className='tabs'>
                                    <Tabs unmountOnExit={true} defaultActiveKey={1} animation={true} id='user-actions'>
                                        <Tab eventKey={1} title='Activity'>
                                            <TabContent componentClass={ActivityContainer}/>
                                        </Tab>
                                        <Tab eventKey={2} title='Tracks'>
                                            <TabContent componentClass={TracksContainer}/>
                                        </Tab>
                                        <Tab eventKey={3} title='Favourites'>
                                            <TabContent componentClass={FavouritesContainer}/>
                                        </Tab>
                                    </Tabs>
                                </div>
                            </Col>
                            <Col sm={4} className='activity-logs'>
                                <div>
                                    <p className='title'>
                                        <strong>Activity Logs</strong>
                                    </p>
                                    <ActivityLogContainer />
                                </div>
                                    
                                
                            </Col>
                        </Grid>
                    </div>
            </div>
            
        )
    }
}
                
export default MyProfileContainer;