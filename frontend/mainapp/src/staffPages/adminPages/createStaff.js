import './viewLocations.css';
import NavBar from './navBar';
import axios from 'axios';
import Cookies from 'universal-cookie';

function CreateStaff() {
  const cookies = new Cookies();

  const ADD = (event) => {
      /*
       "staff_id": 0,
  "email": "string",
  "username": "string",
  "password": "string",
  "last_name": "string",
  "first_name": "string"*/
    event.preventDefault();
    let email = event.target.email.value;
    let username = event.target.user.value;
    let password = event.target.pass.value;
    let first_name = event.target.fname.value;
    let last_name = event.target.lname.value;

    return axios
      .post(`http://localhost:5002/UserStaff`, {
        "email": email,
        "username":username,
        "password": password,
        "first_name": first_name,
        "last_name": last_name
        },{
        headers: { 
            'Content-Type': 'application/json',
            'authorization': cookies.get('token')
        },
    })
      .then((response) => {
          if (response) {
              alert("User has been added!");
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
                Create Staff
            </h2>

            <form onSubmit={ADD}> 
              <label htmlFor="fname">First Name:</label>
              <input type="text" id="fname" name="fname"></input>

              <label htmlFor="lname">Last Name:</label>
              <input type="text" id="lname" name="lname"></input>

              <label htmlFor="email">Email:</label>
              <input type="text" id="email" name="email"></input>

              <label htmlFor="user">Username:</label>
              <input type="text" id="user" name="user"></input>

              <label htmlFor="pass">Password:</label>
              <input type="text" id="pass" name="pass"></input>


              <input type="submit" value="Add User"></input>
            </form>
        </div>
    </>
  );
}

export default CreateStaff;



