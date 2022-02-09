import './login.css';
import Navigation from './navigation';
import LoginComponent from '../components/loginComponent';

function AdminLogin() {
  return (
    <>
        <Navigation 
            information = {[""]}
            links = {[ ]}
        />
        <LoginComponent
          title="Admin Login"
        />
    </>
  );
}

export default AdminLogin;
