import { setToken, removeToken } from '../utils/token'
import * as types from '../constants/ActionTypes'

const authMiddleware = (store) => (next) => (action) => {
  
  switch (action.type) {
    case types.LOGIN_SUCCESS:
      setToken(action.token)
      break
    case types.LOGIN_FAILURE:
    case types.LOGOUT:
      removeToken()
      break
  }

  return next(action)
}

export default authMiddleware
