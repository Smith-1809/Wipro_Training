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

    if (!response.ok) {
        const errorText = await response.text();
        console.error("API Error:", response.status, errorText);
        throw new Error(`API request failed: ${response.status}`);
    }

    if (response.status === 204) {
        return null;
    }

    const text = await response.text();
    return text ? JSON.parse(text) : null;
}

// ================= TICKETS =================

function getTickets() {
    return apiRequest("/Tickets");
}

function createTicket(ticket) {
    return apiRequest("/Tickets", "POST", ticket);
}

// ================= CUSTOMERS =================

function getCustomers() {
    return apiRequest("/Customers");
}

function createCustomer(customer) {
    return apiRequest("/Customers", "POST", customer);
}