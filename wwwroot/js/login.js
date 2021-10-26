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
  })
    .then(
        function(response) {
          if(response.status ==200){
            alert("You have logged in");
            _displayItems();
          }else{
            alert("Incorrect Username or Password");
          }
          return;
        });
}

function _displayItems() {
  const tBody = document.getElementById('games');

}
