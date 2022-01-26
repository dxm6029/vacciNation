import './Home.css';
import ToggleSwitch from "./toggleSwitch";
import NavBar from './navBar';
import GeneralHeader from './generalHeader';
import { BrowserRouter, Link, Switch, Route } from 'react-router-dom';

function Home() {
  return (
    <div className="App">
      <div className="header">
        <div className="toggle"> 
          <ToggleSwitch 
            leftLabel="English"
            rightLabel="Español" />
        </div>

        <div className="title">
          Is it time for 
          <div className="line2"> 
            your COVID vaccine?
          </div>
        </div>

        <NavBar 
          links = {[
            ["Home", "Home"],
            ["Schedule", "Schedule Appointment"],
            ["FAQ", "FAQ"],
            ["Report", "Report Reaction"]
          ]}
        />
      </div>
    </div>
  )  
}

export default Home;
