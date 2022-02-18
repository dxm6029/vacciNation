import './navbar.css';
import { Link } from 'react-router-dom';

function NavBar() {
    // Admin navigation
  return (
    <>
        <div className="adminheader">
          <div className='name'>
            NAME
          </div>
           
          <div className="adminnavBar">
              <Link to="/" className='adminlink'>Onboarding</Link>
              <Link to="/" className='adminlink'>Management</Link>
              <Link to="/" className='adminlink'>Vaccine Priority Settings</Link>
          </div>
      </div>
    </>
  );
}

export default NavBar;
