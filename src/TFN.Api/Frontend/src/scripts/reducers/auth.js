
import { hasToken, getToken } from '../utils/token'
import * as types from '../constants/ActionTypes'

const initialState = {
  isLoggedIn: hasToken(),
  token: getToken(),
  error: null
}

const auth = (state = initialState, action) => {
  switch (action.type) {
    case types.LOGIN_SUCCESS:
      return Object.assign({}, state, {
        isLoggedIn: true,
        token: action.token,
        error: null
      })
    case types.LOGIN_FAILURE:
      return Object.assign({}, state, {
        isLoggedIn: false,
        token: null,
        error: action.error
      })
    case types.LOGOUT:
      return Object.assign({}, state, {
        isLoggedIn: false,
        token: null,
        error: null
      })
    default:
      return state
  }
}

export default auth
