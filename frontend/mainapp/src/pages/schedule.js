import './schedule.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';
import axios from 'axios';

function Schedule() {
  var list;

  const FIND = (event) => {
    event.preventDefault();
    console.log(event.target);
    let datePick = event.target.datePick.value;

    console.log(datePick);
    console.log("Before return");

    return axios.get(`http://localhost:5002/Appointment/date/${datePick}`)
      .then((response) => {
          if (response) {
              console.log(response); 
              list = response;
          } else {
              console.log('API failed: No data received!');
              return null;
          }
      }).catch((err) => {
          console.log('*** API Call Failed ***');
          console.log(err);
          console.log(err.toString());
          return null;
      });
  };

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

            <form onSubmit={e => {FIND(e)}}>
              <label htmlFor="datePick">Pick a date:</label>
              <input type="date" id="datePick" name="datePick"/>


              <input type="submit" value="Search"/>
            </form>
            

            <table>
              <thead>
                <tr>
                  <th>Location</th>
                  <th>Distance</th>
                </tr>
              </thead>
              <tbody>
                {list && list.map((place, index) => (
                  <div>
                    <tr className="" key={`location ${index}`}>{place}</tr>
                  </div>
                ))}

              </tbody>
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
