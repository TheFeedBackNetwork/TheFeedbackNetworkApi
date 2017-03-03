import * as types from '../constants/ActionTypes';

export default function reducer(state ={
    IP: "",
    fetchingIP: false,
    fetchedIP: false,
    errorIP: null,
}, action) {
    switch(action.type) {
        case types.FETCH_IP: {
            return {
                ...state,
                fetchingIP: true
            }
        }
        case types.FETCH_IP_FULFILLED: {
            return {
                ...state,
                fetchingIP:false,
                fetchedIP:true,
                IP: action.payload
            }
        }
        case types.FETCH_TOKEN_REJECTED: {
            return {
                ...state,
                fetchingToken:false,
                fetchedToken:false,
            }            
        }

    }

    return state;
}