// ==============================
// SHOW TOAST
// ==============================
function showToast(message, type = "success") {

    const toastEl = document.getElementById("appToast");
    const toastMessage = document.getElementById("toastMessage");

    toastMessage.innerText = message;

    toastEl.classList.remove("text-bg-success", "text-bg-danger");

    if (type === "error") {
        toastEl.classList.add("text-bg-danger");
    } else {
        toastEl.classList.add("text-bg-success");
    }

    const toast = new bootstrap.Toast(toastEl);
    toast.show();
}


// ==============================
// SHOW LOADING ROW
// ==============================
function showLoading(tableBodyId, colSpan = 5) {

    const tbody = document.getElementById(tableBodyId);
    tbody.innerHTML =
        `<tr>
            <td colspan="${colSpan}" class="text-center">
                Loading...
            </td>
        </tr>`;
}