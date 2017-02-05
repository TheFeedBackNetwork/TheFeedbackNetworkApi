import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import { login, logout } from '../actions/auth'

const config = {
        url: "http://localhost:5000/identity/connect/authorize",
        client: "tfn_frontend",
        redirect: "http://localhost:5001/callback.html",
        scope: "posts.write posts.read posts.edit posts.delete tracks.read tracks.write tracks.delete"
}
const test = () => {
        console.log('lol')
}
const Login = ({ isLoggedIn, login, logout }) => {
  if (isLoggedIn) {
    return <button type='button' onClick={logout}>Logout</button>
  } else {
    return <button type='button' onClick={login}>Login</button>
  }
}

Login.propTypes = {
  isLoggedIn: PropTypes.bool.isRequired,
  login: PropTypes.func.isRequired,
  logout: PropTypes.func.isRequired
}

const mapStateToProps = ({ auth }) => ({
  isLoggedIn: auth.isLoggedIn
})

const mapDispatchToProps = {
  login: () => login(config),
  logout
}

export default connect(mapStateToProps, mapDispatchToProps)(Login)