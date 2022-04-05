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
              <Link to="/addLocation" className='adminlink'>Add Location</Link>
              <Link to="/viewLocations" className='adminlink'>View Locations</Link>
              <Link to="/addTimeslots" className='adminlink'>Add Timeslots</Link>
              <Link to="/createStaff" className='adminlink'>Create Staff</Link>
          </div>
      </div>
    </>
  );
}

export default NavBar;
