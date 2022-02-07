import { Link } from 'react-router-dom';
import './login.css';
import Navigation from './navigation';

function LogIn() {
  return (
    <>
        <Navigation 
            information = {[""]}
            links = {[ ]}
        />
        <h1>Sign in as:</h1>

        <Link to="/stafflogin" className="buttonLink"> Staff Member </Link>
        <Link to="/adminlogin" className="buttonLink"> Admin </Link>
        <Link to="/sitemanagerlogin" className="buttonLink"> Site Manager </Link>
        <Link to="/nurselogin" className="buttonLink"> Nurse  </Link>
    </>
  );
}

export default LogIn;
