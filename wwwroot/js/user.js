const uri = 'registration/api/user';
let users = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');
  const addPasswordTextbox = document.getElementById('add-password');
  const addBioTextBox = document.getElementById('add-bio');

  const item = {
    name: addNameTextbox.value.trim,
    password: addPasswordTextbox.value,
    bio: addBioTextBox.value
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox.value = '';
      addPasswordTextbox.value = '';
      addBioTextBox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(name) {
  fetch(`${uri}/${name}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(name) {
  const item = users.find(item => item.name === name);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-password').value = item.password;
  document.getElementById('edit-bio').value = item.bio;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemName = document.getElementById('edit-name').value;  
  const item = {
    name: document.getElementById('edit-name').value,
    password: document.getElementById('edit-password').value,
    bio: document.getElementById('edit-bio').value
  };
  
  fetch(`${uri}/${itemName}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'user' : 'users';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('users');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
      
    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    //editButton.setAttribute('onclick', `displayEditForm(${item.name})`);
    editButton.onclick = function() { displayEditForm(item.name) };

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    // deleteButton.setAttribute('onclick', `deleteItem(${item.name})`);
    deleteButton.onclick = function() { deleteItem(item.name) };

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.name);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode1 = document.createTextNode(item.password);
    td2.appendChild(textNode1);

    let td3 = tr.insertCell(2);
    let textNode2 = document.createTextNode(item.bio);
    td3.appendChild(textNode2);

    let td4 = tr.insertCell(3);
    td4.appendChild(editButton);

    let td5 = tr.insertCell(4);
    td5.appendChild(deleteButton);
  });

  users = data;
}