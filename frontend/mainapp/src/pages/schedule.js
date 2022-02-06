import './schedule.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function Schedule() {

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
            Schedule <br/>
            Here is information.
        </div>

        <Link to="/personalInfo">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default Schedule;
