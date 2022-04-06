import './navBar.css';
import { Link } from 'react-router-dom';

function NavBar() {
    // Nurse navigation
  return (
    <>
        <div className="nurseheader">
          <div className='name'>
            
          </div>
           
          <div className="nursenavBar">
              <Link to="/findPatient" className='nurselink'>Find Patient</Link>
              <Link to="/todaysAppointments" className='nurselink'>Today's Appointments</Link>
          </div>
      </div>
    </>
  );
}

export default NavBar;
