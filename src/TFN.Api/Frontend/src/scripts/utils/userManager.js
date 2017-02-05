import { createUserManager } from 'redux-oidc';
import config from '../config/config'

const userManagerConfig = config().config.userManager;


const userManager = createUserManager(userManagerConfig);

export default userManager;