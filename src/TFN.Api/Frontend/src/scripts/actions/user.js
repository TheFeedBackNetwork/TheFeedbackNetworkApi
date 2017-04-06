import axios from 'axios';
import querystring from 'querystring'
import config from '../config/config';
import * as types from '../constants/ActionTypes';
import { formatHeader } from '../utils/app';

export function fetchMe(token) {
    
    return function(dispatch) {
        dispatch({type: types.FETCH_ME})
        const configuration = config().config;

        var headers = formatHeader(token);
        axios.defaults.headers = headers;

        axios.get(`${configuration.server.url}/users/me`)
            .then((response) => {
                dispatch({type: types.FETCH_ME_FULFILLED, payload: response.data})
                Raven.setUserContext({
                    email: response.data.email,
                    id: response.data.id,
                    username: response.data.username
                })
            })
            .catch((error) => {
                Raven.captureException(error)
                dispatch({type: types.FETCH_ME_REJECTED, payload: error.data})
            })

    }

}