if (!localStorage.getItem("authToken")) {

    document.body.innerHTML = `
        <div class="d-flex vh-100 justify-content-center align-items-center">
            <div class="card p-4 shadow" style="width:300px;">
                <h5 class="mb-3 text-center">Login</h5>
                <input id="loginUser" class="form-control mb-2" placeholder="Username">
                <input id="loginPass" type="password" class="form-control mb-3" placeholder="Password">
                <button class="btn btn-primary w-100" onclick="login()">Login</button>
            </div>
        </div>
    `;
}

function login() {

    const user = document.getElementById("loginUser").value;
    const pass = document.getElementById("loginPass").value;

    if (user === "admin" && pass === "admin123") {
        localStorage.setItem("authToken", "demo");
        location.reload();
    } else {
        alert("Invalid credentials");
    }
}

function logout() {
    localStorage.removeItem("authToken");
    location.reload();
}