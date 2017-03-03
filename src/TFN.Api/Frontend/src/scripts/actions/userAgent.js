import axios from 'axios';
import querystring from 'querystring'
import config from '../config/config';
import * as types from '../constants/ActionTypes';
import { formatHeader } from '../utils/app';

export function fetchIP(token) {
    
    return function(dispatch) {

        dispatch({type: types.FETCH_IP})
        const configuration = config().config;

        var headers = formatHeader(token);
        axios.defaults.headers = headers;

        axios.get(`${configuration.server.url}/ip`)
            .then((response) => {
                console.log(response.data)
                dispatch({type: types.FETCH_IP_FULFILLED, payload: response.data.ip})
            })
            .catch((error) => {
                console.log(error)
                dispatch({type: types.FETCH_IP_REJECTED, payload: error.data})
            })

    }

}