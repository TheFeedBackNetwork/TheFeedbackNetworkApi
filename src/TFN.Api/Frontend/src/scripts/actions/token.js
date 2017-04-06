import axios from 'axios';
import querystring from 'querystring'
import config from '../config/config';
import * as types from '../constants/ActionTypes';


export function fetchToken() {
    
    return function(dispatch) {

        dispatch({type: types.FETCH_TOKEN})
        const configuration = config().config;
        const body = {
            client_id: configuration.basicClient.client_id,
            grant_type: 'client_credentials',
            scope: configuration.basicClient.scope,
        };
        
        axios.post(`${configuration.basicClient.authority}/connect/token`, querystring.stringify(body))
            
            .then((response) => {
                dispatch({type: types.FETCH_TOKEN_FULFILLED, payload: response.data.access_token})
            })
            .catch((error) => {
                Raven.captureException(error)
                dispatch({type: types.FETCH_TOKEN_REJECTED, payload: error})
            })
    }

}