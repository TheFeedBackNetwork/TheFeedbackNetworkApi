import * as types from '../constants/ActionTypes';

export default function reducer(state = {
    postRollFetchFailed: false,
    postRollFetchInProgress: true,
    postRollFetched: false,
    postRoll: [],
    errorMessage: null,
}, action) {
    switch(action.type) {
        case types.FETCH_POSTROLL: {
            return {
                ...state,
                postRollFetchInProgress: true
            }
        }
        case types.FETCH_POSTROLL_FULFILLED: {
            return {
                ...state,
                postRoll: action.payload,
                postRollFetched: true,
                postRollFetchInProgress: false,
                postRollFetchFailed: false,
            }
        }
        case types.FETCH_POSTROLL_REJECTED: {
            return {
                ...state,
                postRollFetchFailed: true,
                postRollFetchInProgress: false,
            }
        }
    }

    return state;
}
