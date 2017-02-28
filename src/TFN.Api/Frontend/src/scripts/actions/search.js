
import axios from 'axios';
import config from '../config/config';
import { formatHeader } from '../utils/app';

export function searchUser(query, token)
{
    const configuration = config().config;

    var headers = formatHeader(token);
    axios.defaults.headers = headers;
    //console.log(axios.get(`${configuration.server.url}/users?username=${query}`))
    return axios.get(`${configuration.server.url}/users?username=${query}`)
        
}

export function getSearchQuery(query, token)
{
        const configuration = config().config;

    var headers = formatHeader(token);
    axios.defaults.headers = headers;

    return `${configuration.server.url}/users?username=${query}`
}