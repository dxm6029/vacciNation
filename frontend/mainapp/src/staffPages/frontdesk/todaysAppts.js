import './search.css';
import NavBar from './navbar';
import axios from 'axios';
import { useState } from 'react';
import Cookies from 'universal-cookie';

function TodaysAppts() {
    const cookies = new Cookies();
    //find all of today's patients

    const [appts, setAppts] = useState(null);

    const FIND = (event) => {
      event.preventDefault();
      let id = event.target.id.value;
      let date = event.target.date.value;
      return axios
        .get(`http://localhost:5002/Appointment/date/${id}?date=${date}`, {
            headers: { 
                'Content-Type': 'application/json',
                'authorization': cookies.get('token')
            },
        })
        .then((response) => {
            if (response) {
                setAppts(response.data);
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

        <h1>Today's Appointments</h1> 

        <form className="todaysForm" onSubmit={e => {FIND(e)}}>
          <label htmlFor="id">Location ID:</label>
          <input type="text" id="id" name="id"></input>

          <label htmlFor="date">Date:</label>
          <input type="date" id="date" name="date"></input>

          <button className="regularButton" type="submit" value="Search">Search</button>
        </form>

        <table className="appts">
            <thead>
            <tr>
                <th>Appointment ID</th>
                <th>Time</th>
                <th>Patient Name</th>
                <th>Staff Name</th> 
                <th>Dose</th>
                <th>Status</th> 
            </tr>
            </thead>
            <tbody>
            {appts && 
                appts.map((info, index) => (
                    <tr 
                    className="" 
                    key={`location ${index}`} 
                >
                    <td>
                        {info.appointment_id}
                    </td>
                    <td>
                        {info.date}
                    </td>
                    <td>
                        {info.citizen_first_name} {info.citizen_last_name}
                    </td>
                    <td>
                        {info.staff_first_name} {info.staff_last_name}
                    </td>
                    <td>
                        {info.description}
                    </td>
                    <td>
                        {info.status_desc} 
                    </td>
                </tr>
                ))
            }
            </tbody>
        </table>

        
    </>
  );
}

export default TodaysAppts;
