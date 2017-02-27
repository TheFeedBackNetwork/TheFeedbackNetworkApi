import * as types from '../constants/ActionTypes';

export default function reducer(state ={
    location: window.location.pathname
}, action) {
    switch(action.type) {
        case types.LOCATION_CHANGE: {
            return {
                ...state,
                location: action.payload
            }
        }
    }
    return state;
}