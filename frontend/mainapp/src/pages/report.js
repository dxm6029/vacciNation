import './schedule.css';
import {Link} from "react-router-dom";
import NavBar from './navBar';

function Report() {

  return (
    <div className="scheduler">
      <div>
      <NavBar 
          information = {["Information"]}
          links = {[
            ["/home", "Home"],
            ["/notices", "Schedule Appointment"],
            ["/faq", "FAQ"],
            ["/report", "Report Reaction"]
          ]}
        />
      </div>

      
    </div>
  );
}

export default Report;
