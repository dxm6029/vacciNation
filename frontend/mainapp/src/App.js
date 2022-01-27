import './App.css';
import ToggleSwitch from "./components/toggleSwitch";
import NavBar from './components/navBar';

function App() {
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
            ["/app.js", "Home"],
            ["/fakeURL.js", "Schedule Appointment"],
            ["/fakeLink.js", "FAQ"],
            ["/fakeLink.js", "Report Reaction"]

          ]}
        />

        
      </div>
    </div>
  );
}

export default App;
