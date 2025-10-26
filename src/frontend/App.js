import React from 'react';
import YoComponent from './ui/YoComponent';

function App() {
    const sendYo = async (friend) => {
        await fetch('http://localhost:3001/sendYo', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({ friend })
        });
    };

    return (
        <div className="App">
            <h1>Send a Yo!</h1>
            <YoComponent sendYo={sendYo} />
        </div>
    );
}

export default App;
