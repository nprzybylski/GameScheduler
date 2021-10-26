const uri = 'User';
let users = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');
  const addPassTextbox = document.getElementById('add-pass');

  const item = {
    name: addNameTextbox.value.trim(),
    password: addPassTextbox.value.trim()
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
      addPassTextbox.value = '';
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
  document.getElementById('edit-pass').value = item.password;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const item = {
    name: document.getElementById('edit-name').value.trim(),
    password: document.getElementById('edit-pass').value.trim(),
  };
  
  fetch(`${uri}/${item.name}`, {
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
    editButton.setAttribute('onclick', `displayEditForm(${item.name})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.name})`);

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.name);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode1 = document.createTextNode(item.password);
    td2.appendChild(textNode1);

    let td4 = tr.insertCell(2);
    td4.appendChild(editButton);

    let td5 = tr.insertCell(3);
    td5.appendChild(deleteButton);
  });

  users = data;
}