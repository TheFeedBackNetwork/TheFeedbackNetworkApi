import React, { PropTypes } from 'react'
import { Col, Grid, Tabs, Tab, TabContent } from 'react-bootstrap'
import { connect } from 'react-redux'
import HeroAvatar from '../components/HeroAvatar'
import HeroBiography from '../components/HeroBiography'
import HeroSummary from '../components/HeroSummary'
import ActivityContainer from './ActivityContainer'
import TracksContainer from './TracksContainer'
import FavouritesContainer from './FavouritesContainer'
import ActivityLogContainer from './ActivityLogContainer'

class MyProfileContainer extends React.Component {

    render() {
        
        const { profilePictureUrl, username, biography, credits, id } = this.props.user
        
        return (
            <div>
                <div className='profile-header'>
                    <Grid>
                        <Col style={{textAlign: 'center'}} md={2}>
                            <HeroAvatar
                                soundcloud={biography.soundCloudUrl}
                                twitter={biography.twitterUrl}
                                instagram={biography.instagramUrl}
                                youtube={biography.youTubeUrl}
                                facebook={biography.facebookUrl}
                                avatar={profilePictureUrl}
                            />
                        </Col>
                        <Col md={3}>
                            <HeroBiography
                                username={username}
                                score={credits.totalCredits}
                                location={biography.location}
                                biography={biography.text}
                            />
                        </Col>
                        <Col md={7}>
                            <HeroSummary
                                
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
                                            <TabContent componentClass={ActivityContainer} username={username} userId={id}/>
                                        </Tab>
                                        <Tab eventKey={2} title='Tracks'>
                                            <TabContent componentClass={TracksContainer}  username={username} userId={id}/>
                                        </Tab>
                                        <Tab eventKey={3} title='Favourites'>
                                            <TabContent componentClass={FavouritesContainer}  username={username} userId={id}/>
                                        </Tab>
                                    </Tabs>
                                </div>
                            </Col>
                            <Col sm={4} className='activity-logs'>
                                <div>
                                    <p className='title'>
                                        <strong>Activity Logs</strong>
                                    </p>
                                    <ActivityLogContainer  username={username} userId={id} />
                                </div>
                                    
                                
                            </Col>
                        </Grid>
                    </div>
            </div>
            
        )
    }
}



MyProfileContainer.PropTypes = {
    user: PropTypes.object.isRequired,
}

function mapStateToProps(state) {
    const { me } = state.user

    return {
        user: me
    }
}

                
export default connect(mapStateToProps)(MyProfileContainer);