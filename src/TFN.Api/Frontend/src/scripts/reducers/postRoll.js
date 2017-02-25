import * as types from '../constants/ActionTypes';

export default function reducer(state = {
    postRollFetchFailed: false,
    postRollFetchInProgress: true,
    postRollFetched: false,
    postRoll: null,
    errorMessage: null,
}, action) {
    switch(action.type) {
        case types.FETCH_POST: {
            return {
                ...state,
                postRollFetchInProgress: true
            }
        }
        case types.FETCH_POST_FULFILLED: {
            return {
                ...state,
                post: action.payload,
                postRollFetched: true,
                postRollFetchInProgress: false,
                postRollFetchFailed: false,
            }
        }
        case types.FETCH_POST_REJECTED: {
            return {
                ...state,
                postRollFetchFailed: true,
                postRollFetchInProgress: false,
            }
        }
    }

    return state;
}
