import './patient.css';
import NavBar from './navBar';
import axios from 'axios';
import { useState } from 'react';
import Cookies from 'universal-cookie';

function FindPatient() {
    
    const cookies = new Cookies();
    // Find a patient
    const [citizenid, setCitizenid] = useState(null);

    
    getAll();

    function getAll( ) {
        return axios
        .get(`http://localhost:5002/UserCitizen/all`)
        .then((response) => {
            if (response) {
                console.log(response); 
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
                console.log(response);
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

  return (
    <>
        <NavBar />

        <h1>Find Patient</h1> 

        <form className="patientForm" onSubmit={e => {FIND(e)}}>
            <label htmlFor="fname">Patient's First Name</label>
            <input type="text" id="fname" name="fname" placeholder="First Name..."/>

            <label htmlFor="lname">Patient's Last Name</label>
            <input type="text" id="lname" name="lname" placeholder="Last Name..."/>

            <label htmlFor="email">Patient's Email</label>
            <input type="text" id="email" name="email" placeholder="Email Address"/>
        
            <input type="submit" value="Search"/>
        </form>
    </>
  );
}

export default FindPatient;
