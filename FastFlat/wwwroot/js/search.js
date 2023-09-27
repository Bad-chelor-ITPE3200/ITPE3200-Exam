async function getCitySearchResult(keyword) {
    const endpointUrl = `https://test.api.amadeus.com/reference-data/locations/cities?keyword=${keyword}`;
    try {
        fetch(endpointUrl, {
            method: "get",
            headers: {
                'Authorization': `Bearer ${key}`,
            }
        })
            .then((data) => {
                if (data) {
                    data.forEach(result => {
                        console.log(result)
                    })
                }
            })
    } catch (err) {
        console.error(err)
    }
 
}

let key = "";

async function getAccessToken() {
    const clientId = 'AtXWGpJxGJHSdIFtL1LOASA6kzGcWAFS';
    const clientSecret = 'AGPvwdO8OF1Fhjao';

    // Define the endpoint URL
    const endpointUrl = 'https://test.api.amadeus.com/v1/security/oauth2/token';

    // Define the request parameters
    const requestData = new URLSearchParams();
    requestData.append('grant_type', 'client_credentials');
    requestData.append('client_id', clientId);
    requestData.append('client_secret', clientSecret);

    // Make the POST request
    fetch(endpointUrl, {
        method: 'POST',
        body: requestData,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
        },
    })
    .then((response) => {
        // Check if the response status is OK (200)
        if (response.ok) {
            // Parse the JSON response
            return response.json();
        } else {
            throw new Error('Failed to obtain token');
        }
    })
    .then((data) => {
        key = data.access_token;
    })
    .catch((error) => {
        console.error('Error:', error);
    });
}
getAccessToken();