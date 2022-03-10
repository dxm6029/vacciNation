import { Link } from 'react-router-dom';
import './login.css';
import Navigation from './navigation'; 
import LoginComponent from '../components/loginComponent';

function LogIn() {
  return (
    <>
        <Navigation 
            information = {[""]}
            links = {[ ]}
        />

        <LoginComponent
          title="Login"
        />
    </>
  );
}

export default LogIn;
