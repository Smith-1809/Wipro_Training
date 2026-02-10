const sections = document.querySelectorAll(".section");
const navBtns = document.querySelectorAll(".nav-btn");

const showSection = (id) => {
    sections.forEach(sec => sec.classList.add("d-none"));
    document.getElementById(id).classList.remove("d-none");
};

navBtns.forEach(btn =>
    btn.addEventListener("click", () =>
    showSection(btn.dataset.target)
)
);

document.getElementsById("goRequest")
.addEventListener("click", () => showSection("request"));

/*======== FETCH SERVICES =============*/

const loadServices = async () => {
    try {
        const res = await fetch("data.json");
        const services = await res.json();

        const container = document.getElementsById("serviceCards");

        services.forEach(({name, desc})) => {
            container.innerHTML += `
            <div class="col-md-4">
             <div class="card p-3 m-2">
              <h5>${name}</h5>
              <p>${desc}</p>
              <button class="btn btn-primary">Apply</button>
            </div>
         </div>`;
    });

  } catch {
    alert("Failed to load services");
  }
};

loadServices();

/* ============== FORM =========== */

const requests = [];

document.getElementById("requestForm").addEventListener("submit", (e) => {

  e.preventDefault();

  const name = nameInput.value;
  const email = emailInput.value;
  const type = typeInput.value;
  const desc = descInput.value;

  if(!name || !email || !type || !desc){
    msg.textContent = "All fields required";
    return;
  }

  const req = {name,email,type,desc};
  requests.push({...req});

  tableBody.innerHTML += `
    <tr>
      <td>${name}</td>
      <td>${email}</td>
      <td>${type}</td>
      <td>${desc}</td>
    </tr>
  `;

  requestForm.reset();
  msg.textContent = "Submitted Successfully";
});
