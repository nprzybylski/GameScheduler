const uri = 'Game';
let games = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addTitleTextbox = document.getElementById('add-title');
  const addDescriptionTextbox = document.getElementById('add-description');
  const addGenreTextBox = document.getElementById('add-genre');

  const item = {
    title: addTitleTextbox.value.trim(),
    description: addDescriptionTextbox.value.trim(),
    genre: addGenreTextBox.value.trim()
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
      addDescriptionTextbox.value = '';
      addGenreTextBox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(title) {
  fetch(`${uri}/${title}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(title) {
  const item = games.find(item => item.title === title);
  
  document.getElementById('edit-title').value = item.title;
  document.getElementById('edit-description').value = item.description;
  document.getElementById('edit-genre').value = item.genre;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const item = {
    title: document.getElementById('edit-title').value.trim(),
    description: document.getElementById('edit-description').value.trim(),
    genre: document.getElementById('edit-genre').value.trim()
  };
  
  fetch(`${uri}/${item.title}`, {
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
  const name = (itemCount === 1) ? 'game' : 'games';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('games');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
      
    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.title})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.title})`);

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.title);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode1 = document.createTextNode(item.description);
    td2.appendChild(textNode1);

    let td3 = tr.insertCell(2);
    let textNode2 = document.createTextNode(item.genre);
    td3.appendChild(textNode2);

    let td4 = tr.insertCell(3);
    td4.appendChild(editButton);

    let td5 = tr.insertCell(4);
    td5.appendChild(deleteButton);
  });

  games = data;
}