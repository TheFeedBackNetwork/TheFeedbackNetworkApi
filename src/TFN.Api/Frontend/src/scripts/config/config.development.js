module.exports = {
    auth: {
        url: "http://localhost:5000/identity/authorize",
        client: "some_client_id",
        redirect: "http://localhost.com:5001/callback.html",
        scope: "posts.write posts.read posts edit posts.delete tracks.read tracks.write tracks.delete"
    },
    userManager : {
        client_id: "tfn_frontend",
        redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/oidc-callback`,
        response_type: "token",
        scope: "posts.write posts.read posts.edit posts.delete tracks.read tracks.write tracks.delete",
        authority: "http://localhost:5000/identity",
        silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/oidc-renew`,
        automaticSilentRenew: true,
        filterProtocolClaims: false,
        loadUserInfo: true,
    }
}