import './login.css';
import Navigation from './navigation';
import LoginComponent from '../components/loginComponent';

function NurseLogin() {
  return (
    <>
        <Navigation 
            information = {[""]}
            links = {[ ]}
        />
        <LoginComponent
          title="Nurse Login"
        />
    </>
  );
}

export default NurseLogin;