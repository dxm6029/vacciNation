import './schedule.css';
import { Navigate, useNavigate } from 'react-router-dom';
import NavBar from './navBar';
import axios from 'axios';
import { useState } from 'react';
import { Navigation } from '@mui/icons-material';

function Schedule() {
  const [list, setList] = useState(null);
  const [datePick, setDatePick] = useState(null);
  const [canSubmit, setCanSubmit] = useState(false);
  const [locationId, setLocationId] = useState(null);
  const [appt, setAppt] = useState(null);

  const navigate = useNavigate();

  const FIND = (event) => {
    event.preventDefault();
    setDatePick(event.target.datePick.value);

    return axios.get(`http://localhost:5002/Appointment/open/date?date=${event.target.datePick.value}`)
      .then((response) => {
          if (response) {
              console.log(response); 
              setList(response.data);
              console.log(response.data);
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

  function selectLocation(id, apptid) {
    setLocationId(id);
    setAppt(apptid);
    setCanSubmit(true);
   /* return axios.get(`http://localhost:5002/Appointment/date/open/${id}?date=${datePick}`)
      .then((response) => {
          if (response) {
              console.log(response); 
              setLocationId(response.data.location_id);
              setCanSubmit(true);
          } else {
              console.log('API failed: No data received!');
              return null;
          }
      }).catch((err) => {
          console.log('*** API Call Failed ***');
          console.log(err);
          console.log(err.toString());
          return null;
      });*/
  }

  function nextPage() {
    if (canSubmit) {
      console.log("Log in succeed");
      console.log(locationId);
      navigate({
        pathname: '/personalInfo',
        search: `?locationId=${locationId}&appt=${appt}`
      });
    }
    else {
      // pop up an alert that says no
      console.log("Log in failed");
      alert("Please fill out all fields and select an appointment.");
    }
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

            <form onSubmit={e => {FIND(e)}}>
              <label htmlFor="datePick">Pick a date:</label>
              <input type="date" id="datePick" name="datePick"/>


              <input type="submit" value="Search"/>
            </form>

            <table id="customers">
              <thead>
                <tr>
                  <th>Location</th>
                  <th>Date and Time</th>
                </tr>
              </thead>
              <tbody>
                {list && 
                  list.map((place, index) => (
                    <tr 
                      className="" 
                      key={`location ${index}`} 
                      onClick={() => selectLocation(place.location_id, place.appointment_id)}
                    >
                      <td>
                        {place.location_name} 
                      </td>

                      <td>{place.date} </td>
                    </tr>
                  ))
                }
              </tbody>
            </table>
        </div>

        <button onClick={() => nextPage()}> Next </button>
    </div>
  );
}

export default Schedule;


/*

{
  "timeslot_id": 0,
  "staff_id": 0,
  "citizen_id": 0,
  "location_id": 0,
  "dose_id": 0,
  "date": "string",
  "status": 0,
  "reactions": "string"
}

*/