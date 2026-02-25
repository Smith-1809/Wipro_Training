let allCustomers = [];
let customerPage = 1;
const customerPageSize = 5;


// ================= LOAD =================
async function loadCustomers() {

    try {

        const customers = await apiRequest("/Customers");
        allCustomers = customers;

        const totalCustomersEl = document.getElementById("totalCustomers");
        if (totalCustomersEl)
            totalCustomersEl.innerText = customers.length;

        customerPage = 1;
        renderCustomers();

    } catch (error) {
        showToast(error, "error");
    }
}


// ================= CREATE =================
async function handleCreateCustomer(event) {

    event.preventDefault();

    const nameInput = document.getElementById("customerName");
    const emailInput = document.getElementById("customerEmail");
    const phoneInput = document.getElementById("customerPhone");

    const name = nameInput.value.trim();
    const email = emailInput.value.trim();
    const phone = phoneInput.value.trim();

    let isValid = true;

    // Reset validation styles
    [nameInput, emailInput, phoneInput].forEach(i => {
        i.classList.remove("is-invalid");
    });

    // Name validation
    if (!name || name.length < 3) {
        nameInput.classList.add("is-invalid");
        isValid = false;
    }

    // Email validation
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email || !emailRegex.test(email)) {
        emailInput.classList.add("is-invalid");
        isValid = false;
    }

    // Phone validation
    const phoneRegex = /^[0-9]{8,15}$/;
    if (!phoneRegex.test(phone)) {
        phoneInput.classList.add("is-invalid");
        isValid = false;
    }

    if (!isValid) {
        showToast("Please correct highlighted fields.", "error");
        return;
    }

    try {
        await createCustomer({ fullName: name, email, phone });
        showToast("Customer created successfully");
        loadCustomers();

        const modal = bootstrap.Modal.getInstance(document.getElementById("customerModal"));
        modal.hide();

        nameInput.value = "";
        emailInput.value = "";
        phoneInput.value = "";

    } catch (error) {
        showToast(error.message, "error");
    }
}

// ================= RENDER =================
function renderCustomers() {

    const tbody = document.getElementById("customerTableBody");
    tbody.innerHTML = "";

    if (!allCustomers || allCustomers.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center">No customers found.</td>
            </tr>`;
        return;
    }

    const totalPages = Math.ceil(allCustomers.length / customerPageSize);

    if (customerPage > totalPages)
        customerPage = totalPages;

    const start = (customerPage - 1) * customerPageSize;
    const end = start + customerPageSize;

    const pageCustomers = allCustomers.slice(start, end);

    pageCustomers.forEach(c => {
        tbody.innerHTML += `
            <tr>
                <td>${c.customerId}</td>
                <td>${c.fullName}</td>
                <td>${c.email}</td>
                <td>${c.phone}</td>
            </tr>`;
    });

    renderCustomerPagination(totalPages);
}


// ================= PAGINATION =================
function renderCustomerPagination(totalPages) {

    const pagination = document.getElementById("customerPagination");
    pagination.innerHTML = "";

    pagination.innerHTML += `
        <li class="page-item ${customerPage === 1 ? "disabled" : ""}">
            <button class="page-link"
                onclick="changeCustomerPage(${customerPage - 1})">
                Previous
            </button>
        </li>
    `;

    for (let i = 1; i <= totalPages; i++) {
        pagination.innerHTML += `
            <li class="page-item ${i === customerPage ? "active" : ""}">
                <button class="page-link"
                    onclick="changeCustomerPage(${i})">
                    ${i}
                </button>
            </li>`;
    }

    pagination.innerHTML += `
        <li class="page-item ${customerPage === totalPages ? "disabled" : ""}">
            <button class="page-link"
                onclick="changeCustomerPage(${customerPage + 1})">
                Next
            </button>
        </li>
    `;
}


function changeCustomerPage(page) {

    const totalPages = Math.ceil(allCustomers.length / customerPageSize);

    if (page < 1 || page > totalPages) return;

    customerPage = page;
    renderCustomers();
}