import './search.css';
import NavBar from './navbar';
import axios from 'axios';
import { useState } from 'react';
import Cookies from 'universal-cookie';

function FrontDeskSearch() {
    const cookies = new Cookies();

    const [citizenid, setCitizenid] = useState(null);
    const [appt, setAppt] = useState(null);
    const [apptDetails, setApptDetails] = useState(null);

    // Front desk Search

    const FIND = (event) => {
      event.preventDefault();
      let firstName = event.target.fname.value;
      let lastName = event.target.lname.value;
      let emailAddress = event.target.email.value;
      return axios
        .get(`http://localhost:5002/UserCitizen?first_name=${firstName}&last_name=${lastName}&email=${emailAddress}`)
        .then((response) => {
            if (response) {
                setCitizenid(response.data.citizen_id);
                getAppt(response.data.citizen_id);
            } else {
                console.log('API failed: No data received!');
                return null;
            }
        }).catch((err) => {
            console.log('*** API Call Failed ***')
            console.log(err)
            console.log(err.toString())
            return null;
        });
    }

    function getAppt(citid) {
        return axios
        .get(`http://localhost:5002/Appointment/Citizen/${citid}`, {
            headers: { 
                'Content-Type': 'application/json',
                'authorization': cookies.get('token')
            }
        })
        .then((response) => {
            if (response) {
                setAppt(response.data);
                console.log(response.data[0].appointment_id);

                getApptDetails(response.data[0].appointment_id);
            } else {
                console.log('API failed: No data received!');
                return null;
            }
        }).catch((err) => {
            console.log('*** API Call Failed ***')
            console.log(err)
            console.log(err.toString())
            return null;
        });
    }

    function getApptDetails(id){ 
      return axios
      .get(`http://localhost:5002/Appointment/${id}`, {
          headers: { 
              'Content-Type': 'application/json',
              'authorization': cookies.get('token')
          }
      })
      .then((response) => {
          if (response) {
              console.log(response);
              alert(`That patient has an appointment on ${response.data.date} for ${response.data.description}`)
          } else {
              console.log('API failed: No data received!');
              alert("No appointment found or there was an error.");
              return null;
          }
      }).catch((err) => {
          console.log('*** API Call Failed ***')
          console.log(err)
          console.log(err.toString())
          alert("No appointment found or there was an error.");
          return null;
      });
    }


  return (
    <>
        <NavBar />
        <h1>Find Patient</h1> 

        <form className="patientForm" onSubmit={e => {FIND(e)}}>
            <label htmlFor="fname">First Name</label>
            <input type="text" id="fname" name="fname" placeholder="Name..."/>

            <label htmlFor="lname">Last Name</label>
            <input type="text" id="lname" name="lname" placeholder="Name..."/>

            <label htmlFor="email">Email</label>
            <input type="text" id="email" name="email" placeholder="Email..."/>

            <input type="submit" value="Search"/>
        </form>
    </>
  );
}

export default FrontDeskSearch;
