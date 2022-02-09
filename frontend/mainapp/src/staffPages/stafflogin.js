import './login.css';
import Navigation from './navigation';
import LoginComponent from '../components/loginComponent';

function StaffLogin() {
  return (
    <>
      <Navigation 
        information = {[""]}
        links = {[ ]}
      />
      <LoginComponent
          title="Staff Login"
        />
    </>
  );
}

export default StaffLogin;
