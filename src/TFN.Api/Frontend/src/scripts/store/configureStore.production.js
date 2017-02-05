import { createStore, applyMiddleware, compose } from 'redux';
import promiseMiddleware from 'redux-promise';

import authMiddleware from '../middleware/authMiddleware';
import rootReducer from '../reducers';
import thunk from 'redux-thunk';

const middlewares = [thunk, promiseMiddleware, authMiddleware]

const enhancer = compose(
  applyMiddleware(...middlewares)
)(createStore);

export default function configureStore(initialState) {
  return enhancer(rootReducer, initialState);
}
