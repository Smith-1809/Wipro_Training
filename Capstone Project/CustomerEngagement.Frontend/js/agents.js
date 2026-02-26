// ================= GLOBAL =================
let allAgents = [];

// ================= LOAD AGENTS =================
async function loadAgents() {

    try {
        const response = await apiRequest("/Agents");
        allAgents = response;

        renderAgents();
        populateAgentDropdown();

    } catch (error) {
        showToast("Failed to load agents", "error");
    }
}

// ================= RENDER =================
async function renderAgents() {

    const tbody = document.getElementById("agentTableBody");
    tbody.innerHTML = "";

    for (const agent of allAgents) {

        const workloadRes = await apiRequest(`/Agents/workload/${agent.agentId}`);
        const workload = workloadRes?.activeTickets ?? 0;

        const actionBtn = agent.isActive
            ? `<button class="btn btn-sm btn-danger"
                onclick="deactivateAgent('${agent.agentId}')">
                Deactivate
               </button>`
            : `<span class="text-muted">Inactive</span>`;

        tbody.innerHTML += `
            <tr>
                <td>${agent.fullName}</td>
                <td>${agent.email}</td>
                <td>${agent.isActive ? "Yes" : "No"}</td>
                <td>${workload}</td>
                <td>${actionBtn}</td>
            </tr>
        `;
    }
}

// ================= CREATE AGENT =================
async function handleCreateAgent(event) {

    event.preventDefault();

    const nameInput = document.getElementById("agentName");
    const emailInput = document.getElementById("agentEmail");

    const name = nameInput.value.trim();
    const email = emailInput.value.trim();

    let isValid = true;

    [nameInput, emailInput].forEach(i => i.classList.remove("is-invalid"));

    if (!name || name.length < 3) {
        nameInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!email || !email.includes("@")) {
        emailInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!isValid) return;

    try {

        await apiRequest("/Agents", "POST", {
            fullName: name,
            email: email
        });

        showToast("Agent created successfully");

        const modal = bootstrap.Modal.getInstance(
            document.getElementById("agentModal")
        );
        modal.hide();

        nameInput.value = "";
        emailInput.value = "";

        loadAgents();

    } catch (error) {
        showToast("Failed to create agent", "error");
    }
}

// ================= DEACTIVATE =================
async function deactivateAgent(id) {

    try {
        await apiRequest(`/Agents/deactivate/${id}`, "PUT");
        showToast("Agent deactivated");
        loadAgents();
    } catch {
        showToast("Failed to deactivate", "error");
    }
}

// ================= POPULATE DROPDOWN =================
function populateAgentDropdown() {

    const select = document.getElementById("ticketAgentId");
    if (!select) return;

    select.innerHTML = "";

    allAgents
        .filter(a => a.isActive)
        .forEach(agent => {
            select.innerHTML += `
                <option value="${agent.agentId}">
                    ${agent.fullName}
                </option>
            `;
        });
}