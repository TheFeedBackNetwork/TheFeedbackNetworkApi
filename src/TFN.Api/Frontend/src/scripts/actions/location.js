import * as types from '../constants/ActionTypes';

export function changeLocations(location) {
    
    return function(dispatch) {
        console.log(location)
        dispatch({type: types.changeLocations, payload: location})
    }

}