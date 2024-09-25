import React from 'react';
import './App.css'; // Import any necessary CSS
import TripList from './components/TripList'; // Import TripList component

const App: React.FC = () => {
  return (
    <div className="App">
      <header className="App-header">
      </header>
      <TripList />
    </div>
  );
};

export default App;
