import React, { useState } from 'react';

function YoComponent({ sendYo }) {
    const [friend, setFriend] = useState('');

    const handleYo = () => {
        sendYo(friend);
    };

    return (
        <div>
            <input 
                type="text" 
                placeholder="Enter friend's name" 
                value={friend} 
                onChange={(e) => setFriend(e.target.value)}
            />
            <button onClick={handleYo}>Send Yo</button>
        </div>
    );
}

export default YoComponent;
