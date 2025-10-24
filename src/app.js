document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    if (username && password) {
        // Assuming we have an endpoint '/api/login' to handle login
        fetch('/api/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username, password })
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Login failed');
            }
            return response.json();
        })
        .then(data => {
            alert('Login successful');
            // Redirect to a new page or load user dashboard
        })
        .catch(error => {
            alert(error.message);
        });
    } else {
        alert('Please enter both username and password');
    }
});
