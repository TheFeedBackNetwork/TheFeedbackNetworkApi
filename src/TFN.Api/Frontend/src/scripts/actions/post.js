import axios from 'axios';
import querystring from 'querystring'
import config from '../config/config';
import * as types from '../constants/ActionTypes';

export function fetchPostRoll(offset, limit, genre, token) {
    
    return function(dispatch) {

        dispatch({type: types.FETCH_POSTROLL})

        


    }

}

export function fetchPost(offset, limit, genre, token) {
    
    return function(dispatch) {

        dispatch({type: types.FETCH_POST})

    }

}