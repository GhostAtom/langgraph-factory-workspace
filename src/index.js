import React from 'react';
import ReactDOM from 'react-dom';
import './styles.css';

function App() {
    return (
        <div>
            <header>
                <h1>Welcome to Our Website</h1>
                <nav>
                    <a href="#">Home</a>
                    <a href="#">About</a>
                    <a href="#">Contact</a>
                </nav>
            </header>
            <div className="container">
                <p>Discover the amazing features we offer!</p>
            </div>
        </div>
    );
}

ReactDOM.render(<App />, document.getElementById('root'));