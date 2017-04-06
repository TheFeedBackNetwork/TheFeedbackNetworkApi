import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { Navbar, Button, Nav, NavItem, NavDropdown, MenuItem } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import aviImage from 'images/avatar-placeholder.png';
import uploadImage from 'images/i-upload-24.png';
import ProfileNavItem from '../components/ProfileNavItem'
import LoginSignUpNavItem from '../components/LoginSignUpNavItem'
import * as principleTypes from '../constants/PrincipleTypes'
import { fetchMe } from '../actions/user';
import userManager from '../utils/userManager';

class NavigationContainer extends React.Component {

    onLogoutButtonClick = (event) => {
        event.preventDefault();
        userManager.signoutRedirect();
    };

    getNavItem() {        
        if(this.props.meFetched)
        {
            return <ProfileNavItem avatar={this.props.avatar} username={this.props.username} score={this.props.score} /> 
        }
        return <LoginSignUpNavItem />
    }
    something() {
        console.log('lol')
    }
    getUploadItem() {
        if(this.props.meFetched)
        {
            return <NavItem eventKey={1}>
                        <Link to='/upload'>
                            <img src={uploadImage} alt='Upload' className='icon-upload'/>
                        </Link>
                    </NavItem>
        }
        return null
    }

    getNavDropDown() {
        const navItem = this.getNavItem()
        if(this.props.meFetched)
        {
            const navItem = this.getNavItem()
            return <NavDropdown noCaret eventKey={2} title={navItem} id="nav-dropdown">
                        <MenuItem eventKey={2.1}><Link to='/profile'> <i className="fa fa-user" /> Profile </Link></MenuItem>
                        <MenuItem eventKey={2.2}><Link to='/settings'> <i className="fa fa-sliders" /> Settings </Link></MenuItem>
                        <MenuItem eventKey={2.3}><a href='/signout' onClick={this.onLogoutButtonClick}> <i className="fa fa-sign-out" /> Log Out </a></MenuItem>
                    </NavDropdown>
        }
        
        return this.getNavItem()
    }

    render() {
        const uploadItem = this.getUploadItem()
        const navDropDown = this.getNavDropDown()
        return(
            <Navbar default collapseOnSelect staticTop>
                <Navbar.Header>
                <Navbar.Brand>
                    <Link to='/'> Waveform Test UI </Link>
                </Navbar.Brand>
                </Navbar.Header>
                <Nav pullRight>
                    {uploadItem}
                    {navDropDown}
                </Nav>
            </Navbar>
        )
    }
}

NavigationContainer.propTypes = {
    dispatch: PropTypes.func.isRequired,
    principleType: PropTypes.string.isRequired,
    token: PropTypes.string.isRequired,
    meFetched: PropTypes.bool.isRequired,
    username: PropTypes.string,
    avatar: PropTypes.string,
    score: PropTypes.number
}

function mapStateToProps(state) {
    const { principleType } = state.token
    const { token } = state.token
    const { profilePictureUrl, username } = state.user.me
    const { totalCredits } = state.user.me.credits
    const { meFetched } = state.user

    return {
        principleType: principleType,
        token: token,
        meFetched: meFetched,
        username: username,
        avatar: profilePictureUrl,
        score: totalCredits
    }
}

export default connect(mapStateToProps)(NavigationContainer);