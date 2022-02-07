import './schedule.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function PersonalInfo() {

  return (
    <div className="scheduler">
        <NavBar 
          information = {["Information"]}
          links = {[
            ["/home", "Home"],
            ["/notices", "Schedule Appointment"],
            ["/faq", "FAQ"],
            ["/report", "Report Reaction"]
          ]}
        />

        <div className="schedule">
            
        </div>

        <Link to="/personalInfo">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default PersonalInfo;
