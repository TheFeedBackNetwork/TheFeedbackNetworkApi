import * as types from '../constants/ActionTypes';

export default function reducer(state = {
    postFetchFailed: false,
    postFetchInProgress: true,
    post: null,
    errorMessage: null,
}, action) {
    switch(action.type) {
        case types.FETCH_POST: {
            return {
                ...state,
                postFetchInProgress: true
            }
        }
        case types.FETCH_POST_FULFILLED: {
            return {
                ...state,
                post: action.payload,
                postFetchInProgress: false,
                postFetchFailed: false,
            }
        }
        case types.FETCH_POST_REJECTED: {
            return {
                ...state,
                postFetchFailed: true,
                postFetchInProgress: false,
            }
        }
    }

    return state;
}
