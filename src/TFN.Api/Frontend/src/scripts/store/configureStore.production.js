import { createStore, applyMiddleware, compose } from 'redux';
import promiseMiddleware from 'redux-promise';
import createLogger from 'redux-logger';

import createOidcMiddleware, { createUserManager } from 'redux-oidc';
import userManager from '../utils/userManager';

import rootReducer from '../reducers';
import thunk from 'redux-thunk';

const logger = createLogger();
const oidcMiddleware = createOidcMiddleware(userManager);

const middlewares = [promiseMiddleware, thunk, oidcMiddleware, logger,require('redux-immutable-state-invariant')()]

const enhancer = compose(
  applyMiddleware(...middlewares)
)(createStore);

export default function configureStore(initialState) {
  return enhancer(rootReducer, initialState);
}
