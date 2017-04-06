import axios from 'axios';
import querystring from 'querystring'
import config from '../config/config';
import * as types from '../constants/ActionTypes';
import { formatHeader } from '../utils/app';

export function fetchPostRoll(offset, limit, genre, token) {
    
    return function(dispatch) {

        dispatch({type: types.FETCH_POSTROLL})
        const configuration = config().config;

        var headers = formatHeader(token);
        axios.defaults.headers = headers;


        axios.get(`${configuration.server.url}/posts?offset=${offset}&limit=${limit}`)
            .then((response) => {
                dispatch({type: types.FETCH_POSTROLL_FULFILLED, payload: response.data})
            })
            .catch((error) => {
                Raven.captureException(error)
                dispatch({type: types.FETCH_POSTROLL_REJECTED, payload: error.data})
            })

    }

}