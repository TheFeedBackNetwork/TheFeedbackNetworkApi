import axios from 'axios';
import querystring from 'querystring'
import config from '../config/config';
import * as types from '../constants/ActionTypes';
import * as principleTypes from '../constants/PrincipleTypes'
import { formatHeader } from '../utils/app';

export function fetchIP(token, principleType) {
    
    return function(dispatch) {

        dispatch({type: types.FETCH_IP})
        const configuration = config().config;

        var headers = formatHeader(token);
        axios.defaults.headers = headers;

        axios.get(`${configuration.server.url}/ip`)
            .then((response) => {
                dispatch({type: types.FETCH_IP_FULFILLED, payload: response.data.ip})
                if(principleType === principleTypes.BASIC)
                {
                    Raven.setUserContext({
                        ip_address: response.data.ip
                    })
                }
            })
            .catch((error) => {
                Raven.captureException(error)
                dispatch({type: types.FETCH_IP_REJECTED, payload: error.data})
            })

    }

}