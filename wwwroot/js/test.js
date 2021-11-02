const uri = 'api/TodoItems';
let todos = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');
  const addStatusDropdown = document.getElementById('add-status');
  const addPersonTextbox = document.getElementById('add-person');
  const addPriorityTextBox = document.getElementById('add-priority');

  const item = {
    name: addNameTextbox.value.trim(),
    personAssigned: addPersonTextbox.value.trim(),
    priority: addPriorityTextBox.value.trim(),
    name: addNameTextbox.value.trim(),
    status: addStatusDropdown.value
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
      addPersonTextbox.value = '';
      addPriorityTextBox.value = '';
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
  const item = todos.find(item => item.id === id);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-person').value = item.personAssigned;
  document.getElementById('edit-priority').value = item.priority;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-status').value = item.status;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    status: document.getElementById('edit-status').value,
    name: document.getElementById('edit-name').value.trim(),
    personAssigned: document.getElementById('edit-person').value.trim(),
    priority: document.getElementById('edit-priority').value.trim(),
    name: document.getElementById('edit-name').value.trim()
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
  const name = (itemCount === 1) ? 'to-do' : 'to-dos';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
    let StatusDropdown = document.createElement('input');
    StatusDropdown.type = 'select-one';
    StatusDropdown.disabled = true;
    StatusDropdown.value = item.status;

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    
    td1.appendChild(StatusDropdown);

    let td2 = tr.insertCell(1);
    let textNode = document.createTextNode(item.name);
    td2.appendChild(textNode);

    let td3 = tr.insertCell(2);
    let textNode1 = document.createTextNode(item.personAssigned);
    td3.appendChild(textNode1);

    let td4 = tr.insertCell(3);
    let textNode2 = document.createTextNode(item.priority);
    td4.appendChild(textNode2);

    let td5 = tr.insertCell(4);
    td5.appendChild(editButton);

    let td6 = tr.insertCell(5);
    td6.appendChild(deleteButton);
  });

  todos = data;
}