
export function formatHeader (jwt) {
    const header = {
        Authorization: `Bearer ${jwt}`,
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }

    return header;
}