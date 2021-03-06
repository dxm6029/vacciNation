import './navbar.css';
import { Link } from 'react-router-dom';

function FrontDeskNavBar() {
    // Front desk navbar
  return (
    <>
        <div className="nurseheader">
          <div className='name'>
            NAME | Report adverse Reaction
          </div>
           
          <div className="nursenavBar">
              <Link to="/frontdeskHome" className='frontdeskHome'>Weekly View</Link>
              <Link to="/frontdeskSearch" className='frontdeskSearch'>Check In Patient</Link>
          </div>
      </div>
    </>
  );
}

export default FrontDeskNavBar;
