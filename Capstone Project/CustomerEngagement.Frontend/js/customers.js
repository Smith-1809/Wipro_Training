// ==============================
// LOAD CUSTOMERS
// ==============================
async function loadCustomers() {

    showLoading("customerTableBody", 4);

    try {
        const customers = await apiRequest("/Customers");
        renderCustomers(customers);
    } catch (error) {
        showToast(error, "error");
    }
}


// ==============================
// CREATE CUSTOMER
// ==============================
async function handleCreateCustomer() {

    const button = event.target;
    button.disabled = true;

    const fullName = document.getElementById("customerName").value;
    const email = document.getElementById("customerEmail").value;
    const phone = document.getElementById("customerPhone").value;

    try {

        await apiRequest("/Customers", "POST", {
            fullName,
            email,
            phone
        });

const customerModalElement = document.getElementById("customerModal");
const customerModal = bootstrap.Modal.getOrCreateInstance(customerModalElement);
customerModal.hide();

document.body.classList.remove("modal-open");
document.querySelectorAll(".modal-backdrop").forEach(el => el.remove());


        showToast("Customer created successfully!");

        await loadCustomers();

    } catch (error) {
        showToast(error, "error");
    } finally {
        button.disabled = false;
    }
}


// ==============================
// RENDER CUSTOMERS
// ==============================
function renderCustomers(customers) {

    const tbody = document.getElementById("customerTableBody");
    tbody.innerHTML = "";

    customers.forEach(customer => {

        tbody.innerHTML += `
            <tr>
                <td>${customer.customerId}</td>
                <td>${customer.fullName}</td>
                <td>${customer.email}</td>
                <td>${customer.phone || ""}</td>
            </tr>
        `;
    });
}