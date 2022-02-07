import './schedule.css';
import {Link} from "react-router-dom";
import NavBar from './navBar';

function FAQ(props) {

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

      <div className="schedule">
          {props.questions.map((quest, index) => (
            <div>
              <h2 className="" key={`question ${index}`}>{quest[0]}</h2>
              <div className="" key={`answer ${index}`}> {quest[1]} </div>
            </div>
        ))}
      </div>
    </div>
  );
}

export default FAQ;
