const BASE_URL = "https://localhost:7227/api";

async function apiRequest(endpoint, method = "GET", body = null) {
    const options = {
        method,
        headers: {
            "Content-Type": "application/json"
        }
    };

    if (body) {
        options.body = JSON.stringify(body);
    }

    const response = await fetch(`${BASE_URL}${endpoint}`, options);

    // If no content
    if (response.status === 204) {
        return null;
    }

    const text = await response.text();

    // If response is empty
    if (!text) {
        return null;
    }

    try {
        return JSON.parse(text);
    } catch {
        // Not JSON → return plain text
        return text;
    }
}