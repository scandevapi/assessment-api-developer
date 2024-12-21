document.getElementById('customerForm').addEventListener('submit', function (event) {
    var state = document.getElementById('Customer_State').value;
    var postalCode = document.getElementById('Customer_PostalCode').value;

    if (state && !/^\d{5}(-\d{4})?$/.test(postalCode)) {
        alert('Invalid postal code format for the provided state.');
        event.preventDefault();
    }
});