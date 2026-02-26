// ================= GLOBAL STATE =================

let allTickets = [];
let filteredTickets = [];
let currentPage = 1;
const pageSize = 5;

// ================= LOAD TICKETS =================

async function loadTickets() {

    try {
        const tbody = document.getElementById("ticketTableBody");
        tbody.innerHTML = `<tr><td colspan="5" class="text-center">Loading...</td></tr>`;

        allTickets = await getTickets();

        applyTicketFilters();

        updateDashboardCards();

    } catch (error) {
        showToast("Failed to load tickets", "error");
    }
}

// ================= FILTER + SEARCH =================

function applyTicketFilters() {

    const searchText = document.getElementById("ticketSearch").value.toLowerCase();
    const statusFilter = document.getElementById("ticketStatusFilter").value;

    filteredTickets = allTickets.filter(ticket => {

        const matchesSearch =
            ticket.title.toLowerCase().includes(searchText) ||
            ticket.ticketId.toLowerCase().includes(searchText);

        const matchesStatus =
            statusFilter === "all" ||
            ticket.status.toString() === statusFilter;

        return matchesSearch && matchesStatus;
    });

    currentPage = 1;
    renderTickets();
}

// ================= RENDER =================

function renderTickets() {

    const tbody = document.getElementById("ticketTableBody");
    tbody.innerHTML = "";

    if (filteredTickets.length === 0) {
        tbody.innerHTML = `<tr><td colspan="5" class="text-center">No tickets found</td></tr>`;
        renderPagination();
        return;
    }

    const start = (currentPage - 1) * pageSize;
    const paginatedTickets = filteredTickets.slice(start, start + pageSize);

    paginatedTickets.forEach(ticket => {

        const statusText =
            ticket.status === 0 ? "Open" :
            ticket.status === 1 ? "In Progress" : "Resolved";

let actionButton = "";

if (ticket.status === 0) {
    actionButton = `
        <button class="btn btn-sm btn-warning me-1"
            onclick="markInProgress('${ticket.ticketId}')">
            Start
        </button>
    `;
} 
else if (ticket.status === 1) {
    actionButton = `
        <button class="btn btn-sm btn-success"
            onclick="resolveTicket('${ticket.ticketId}')">
            Resolve
        </button>
    `;
} 
else {
    actionButton = `
        <button class="btn btn-sm btn-secondary" disabled>
            Resolved
        </button>
    `;
}

        tbody.innerHTML += `
            <tr>
                <td>${ticket.ticketId}</td>
                <td>${ticket.title}</td>
                <td class="${
    ticket.status === 0 ? "status-open" :
    ticket.status === 1 ? "status-progress" :
    "status-resolved"
}">
    ${statusText}
</td>
                <td>${new Date(ticket.createdAt).toLocaleString()}</td>
                <td>${actionButton}</td>
            </tr>
        `;
    });

    renderPagination();
}

// ================= PAGINATION =================

function renderPagination() {

    const pagination = document.getElementById("ticketPagination");
    pagination.innerHTML = "";

    const totalPages = Math.ceil(filteredTickets.length / pageSize);
    if (totalPages <= 1) return;

    pagination.innerHTML += `
        <li class="page-item ${currentPage === 1 ? "disabled" : ""}">
            <a class="page-link" href="#" onclick="changePage(${currentPage - 1})">Previous</a>
        </li>
    `;

    for (let i = 1; i <= totalPages; i++) {
        pagination.innerHTML += `
            <li class="page-item ${currentPage === i ? "active" : ""}">
                <a class="page-link" href="#" onclick="changePage(${i})">${i}</a>
            </li>
        `;
    }

    pagination.innerHTML += `
        <li class="page-item ${currentPage === totalPages ? "disabled" : ""}">
            <a class="page-link" href="#" onclick="changePage(${currentPage + 1})">Next</a>
        </li>
    `;
}

function changePage(page) {

    const totalPages = Math.ceil(filteredTickets.length / pageSize);

    if (page < 1 || page > totalPages) return;

    currentPage = page;
    renderTickets();
}

// ================= CREATE TICKET =================

async function handleCreateTicket(event) {

    event.preventDefault();

    const button = event.target;
    button.disabled = true;

    const customerIdInput = document.getElementById("ticketCustomerId");
    const agentIdInput = document.getElementById("ticketAgentId");
    const categoryIdInput = document.getElementById("ticketCategoryId");
    const titleInput = document.getElementById("ticketTitle");
    const descriptionInput = document.getElementById("ticketDescription");

    const customerId = customerIdInput.value.trim();
    const agentId = agentIdInput.value.trim();
    const categoryId = parseInt(categoryIdInput.value);
    const title = titleInput.value.trim();
    const description = descriptionInput.value.trim();

    let isValid = true;

// ================= VALIDATIONS =================

    [customerIdInput, agentIdInput, categoryIdInput, titleInput, descriptionInput]
        .forEach(i => i.classList.remove("is-invalid"));

    const guidRegex = /^[0-9a-fA-F-]{36}$/;

    if (!guidRegex.test(customerId)) {
        customerIdInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!guidRegex.test(agentId)) {
        agentIdInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!categoryId) {
        categoryIdInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!title || title.length < 5) {
        titleInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!description || description.length < 10) {
        descriptionInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!isValid) {
        showToast("Please correct highlighted fields.", "error");
        button.disabled = false;
        return;
    }

    try {
        await createTicket({
            customerId,
            agentId,
            categoryId,
            title,
            description
        });

        showToast("Ticket created successfully");
        loadTickets();

        const modal = bootstrap.Modal.getInstance(document.getElementById("ticketModal"));
        modal.hide();

        customerIdInput.value = "";
        agentIdInput.value = "";
        categoryIdInput.value = "";
        titleInput.value = "";
        descriptionInput.value = "";

    } catch (error) {
        showToast(error.message, "error");
    }

    button.disabled = false;
}

// ================= RESOLVE TICKET =================

async function resolveTicket(ticketId) {

    const ticket = allTickets.find(t => t.ticketId === ticketId);

    if (!ticket) return;

    if (ticket.status === 2) {
        showToast("Ticket already resolved.", "error");
        return;
    }

    try {
        await fetch(`https://localhost:7227/api/Tickets/resolve/${ticketId}`, {
            method: "PUT"
        });

        showToast("Ticket resolved successfully");
        loadTickets();

    } catch (error) {
        showToast("Failed to resolve ticket", "error");
    }
}

async function markInProgress(ticketId) {

    const ticket = allTickets.find(t => t.ticketId === ticketId);
    if (!ticket) return;

    try {

        const response = await fetch(
            `${BASE_URL}/Tickets`,
            {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    ticketId: ticket.ticketId,
                    title: ticket.title,
                    description: ticket.description,
                    status: 1
                })
            }
        );

        if (!response.ok) {
            throw new Error("Update failed");
        }

        showToast("Ticket moved to In Progress");

        await loadTickets(); // immediate refresh

    } catch (error) {
        console.error(error);
        showToast("Failed to update ticket", "error");
    }
}

// ================= DASHBOARD UPDATE =================

function updateDashboardCards() {

    if (!allTickets) return;

    const total = allTickets.length;
    const open = allTickets.filter(t => t.status === 0).length;
    const resolved = allTickets.filter(t => t.status === 2).length;

    const totalEl = document.getElementById("totalTickets");
    const openEl = document.getElementById("openTickets");
    const resolvedEl = document.getElementById("resolvedTickets");

    if (totalEl) totalEl.innerText = total;
    if (openEl) openEl.innerText = open;
    if (resolvedEl) resolvedEl.innerText = resolved;
}