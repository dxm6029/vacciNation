import './login.css';
import Navigation from './navigation';
import LoginComponent from '../components/loginComponent';

function SiteManagerLogin() {
  return (
    <>
        <Navigation 
            information = {[""]}
            links = {[ ]}
        />
        <LoginComponent
          title="Site Manager Login"
        />
    </>
  );
}

export default SiteManagerLogin;
