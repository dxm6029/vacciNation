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
              <Link to="/findpatient" className='nurselink'>Patient</Link>
          </div>
      </div>
    </>
  );
}

export default NavBar;
