export function getToken(userToken, basicToken) {
    if(userToken != null)
    {
        return userToken;
    }
    return basicToken;
}