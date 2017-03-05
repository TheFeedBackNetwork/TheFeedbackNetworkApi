import * as types from '../constants/ActionTypes';

export default function reducer(state ={
    token: "",
    principleType: "UNAUTHORIZED",
    fetchingToken: false,
    fetchedToken: false,
    errorToken: null,
}, action) {
    switch(action.type) {
        case types.FETCH_TOKEN: {
            return {
                ...state,
                fetchingToken: true
            }
        }
        case types.FETCH_TOKEN_FULFILLED: {
            return {
                ...state,
                fetchingToken:false,
                fetchedToken:true,
                principleType: "BASIC",
                token: action.payload
            }
        }
        case types.FETCH_TOKEN_REJECTED: {
            return {
                ...state,
                fetchingToken:false,
                fetchedToken:false,
            }            
        }
        case 'redux-oidc/USER_FOUND': {
            return {
                ...state,
                principleType: "STANDARD_USER",
                fetchedToken: true,
                token: action.payload.access_token
            }
        }

    }

    return state;
}