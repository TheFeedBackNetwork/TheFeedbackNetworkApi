import * as types from '../constants/ActionTypes';
import authorize from '../utils/oauth2'

const loginRequest = () => ({
  type: types.LOGIN_REQUEST
})

const loginSuccess = (token) => ({
  type: types.LOGIN_SUCCESS,
  token
})

const loginFailure = (error) => ({
  type: types.LOGIN_FAILURE,
  error
})

export const login = (config) => (dispatch) => {
  dispatch(loginRequest())
  return authorize(config).then(
    (token) => dispatch(loginSuccess(token)),
    (error) => dispatch(loginFailure(error))
  )
}

export const logout = () => ({
  type: types.LOGOUT
})
