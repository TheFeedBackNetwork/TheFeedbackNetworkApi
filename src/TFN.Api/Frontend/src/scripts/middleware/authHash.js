import querystring from 'query-string'

const authHash = (store) => (next) => (action) => {
  
  console.log('h')
  let hash
  try {
    hash = window.location.hash
    
  } catch (err) {
    if (process.env.NODE_ENV !== 'production') {
      /* eslint-disable no-console */
      console.error(err)
      /* eslint-enable no-console */
    }
  }

  if (hash) {
    console.log('ya hash');
    //popup.close()

    const response = querystring.parse(hash.substr(1))
    console.log(response)
    if (response.state !== state) {
      reject('Invalid state returned.')
    }

    if (response.access_token) {
      resolve(response.access_token)
    } else {
      reject(response.error || 'Unknown error.')
    }
  }

  return next(action)
}

export default authHash