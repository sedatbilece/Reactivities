import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import duck from './demo'

function App() {

   const [activities,setActivities] = useState([]);



   useEffect(() => {
    async function fetchData() {
      try {
        const response = await fetch('http://localhost:5000/api/activities');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setActivities(data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    }
  
    fetchData();
  }, []);
  console.log(activities);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <p><div>{duck.name}</div>
        <button onClick={()=>duck.makeSound(duck.name+" sound hear it")}>do sound</button>
        </p>

        <ul>
        {
          activities?.map((activity:any)=>(<li key={activity.id}>{activity.title}</li>))
        }
        </ul>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
