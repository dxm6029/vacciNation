import './navBar.css';
import { Link } from 'react-router-dom';

function NavBar() {
    // Nurse navigation
  return (
    <>
        <div className="nurseheader">
          <div className='name'>
            NAME | Report adverse Reaction
          </div>
           
          <div className="nursenavBar">
              <Link to="/findpatient" className='nurselink'>Patient</Link>
              <Link to="/addVaccineEntry" className='nurselink'>Vaccination Input</Link>
          </div>
      </div>
    </>
  );
}

export default NavBar;
