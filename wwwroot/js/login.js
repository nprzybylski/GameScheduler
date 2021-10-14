const uri = 'registration/api/user';
let user = [];

//LOGIN
function loginUser() {
  const NameTextbox = document.getElementById('username').value;
  const PasswordTextbox = document.getElementById('password').value;

  const login = {
    name: NameTextbox.trim(),
    password: PasswordTextbox.trim()
  };
  console.log(login.name);
  console.log(login.password);
  fetch(`${uri}/${NameTextbox}/${PasswordTextbox}`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(login)
  }
  )
    .then(response => response.json())
    .catch(error => console.error('Unable to login user.', error));
}