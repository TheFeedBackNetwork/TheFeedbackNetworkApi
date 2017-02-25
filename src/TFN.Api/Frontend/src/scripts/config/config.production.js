module.exports = {
    userManager : {
        client_id: "tfn_frontend",
        redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/oidc-callback`,
        response_type: "token",
        scope: "posts.write posts.read posts.edit posts.delete tracks.read tracks.write tracks.delete",
        authority: "http://tfn-dev-webapp.azurewebsites.net/account",
        silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/oidc-renew`,
        automaticSilentRenew: true,
        filterProtocolClaims: false,
        loadUserInfo: true,
    }
}