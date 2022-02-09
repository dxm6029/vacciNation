import './schedule.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function Schedule() {

  const tableClick = () => {
    // Here's where the user will select the row in the table
  }

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
            <h1> Schedule Appointment </h1>

            <label for="zipcode">Enter Zipcode:</label>
            <input type="text" id="zipcode" name="Zipcode"></input>

            <table>
              <tr>
                <th>Location</th>
                <th>Distance</th>
              </tr>
              <tr>
                
              Loop through more things here
              </tr>
            </table>

            <>
              Calendar to go here <br/>
            </>

            <>
              Select a time
            </>
        </div>

        <Link to="/personalInfo">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default Schedule;
