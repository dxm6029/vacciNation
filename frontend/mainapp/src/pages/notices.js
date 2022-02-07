import './schedule.css';
import {Link} from "react-router-dom";
import NavBar from './navBar';

function Notices(props) {

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
            {props.information.map((info, index) => (
              <div>
                <h2 className="" key={`information ${index}`}>{info[0]}</h2>
                <div className="" key={`answer ${index}`}> {info[1]} </div>
              </div>
          ))}
        </div>

        <Link to="/prescreening">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default Notices;
