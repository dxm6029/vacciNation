import './viewLocations.css';
import NavBar from './navBar';
import axios from 'axios';
import Cookies from 'universal-cookie';

function AddTimeslots() {
  const cookies = new Cookies();

  const ADD = (event) => {
    event.preventDefault();
    let id = event.target.id.value;
    let date = event.target.date.value;
    let appttime = event.target.time.value;

    return axios
      .post(`http://68.183.124.193:5002/Appointment`, {
        "location_id": parseInt(id),
        "date": (`${date} ${appttime}`),
        "status": 1},{
        headers: { 
            'Content-Type': 'application/json',
            'authorization': cookies.get('token')
        },
    })
      .then((response) => {
          if (response) {
              alert("Timeslot has been added!");
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

        <div className='viewLocations'>
            <h2>
                Add Timeslots
            </h2>

            <form onSubmit={ADD}> 
              <label htmlFor="id">Location ID:</label>
              <input type="text" id="id" name="id"></input>

              <label htmlFor="date">Date:</label>
              <input type="date" id="date" name="date"></input>

              <label for="time">Appointment Time: </label>
              <input id="time" type="time" name="time"></input>


              <input type="submit" value="Add Timeslot"></input>
            </form>
        </div>
    </>
  );
}

export default AddTimeslots;
