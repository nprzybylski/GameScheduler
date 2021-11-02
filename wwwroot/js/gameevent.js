const uri = '[GameEvents]';
//Test variables for GetByDate
let todos = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

// Fetch Date:

function getEventDate(e){
  var date = e.target.value;
  //sessionStorage.setItem('date', e.target.value)
  alert(e.target.value);

  if(e.target.value == ''){
    getItems();
  }else{
    getItemsByDate(e);
  }
  
}

function getEventGameTitle(t){
  var gametitle = t.target.value;
  //sessionStorage.setItem('date', e.target.value)
  alert(t.target.value);
  getItemsByGameTitleAndDate()

}

// Fetch events by date

function getItemsByDate(e) {
  //fetch(`${uri}/${date}`)
    //2021-09-27
  fetch(`${uri}/date/${e.target.value}`) 
    .then(response => response.json())
    .then(data => _displayItemsByDate(data))
    .catch(error => console.error('Unable to get items.', error));
}

function getItemsByGameTitleAndDate(t){
  fetch(`${uri}/date/${t.target.value}`) 
    .then(response => response.json())
    .then(data => _displayItemsByDateAndTitle(data))
    .catch(error => console.error('Unable to get items.', error));

}
//--------------------------------------------------------------------------------
function addItem() {
  const addNameTextbox = document.getElementById('add-name');

  const item = {
    isComplete: false,
    name: addNameTextbox.value.trim()
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
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-isComplete').checked = item.isComplete;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    isComplete: document.getElementById('edit-isComplete').checked,
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
  const name = (itemCount === 1) ? 'Game Event' : 'Game Events';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('eventByDateTable');
  tBody.innerHTML = '';


  const button = document.createElement('button');

  data.forEach(item => {
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let textNode1 = document.createTextNode(item.id)
    td1.appendChild(textNode1);

    

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(item.title);
    td2.appendChild(textNode2);


    let td3 = tr.insertCell(2);
    let textNode3 = document.createTextNode(item.users);
    td3.appendChild(textNode3);


    let td4 = tr.insertCell(3);
    let textNode4 = document.createTextNode(item.gameTitle);
    td4.appendChild(textNode4);

    let td5 = tr.insertCell(4);
    let textNode5 = document.createTextNode(item.capacity);
    td5.appendChild(textNode5);

    let td6 = tr.insertCell(5);
    let textNode6 = document.createTextNode(item.time);
    td6.appendChild(textNode6);

    let td7 = tr.insertCell(6);
    let textNode7 = document.createTextNode(item.description);
    td7.appendChild(textNode7);

  });

  eventTable = data;
}


// Display items by date

function _displayItemsByDate(data){
  
  const tBody = document.getElementById('eventByDateTable');
  tBody.innerHTML = '';

  //_displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
   
   
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let textNode1 = document.createTextNode(item.id)
    td1.appendChild(textNode1);

    

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(item.title);
    td2.appendChild(textNode2);


    let td3 = tr.insertCell(2);
    let textNode3 = document.createTextNode(item.users);
    td3.appendChild(textNode3);


    let td4 = tr.insertCell(3);
    let textNode4 = document.createTextNode(item.gameTitle);
    td4.appendChild(textNode4);

    let td5 = tr.insertCell(4);
    let textNode5 = document.createTextNode(item.capacity);
    td5.appendChild(textNode5);

    let td6 = tr.insertCell(5);
    let textNode6 = document.createTextNode(item.time);
    td6.appendChild(textNode6);

    let td7 = tr.insertCell(6);
    let textNode7 = document.createTextNode(item.description);
    td7.appendChild(textNode7);

  });

  eventByDateTable = data;



}