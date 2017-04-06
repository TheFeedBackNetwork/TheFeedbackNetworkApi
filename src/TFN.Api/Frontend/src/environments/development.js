require("babel-polyfill");
// Load CSS via Webpack to be able to require Bootstrap, Font Awesome, etc. from npm
// Expose Raven
window.Raven = require('raven-js');
require('styles/styles.scss');

// JavaScript main file
require('../scripts/containers');
