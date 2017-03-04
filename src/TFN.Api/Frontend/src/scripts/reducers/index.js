import { combineReducers } from 'redux';

import { reducer as auth } from 'redux-oidc';
import token from './token';
import post from './post';
import postRoll from './postRoll'
import location from './location'
import userAgent from './useragent'
import user from './user'

export default combineReducers({
    auth,
    token,
    post,
    location,
    postRoll,
    userAgent,
    user
});
