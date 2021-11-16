const uri = '[GameEvents]';
let events = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addTitleTextbox = document.getElementById('add-title');
  const addGameTitleTextBox = document.getElementById('add-gametitle');
  const addDescriptionTextbox = document.getElementById('add-description');
  const addUsersTextbox = document.getElementById('add-users');
  const addCapacityTextBox = document.getElementById('add-capacity');
  const addTimeTextbox = document.getElementById('add-time');
  const addIdTextbox = document.getElementById('add-id');
  

  const item = {
    title: addTitleTextbox.value.trim(),
    gameTitle: addGameTitleTextBox.value.trim(),
    description: addDescriptionTextbox.value.trim(),
    users: addUsersTextbox.value.trim(),
    capacity: addCapacityTextBox.value.trim(),
    time: addTimeTextbox.value.trim(),
    id: addIdTextbox.value.trim()
    
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
      addTitleTextbox.value = '';
      addGameTitleTextBox.value = '';
      addDescriptionTextbox.value = '';
      addUsersTextbox.value = '';
      addCapacityTextBox.value = '';
      addTimeTextbox.value = '';
      addIdTextbox.value = '';
      
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
  const item = events.find(item => item.id === id);
  
  document.getElementById('edit-title').value = item.title;
  document.getElementById('edit-gametitle').value = item.gameTitle;
  document.getElementById('edit-description').value = item.description;
  document.getElementById('edit-users').value = item.users;
  document.getElementById('edit-capacity').value = item.capacity;
  document.getElementById('edit-time').value = item.time;
  document.getElementById('edit-id').value = item.id;
  
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;  
  const item = {
    title: document.getElementById('edit-title').value,
    gametitle: document.getElementById('edit-gametitle').value,
    description: document.getElementById('edit-description').value,
    users: document.getElementById('edit-users').value,
    capacity: document.getElementById('edit-capacity').value,
    time: document.getElementById('edit-time').value,
    id: document.getElementById('edit-id').value
  };
  
  fetch(`${uri}/${itemId}`, {
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
  const name = (itemCount === 1) ? 'event' : 'events';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('events');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
      
    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    //editButton.setAttribute('onclick', `displayEditForm(${item.title})`);
    editButton.onclick = function() { displayEditForm(item.id) };

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    // deleteButton.setAttribute('onclick', `deleteItem(${item.title})`);
    deleteButton.onclick = function() { deleteItem(item.id) };

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.title);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode1 = document.createTextNode(item.gameTitle);
    td2.appendChild(textNode1);

    let td3 = tr.insertCell(2);
    let textNode2 = document.createTextNode(item.description);
    td3.appendChild(textNode2);

    let td4 = tr.insertCell(3);
    let textNode3 = document.createTextNode(item.users);
    td4.appendChild(textNode3);

    let td5 = tr.insertCell(4);
    let textNode4 = document.createTextNode(item.capacity);
    td5.appendChild(textNode4);

    let td6 = tr.insertCell(5);
    let textNode5 = document.createTextNode(item.time);
    td6.appendChild(textNode5);

    let td7 = tr.insertCell(6);
    let textNode6 = document.createTextNode(item.id);
    td7.appendChild(textNode6);

    let td8 = tr.insertCell(7);
    td8.appendChild(editButton);

    let td9 = tr.insertCell(8);
    td9.appendChild(deleteButton);
  });

  events = data;
}