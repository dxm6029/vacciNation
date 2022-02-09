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

      <h1> Report a Reaction </h1>

      <label for="firstname">Patient's First Name:</label>
      <input type="text" id="firstname" name="firstname" placeholder="First Name"></input>

      <label for="lastname">Patient's Last Name:</label>
      <input type="text" id="lastname" name="lastname" placeholder="Last Name"></input>

      <label for="vaccineDate">Date of Vaccine:</label>
      <input type="date" id="vaccineDate" name="vaccineDate" placeholder="MM/DD/YYYY"></input>

      <label for="reactionDate">Date of Reaction:</label>
      <input type="date" id="reactionDate" name="reactionDate" placeholder="MM/DD/YYYY"></input>

      <label for="startTime">Time of Reaction: </label>
      <input type="time" id="startTime"></input>

      <label for="vaccineType">Vaccine Received</label>
      <select name="vaccineType">
          <option> Value </option>
      </select>
      
    </div>
  );
}

export default Report;
