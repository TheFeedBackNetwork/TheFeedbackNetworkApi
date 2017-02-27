import { combineReducers } from 'redux';

import { reducer as auth } from 'redux-oidc';
import token from './token';
import post from './post';
import postRoll from './postRoll'
import location from './location'

export default combineReducers({
    auth,
    token,
    post,
    location,
    postRoll
});
