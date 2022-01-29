import './navBar.css';
import { BrowserRouter, Link, Routes, Route } from 'react-router-dom';
import UBRoutes from '../routes';
import ToggleSwitch from './toggleSwitch';

function NavBar(props) {

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
            Is it time for 
            <div className="line2"> 
              your COVID vaccine?
            </div>
          </div>
        </div>
      </div>
      <div className="navBar">
          {props.links.map((link, index) => (
              <Link to={link[0]} className="link" key={index}>{link[1]}</Link>
          ))}
      </div>
    </div>
  );
}

export default NavBar;


/*

<BrowserRouter>
            {props.links.map((link, index) => (
                <Link to={link[0]} className="link" key={index}>{link[1]}</Link>
            ))}
        </BrowserRouter> 

        */