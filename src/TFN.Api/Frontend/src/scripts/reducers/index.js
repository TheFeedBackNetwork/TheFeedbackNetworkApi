import { combineReducers } from 'redux';

import { reducer as auth } from 'redux-oidc';

export default combineReducers({
    auth
});
