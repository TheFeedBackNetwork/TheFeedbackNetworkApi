import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { Navbar, Nav, NavItem, NavDropdown, MenuItem } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import aviImage from 'images/avatar-placeholder.png';
import uploadImage from 'images/i-upload-24.png';
import ProfileNavItem from '../components/ProfileNavItem'
import LoginSignUpNavItem from '../components/LoginSignUpNavItem'
import * as principleTypes from '../constants/PrincipleTypes'

class NavigationContainer extends React.Component {

    getNavItem() {
        if(this.props.principleType !== principleTypes.UNAUTHORIZED && this.props.principleType !== principleTypes.BASIC)
        {    
            return <ProfileNavItem avatar={this.props.avatar} username={this.props.username} score={this.props.score} /> 
        }

        return <LoginSignUpNavItem />
    }

    getUploadItem() {
        if(this.props.principleType !== principleTypes.UNAUTHORIZED && this.props.principleType !== principleTypes.BASIC)
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
        console.log(this.props.principleType)
        console.log(principleTypes)
        if(this.props.principleType !== principleTypes.UNAUTHORIZED)
        {
            return <NavDropdown noCaret eventKey={2} title={navItem} id="nav-dropdown">
                        <MenuItem eventKey={2.1}><Link to='/profile'> <i className="fa fa-user" /> Profile </Link></MenuItem>
                        <MenuItem eventKey={2.2}><Link to='/settings'> <i className="fa fa-sliders" /> Settings </Link></MenuItem>
                        <MenuItem eventKey={2.3}><Link to='/logout'> <i className="fa fa-sign-out" /> Log Out </Link></MenuItem>
                    </NavDropdown>
        }
        return navItem
    }

    render() {
        const uploadItem = this.getUploadItem()
        const navDropDown = this.getNavDropDown()
        return(
            <Navbar default collapseOnSelect staticTop>
                <Navbar.Header>
                <Navbar.Brand>
                    <Link to='/'> The Feedback Network </Link>
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
    username: PropTypes.string,
    avatar: PropTypes.string,
    score: PropTypes.number
}

function mapStateToProps(state) {
    const { principleType } = state.token
    const { profilePictureUrl, username } = state.user
    const { totalCredits } = state.user.me.credits

    return {
        principleType: principleType,
        username: username,
        avatar: profilePictureUrl,
        score: totalCredits
    }
}

export default connect(mapStateToProps)(NavigationContainer);