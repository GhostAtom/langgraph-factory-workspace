import React, { useState } from 'react';
import createFdpFile from './createFile';

function App() {
  const [directory, setDirectory] = useState('');
  const [message, setMessage] = useState('');

  const handleCreateFile = async () => {
    try {
      const result = await createFdpFile(directory);
      setMessage(result);
    } catch (error) {
      setMessage(error);
    }
  };

  return (
    <div>
      <h1>Create "fdp" File</h1>
      <input
        type="text"
        placeholder="Enter directory path"
        value={directory}
        onChange={(e) => setDirectory(e.target.value)}
      />
      <button onClick={handleCreateFile}>Create File</button>
      <p>{message}</p>
    </div>
  );
}

export default App;