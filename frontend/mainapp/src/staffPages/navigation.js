import './navigation.css';
import ToggleSwitch from '../components/toggleSwitch';
import { Link } from 'react-router-dom';

function Navigation(props) {
  return (
    <div>
      <div className="App">
        <div className="header">
          <div className="toggle"> 
            <ToggleSwitch 
              leftLabel="English"
              rightLabel="Español" />
          </div>

          <div className="title">
            VacciNation
          </div>
        </div>
      </div>
      <div className="navBar">
          {props.links.map((link, index) => (
            <Link replace to={link[0]} className="link" key={index}>{link[1]}</Link>
          ))}
      </div>
    </div>
  );
}

export default Navigation;

