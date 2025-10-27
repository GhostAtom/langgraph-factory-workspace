import React from 'react';
import TopBar from './components/TopBar';
import './App.css';

function App() {
  return (
    <div className="App">
      <TopBar />
      <div className="content">
        {/* Application Content Here */}
      </div>
    </div>
  );
}

export default App;
