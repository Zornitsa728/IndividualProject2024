document.addEventListener('DOMContentLoaded', () => {
    let index = 0;

    // Add hidden inputs to the form
    const form = document.getElementById('recipe-form');

    const tableBody = document.getElementById('ingredients-table-body');

    // Loop through all rows and add hidden inputs with updated indices
    if (tableBody) {
        Array.from(tableBody.children).forEach((row, newIndex) => {
            const ingredientId = row.getAttribute('data-id');
            const quantity = row.children[1].textContent;
            const unit = row.children[2].textContent;

            form.insertAdjacentHTML(
                'beforeend',
                `<input type="hidden" name="Ingredients[${newIndex}].IngredientId" value="${ingredientId}" class="ingredient-hidden">
             <input type="hidden" name="Ingredients[${newIndex}].Quantity" value="${quantity}" class="ingredient-hidden">
             <input type="hidden" name="Ingredients[${newIndex}].Unit" value="${unit}" class="ingredient-hidden">`
            );
        });
    }

    document.getElementById('add-ingredient-btn')?.addEventListener('click', function () {
        const nameDropdown = document.getElementById('ingredient-name');
        const name = nameDropdown.options[nameDropdown.selectedIndex].text;

        const ingredientId = document.getElementById('ingredient-name').value;

        const quantity = document.getElementById('ingredient-quantity').value;

        const unitDropdown = document.getElementById('ingredient-unit');
        const checkUnit = unitDropdown.options[unitDropdown.selectedIndex].value; // Get the visible text
        const unit = unitDropdown.options[unitDropdown.selectedIndex].text; // Get the visible text

        if (!ingredientId || !quantity || !checkUnit) {
            alert('Please fill all fields.');
            return;
        }

        // Add row to the table
        const row = document.createElement('tr');
        row.setAttribute('data-id', ingredientId);
        row.innerHTML = `
            <td>${name}</td>
            <td>${quantity}</td>
            <td>${unit}</td>
            <td><button type="button" class="btn btn-danger remove-ingredient">Remove</button></td>
        `;
        tableBody?.appendChild(row);

        // First, remove all existing hidden inputs
        form?.querySelectorAll('.ingredient-hidden').forEach(input => input.remove());

        // Loop through all rows and add hidden inputs with updated indices
        if (tableBody) {
            Array.from(tableBody.children).forEach((row, newIndex) => {
                const ingredientId = row.getAttribute('data-id');
                const quantity = row.children[1].textContent;
                const unit = row.children[2].textContent;

                form?.insertAdjacentHTML(
                    'beforeend',
                    `<input type="hidden" name="Ingredients[${newIndex}].IngredientId" value="${ingredientId}" class="ingredient-hidden">
             <input type="hidden" name="Ingredients[${newIndex}].Quantity" value="${quantity}" class="ingredient-hidden">
             <input type="hidden" name="Ingredients[${newIndex}].Unit" value="${unit}" class="ingredient-hidden">`
                );
            });

            index++

            // Clear the input fields
            document.getElementById('ingredient-name').value = '';
            document.getElementById('ingredient-quantity').value = '';
            document.getElementById('ingredient-unit').value = '';
        }
    });

    // Handle remove ingredient
    document.getElementById('ingredients-table-body')?.addEventListener('click', function (e) {
        if (e.target.classList.contains('remove-ingredient')) {
            const row = e.target.closest('tr');
            //const indexToRemove = Array.from(row.parentNode.children).indexOf(row);

            form?.querySelectorAll('.ingredient-hidden').forEach(input => input.remove());

            // Remove table row
            row.remove();

            // Rebuild hidden inputs with updated indices
            if (tableBody) {
                Array.from(tableBody.children).forEach((row, newIndex) => {
                    const cells = row.children;
                    const ingredientId = row.getAttribute('data-id');
                    const quantity = cells[1].textContent;
                    const unit = cells[2].textContent;

                    form?.insertAdjacentHTML(
                        'beforeend',
                        `<input type="hidden" name="Ingredients[${newIndex}].IngredientId" value="${ingredientId}" class="ingredient-hidden">
                     <input type="hidden" name="Ingredients[${newIndex}].Quantity" value="${quantity}" class="ingredient-hidden">
                     <input type="hidden" name="Ingredients[${newIndex}].Unit" value="${unit}" class="ingredient-hidden">`
                    );
                });

                // Update the global index
                index = tableBody.children.length;
            }
        }
    });

}); 

document.addEventListener('DOMContentLoaded', () => {

    const addToCookbookModal = document.getElementById('addToCookbookModal');
    if (addToCookbookModal) {
        addToCookbookModal.addEventListener('show.bs.modal', function (event) {
            // Get the button that triggered the modal
            const button = event.relatedTarget;
    
            // Extract the recipe ID and return URL from the data attributes
            const recipeId = button.getAttribute('data-recipe-id');
            const returnUrl = button.getAttribute('data-return-url');
    
            // Set the hidden input field in the modal
            const modalRecipeIdInput = document.getElementById('modal-recipe-id');
            modalRecipeIdInput.value = recipeId;
    
            // Set the return URL in the hidden input field
            const modalReturnUrlInput = document.getElementById('modal-return-url');
            modalReturnUrlInput.value = returnUrl;
        });
    }

    const removeCookbookModal = document.getElementById('removeCookbookModal');
    if (removeCookbookModal) {
        removeCookbookModal.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget;
            const cookbookId = button.getAttribute('data-cookbook-id');
            const modalCookbookIdInput = removeCookbookModal.querySelector('#modal-cookbook-id');
            modalCookbookIdInput.value = cookbookId;
        });
    }
});

// Toggle Comments Functionality it's not supposed to be in the dom 
    const toggleComments = document.getElementById('toggle-comments');
    if (toggleComments) {
        toggleComments.addEventListener('click', function (event) {
            event.preventDefault();
            const hiddenComments = document.getElementById('hidden-comments');
            if (hiddenComments.style.display === 'none') {
                hiddenComments.style.display = 'block';
                toggleComments.textContent = 'Show Less Comments';
            } else {
                hiddenComments.style.display = 'none';
                toggleComments.textContent = 'Show All Comments';
            }
        });
    }



