    let index = 0;
document.getElementById('add-ingredient-btn').addEventListener('click', function () {
    const nameDropdown = document.getElementById('ingredient-name');
    const Name = nameDropdown.options[nameDropdown.selectedIndex].text;

    const IngredientId = document.getElementById('ingredient-name').value; 

    const Quantity = document.getElementById('ingredient-quantity').value;

    const unitDropdown = document.getElementById('ingredient-unit');
    const Unit = unitDropdown.options[unitDropdown.selectedIndex].text; // Get the visible text

    if (!IngredientId || !Quantity || !Unit) {
        alert('Please fill all fields.');
        return;
    }

    // Add row to the table
    const tableBody = document.getElementById('ingredients-table-body');
    const row = document.createElement('tr');
    row.innerHTML = `
        <td>${Name}</td>
        <td>${Quantity}</td>
        <td>${Unit}</td>
        <td><button type="button" class="remove-ingredient">Remove</button></td>
    `;
    tableBody.appendChild(row);
     

    // Add hidden inputs to the form
    const form = document.getElementById('recipe-form');
    form.insertAdjacentHTML(
        'beforeend',
        `<input type="hidden" name="Ingredients[${index}].IngredientId" value="${IngredientId}" class="ingredient-hidden">
         <input type="hidden" name="Ingredients[${index}].Quantity" value="${Quantity}" class="ingredient-hidden">
         <input type="hidden" name="Ingredients[${index}].Unit" value="${Unit}" class="ingredient-hidden">`
    );
        index++

    // Clear the input fields
    document.getElementById('ingredient-name').value = '';
    document.getElementById('ingredient-quantity').value = '';
    document.getElementById('ingredient-unit').value = '';
});

// Handle remove ingredient
document.getElementById('ingredients-table-body').addEventListener('click', function (e) {
    if (e.target.classList.contains('remove-ingredient')) {
        const row = e.target.closest('tr');
        const indexToRemove = Array.from(row.parentNode.children).indexOf(row);

        // Remove hidden inputs
        const form = document.getElementById('recipe-form');
        form.querySelectorAll('.ingredient-hidden').forEach(input => input.remove());

        // Remove table row
        row.remove();

        // Rebuild hidden inputs with updated indices
        const tableBody = document.getElementById('ingredients-table-body');
        Array.from(tableBody.children).forEach((row, newIndex) => {
            const cells = row.children;
            const ingredientId = cells[0].textContent;
            const quantity = cells[1].textContent;
            const unit = cells[2].textContent;

            form.insertAdjacentHTML(
                'beforeend',
                `<input type="hidden" name="Ingredients[${newIndex}].IngredientId" value="${ingredientId}" class="ingredient-hidden">
                 <input type="hidden" name="Ingredients[${newIndex}].Quantity" value="${quantity}" class="ingredient-hidden">
                 <input type="hidden" name="Ingredients[${newIndex}].Unit" value="${unit}" class="ingredient-hidden">`
            );
        });

        // Update the global index
        index = tableBody.children.length;
    }
});


