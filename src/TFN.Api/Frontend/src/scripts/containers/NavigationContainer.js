import React, { PropTypes } from 'react';
import { connect } from 'react-redux';
import { Navbar, Nav, NavItem, NavDropdown, MenuItem } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import aviImage from 'images/avatar-placeholder.png';
import uploadImage from 'images/i-upload-24.png';
import ProfileNavItem from '../components/ProfileNavItem'

class NavigationContainer extends React.Component {



    render() {
        var p = {avatar: aviImage, username: 'crzymnky', score: 1000}
        var a = <ProfileNavItem avatar={p.avatar} username={p.username} score={p.score} />
        return(
            <Navbar default collapseOnSelect staticTop>
                <Navbar.Header>
                <Navbar.Brand>
                    <Link to='/'> The Feedback Network </Link>
                </Navbar.Brand>
                </Navbar.Header>
                <Nav pullRight>
                    <NavItem eventKey={1}>
                        <Link to='/upload'>
                            <img src={uploadImage} alt='Upload' className='icon-upload'/>
                        </Link>
                    </NavItem>
                    <NavDropdown noCaret eventKey={2} title={a} id="nav-dropdown">
                        <MenuItem eventKey={2.1}><Link to='/profile'>Profile </Link></MenuItem>
                        <MenuItem eventKey={2.2}><Link to='/settings'>Settings </Link></MenuItem>
                        <MenuItem eventKey={2.3}><Link to='/logout'>Log Out </Link></MenuItem>
                    </NavDropdown>
                </Nav>
            </Navbar>
        )
    }
}

NavigationContainer.propTypes = {
    dispatch: PropTypes.func.isRequired,
    principleType: PropTypes.string.isRequired
}

function mapStateToProps(state) {
    const { principleType } = state.token

    return {
        principleType: principleType
    }
}

export default connect(mapStateToProps)(NavigationContainer);