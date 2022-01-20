import './App.css';
import ToggleSwitch from "./components/toggleSwitch";

function App() {
  return (
    <div className="App">
      <div className="header">
        <div className="toggle"> 
          <ToggleSwitch 
            leftLabel="English"
            rightLabel="Español" />
        </div>
        
      </div>
    </div>
  );
}

export default App;
