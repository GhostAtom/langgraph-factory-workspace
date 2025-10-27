import React from 'react';
import YellowButton from './components/YellowButton';

function App() {
    const handleButtonClick = () => {
        alert('Button clicked!');
    };

    return (
        <div className="App">
            <h1>Welcome to the App</h1>
            <YellowButton onClick={handleButtonClick}>Click Me</YellowButton>
        </div>
    );
}

export default App;
