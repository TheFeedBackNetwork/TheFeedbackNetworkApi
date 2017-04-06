// @flow

import React, { PropTypes } from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';
import { OidcProvider } from 'redux-oidc';

import userManager from '../utils/userManager';
import  App  from './App';

import config from '../config/config'

// If you use React Router, make this component
// render <Router> with your routes. Currently,
// only synchronous routes are hot reloaded, and
// you will see a warning from <Router> on every reload.
// You can ignore this warning. For details, see:
// https://github.com/reactjs/react-router/issues/2182

const configuration = config().config;

const SENTRY_URL = `https://${configuration.DevOps.SentryKey}@sentry.io/${configuration.DevOps.SentryApp}`

window.Raven && Raven.config(SENTRY_URL).install();
Raven.setUserContext({
  username: 'unauthorized'
})

const Root = ({ store }) => {
  let ComponentEl = (
    <Provider store={store}>
      <OidcProvider store={store} userManager={userManager}>
        <Router>
          <App />
        </Router>
      </OidcProvider>
    </Provider>
  );

  if (process.env.NODE_ENV !== 'production') {
    const DevTools = require('../components/dev/DevTools').default;

    ComponentEl = (
      <Provider store={store}>
        <OidcProvider store={store} userManager={userManager}>
          <div>
            <Router>
              <App />
            </Router>
            {!window.devToolsExtension ? <DevTools /> : null}
            </div>
        </OidcProvider>
      </Provider>
    );
  }

  return ComponentEl;
};

Root.propTypes = {
  store: PropTypes.object.isRequired
};

export default Root;
