const uri = 'registration/api/user';
let todos = [];

//Add new user
function addUser() {
    const addNameTextbox = document.getElementById('add-username');
    const addPasswordTextbox = document.getElementById('add-password');
    const addConfirmPasswordTextbox = document.getElementById("confirm-password");
    const addBioTextBox = document.getElementById('add-bio');
  
    const user = {
      name: addNameTextbox.value.trim(),
      password: addPasswordTextbox.value.trim(),
      bio: addBioTextBox.value.trim()
    };
    if((confirmPassword(addPasswordTextbox,addConfirmPasswordTextbox)) == true){
      console.log(user.name);
      console.log(user.password);
      console.log(user.bio);
      fetch(uri, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
      })
        .then(response => response.json())
        .catch(error => console.error('Unable to add user.', error));
    }else{
      alert("INCORRECT BTICH")
    }
  }
function confirmPassword(pass, confirmPass){
  if (pass.value != confirmPass.value){
    return false
  }else{
  return true;
  }
}