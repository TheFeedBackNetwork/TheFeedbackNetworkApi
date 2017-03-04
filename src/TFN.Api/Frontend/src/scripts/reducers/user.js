import * as types from '../constants/ActionTypes';

export default function reducer(state = {
    meFetchFailed: false,
    meFetchInProgress: true,
    meFetched: false,
    meNotFound: false,
    userFetchFailed: false,
    userFetchInProgress: true,
    userFetched: false,
    userNotFound: false,
    me: {credits:{}},
    users: {},
    errorMessage: null,
}, action) {
    switch(action.type) {
        case types.FETCH_ME: {
            return {
                ...state,
                meFetchInProgress: true
            }
        }
        case types.FETCH_ME_FULFILLED: {
            return {
                ...state,
                me: action.payload,
                meFetched: true,
                meFetchInProgress: false,
                meFetchFailed: false,
            }
        }
        case types.FETCH_ME_REJECTED: {
            return {
                ...state,
                meFetched:false,
                meFetchFailed: true,
                meFetchInProgress: false,
            }
        }

    }

    return state;
}
