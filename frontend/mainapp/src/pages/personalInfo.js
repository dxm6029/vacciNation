import './schedule.css';
import { Link, useParams, useSearchParams } from 'react-router-dom';
import NavBar from './navBar';
import axios from 'axios';
import { useState } from 'react';

function PersonalInfo(props) {
  const [fname, setFName] = useState(null);
  const [lname, setLName] = useState(null);
  const [email, setEmail] = useState(null);
  const [canSubmit, setCanSubmit] = useState(false);
  const [citizenId, setCitizenId] = useState(null);
  const [vaxId, setVaxId] = useState(null);

  const [searchParams] = useSearchParams();

  // Create a user citizen
  const CREATE = (event) => {
    event.preventDefault();
    let emailAddress = event.target.email.value;
    setEmail(event.target.email.value);
    let phone = event.target.phone.value;
    let firstname = event.target.firstName.value;
    setFName(event.target.firstName.value);
    let lastname = event.target.lastName.value;
    setLName(event.target.lastName.value);
    let dob = event.target.datePick.value;
    setVaxId(event.target.vax.value);


    return axios.post(`http://localhost:5002/UserCitizen`, {
      "email": emailAddress,
      "first_name": firstname,
      "last_name": lastname,
      "date_of_birth": dob,
      "phone_number": phone
    })
      .then((response) => {
          if (response) {
              console.log(response); 
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
      });
  };

  function nextPage() {
    if (canSubmit && getUser() != null) {
      claimAppt();

    }
    else {
      alert("Please fill out all fields and select an appointment.");
    }
  }

  function claimAppt() {
    return axios.put(`http://localhost:5002/Appointment/Signup`, {
      "timeslot_id": searchParams.get('appt'),
      "citizen_id": citizenId,
      "vaccine_type": vaxId
    })
      .then((response) => {
          if (response) {
              console.log(response); 
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
      });
  }


  function getUser() {
    return axios.get(`http://localhost:5002/UserCitizen?first_name=${fname}&last_name=${lname}&email=${email}`)
      .then((response) => {
          if (response) {
              console.log(response); 
              setCitizenId(response.data.citizen_id)
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
            <h1> Personal Information </h1>

            <form onSubmit={e => {CREATE(e)}}>
              <label htmlFor="firstName"> First Name </label>
              <input type="text" id="firstName" name="firstName" />

              <label htmlFor="lastName"> Last Name </label>
              <input type="text" id="lastName" name="lastName" />

              <label htmlFor="datePick">Date of Birth</label>
              <input type="date" id="datePick" name="datePick"/>

              <label htmlFor="street"> Address Line 1 </label>
              <input type="text" id="street" name="street" />

              <label htmlFor="town"> City </label>
              <input type="text" id="town" name="town" />

              <label htmlFor="zip"> Zipcode </label>
              <input type="text" id="zip" name="zip" />

              <label htmlFor="email"> Email </label>
              <input type="text" id="email" name="email" />

              <label htmlFor="phone"> Phone Number </label>
              <input type="text" id="phone" name="phone" />

              <label htmlFor="vax">Which vaccine are you scheduling for?:</label>
              <select name="vax" id="vax">
                <option value="1">First</option>
                <option value="2">Second</option>
                <option value="3">Third</option>
              </select>

              <input type="submit" value="Submit"/>
            </form>
        </div>

        <button onClick={() => nextPage()}> Next </button>
    </div>
  );
}

export default PersonalInfo;
