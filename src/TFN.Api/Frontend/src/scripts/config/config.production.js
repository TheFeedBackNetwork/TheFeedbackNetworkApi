module.exports = {
    auth: {
        url: "http://localhost:5000/account/authorize",
        client: "some_client_id",
        redirect: "http://localhost.com:5001/callback.html",
        scope: "openid biography posts.write posts.read posts edit posts.delete tracks.read tracks.write tracks.delete offline_access"
    }
}