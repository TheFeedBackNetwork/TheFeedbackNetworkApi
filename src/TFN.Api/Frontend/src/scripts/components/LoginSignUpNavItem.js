import React from 'react'
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap'

class LoginSignUpNavItem extends React.Component {

    render() {
        return(
        <div className='login-signup'>
            <Button> Log In </Button>
            <Button bsStyle='primary'>Sign Up! </Button>
        </div>    
        )
    }
}

export default LoginSignUpNavItem;