document.getElementById('loginButton').addEventListener('click', function() {
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    if (!username || !password) {
        errorMessage.textContent = 'Please enter both username and password.';
        return;
    }

    const loginData = {
        username: username,
        password: password
    };

    fetch('/api/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(loginData)
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            window.location.href = '/dashboard';
        } else {
            errorMessage.textContent = data.message;
        }
    })
    .catch(error => {
        console.error('Error during login:', error);
        errorMessage.textContent = 'Login failed. Please try again later.';
    });
});
