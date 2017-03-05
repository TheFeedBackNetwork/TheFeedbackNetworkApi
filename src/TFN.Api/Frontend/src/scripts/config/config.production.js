module.exports = {
    userManager : {
        client_id: "tfn_frontend",
        redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/oidc-callback`,
        response_type: "token",
        scope: "posts.write posts.read posts.edit posts.delete tracks.read tracks.write tracks.delete users.read ip.read",
        authority: "http://localhost:5000/account",
        silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/oidc-renew`,
        automaticSilentRenew: true,
        filterProtocolClaims: false,
        loadUserInfo: true,
    },
    basicClient : {
        client_id: "tfn_frontend",
        scope: "posts.read tracks.read credits.read users.read ip.read",
        authority: "http://localhost:5000/account",
    },
    server : {
        url: 'http://localhost:5000/api'
    },
    DevOps: {
        SentryKey : 'ed8abe5105234b7282f68d27e02fe08d',
        SentryApp : '130476'
    }
}