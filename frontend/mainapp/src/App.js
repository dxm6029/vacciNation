import './App.css';
import Home from './components/Home';
import Schedule from './components/schedule';
import { BrowserRouter, Link, Switch, Route } from 'react-router-dom';
import UBRoutes from './routes';
import NavBar from './components/navBar';

function App() {
  return (
    <div>
      <div>
      <NavBar 
          links = {[
            ["Home", "Home"],
            ["schedule", "Schedule Appointment"],
            ["FAQ", "FAQ"],
            ["Report", "Report Reaction"]
          ]}
        />

      </div>
        <UBRoutes/>
          
    </div>
  );
}

export default App;
