import * as types from '../constants/ActionTypes';

export function changeLocation(location) {
    
    return function(dispatch) {
        console.log(location)
        dispatch({type: types.LOCATION_CHANGE, payload: location})
    }

}