const uri = 'api/catbreeds';
let catbreeds = [];

function getCatBreeds() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCatBreeds(data))
        .catch(error => console.error('Unable to get cat breeds.', error));
}

function addCatBreed() {
    const addNameTextbox = document.getElementById('add-name');
    const addInfoTextbox = document.getElementById('add-description');

    const catbreed = {
        name: addNameTextbox.value.trim(),
        description: addInfoTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(catbreed)
    })
        .then(response => response.json())
        .then(() => {
            getCatBreeds();
            addNameTextbox.value = '';
            addInfoTextbox.value = '';
        })
        .catch(error => console.error('Unable to add cat breed.', error));
}

function deleteCatBreed(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getCatBreeds())
        .catch(error => console.error('Unable to delete cat breed.', error));
}

function displayEditForm(id) {
    const catbreed = catbreeds.find(cb => cb.id === id);

    document.getElementById('edit-id').value = catbreed.id;
    document.getElementById('edit-name').value = catbreed.name;
    document.getElementById('edit-description').value = catbreed.description;
    document.getElementById('editForm').style.display = 'block';
}

function updateCatBreed() {
    const catbreedId = document.getElementById('edit-id').value;
    const catbreed = {
        id: parseInt(catbreedId, 10),
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim()
    };

    console.log(`${uri}/${catbreedId}`);
    console.log(JSON.stringify(catbreed));

    fetch(`${uri}/${catbreedId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(catbreed)
    })
        .then(() => getCatBreeds())
        .catch(error => console.error('Unable to update category.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayCatBreeds(data) {
    const tBody = document.getElementById('catbreeds');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(cb => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${cb.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCatBreed(${cb.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(cb.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(cb.description);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    catbreeds = data;
}
