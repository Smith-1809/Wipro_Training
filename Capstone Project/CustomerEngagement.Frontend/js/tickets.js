// ========================================
// GLOBAL STATE
// ========================================
let allTickets = [];


// ========================================
// LOAD TICKETS
// ========================================
async function loadTickets() {

    showLoading("ticketTableBody", 5);

    try {
        const tickets = await apiRequest("/Tickets");

        allTickets = tickets;

        renderTickets(allTickets);

    } catch (error) {
        showToast(error, "error");
    }
}


// ========================================
// CREATE TICKET
// ========================================
async function handleCreateTicket(event) {

    const button = event.target;
    button.disabled = true;

    const customerId = document.getElementById("ticketCustomerId").value;
    const agentId = document.getElementById("ticketAgentId").value;
    const categoryId = parseInt(document.getElementById("ticketCategoryId").value);
    const title = document.getElementById("ticketTitle").value;
    const description = document.getElementById("ticketDescription").value;

    try {

        await apiRequest("/Tickets", "POST", {
            customerId,
            agentId,
            categoryId,
            title,
            description
        });

        // Close modal safely (NO FREEZE FIX)
        const modalElement = document.getElementById("ticketModal");
        const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
        modal.hide();

        document.body.classList.remove("modal-open");
        document.querySelectorAll(".modal-backdrop").forEach(el => el.remove());

        showToast("Ticket created successfully!");

        await loadTickets();

    } catch (error) {
        showToast(error, "error");
    } finally {
        button.disabled = false;
    }
}


// ========================================
// RESOLVE TICKET
// ========================================
async function resolveTicket(ticketId) {

    try {

        const response = await fetch(
            `https://localhost:7227/api/Tickets/resolve/${ticketId}`,
            { method: "PUT" }
        );

        if (!response.ok)
            throw new Error("Failed to resolve ticket");

        showToast("Ticket resolved successfully!");

        await loadTickets();

    } catch (error) {
        showToast(error.message, "error");
    }
}


// ========================================
// APPLY SEARCH + STATUS FILTER
// ========================================
function applyTicketFilters() {

    const searchValue = document
        .getElementById("ticketSearch")
        .value
        .toLowerCase();

    const statusFilter = document
        .getElementById("ticketStatusFilter")
        .value;

    let filtered = allTickets;

    // Search filter
    if (searchValue) {
        filtered = filtered.filter(ticket =>
            ticket.title.toLowerCase().includes(searchValue)
        );
    }

    // Status filter
    if (statusFilter !== "all") {
        filtered = filtered.filter(ticket =>
            ticket.status.toString() === statusFilter
        );
    }

    renderTickets(filtered);
}


// ========================================
// RENDER TICKETS
// ========================================
function renderTickets(tickets) {

    const tbody = document.getElementById("ticketTableBody");
    tbody.innerHTML = "";

    if (!tickets || tickets.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="5" class="text-center">
                    No tickets found.
                </td>
            </tr>`;
        return;
    }

    tickets.forEach(ticket => {

        const id = ticket.ticketId || ticket.id;

        let statusText = "Open";
        if (ticket.status === 1) statusText = "In Progress";
        if (ticket.status === 2) statusText = "Resolved";

        const isResolved = ticket.status === 2;

        tbody.innerHTML += `
            <tr>
                <td>${id}</td>
                <td>${ticket.title}</td>
                <td>${statusText}</td>
                <td>${new Date(ticket.createdAt).toLocaleString()}</td>
                <td>
                    <button class="btn btn-sm btn-success"
                        onclick="resolveTicket('${id}')"
                        ${isResolved ? "disabled" : ""}>
                        Resolve
                    </button>
                </td>
            </tr>
        `;
    });
}