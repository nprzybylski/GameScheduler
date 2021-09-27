const uri = 'GameScheduler';
let user = [];

//LOGIN
function loginUser() {
  const addNameTextbox = document.getElementById('username');
  const addPasswordTextbox = document.getElementById('password');

  const user = {
    name: addNameTextbox.value.trim(),
    password: addPasswordTextbox.value.trim()
  };
  console.log(user.name);
  console.log(user.password);
  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(user)
  })
    .then(response => response.json())

    .catch(error => console.error('Unable to login user.', error));
}
