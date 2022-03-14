import './search.css';
import NavBar from './navbar';
import axios from 'axios';

function FrontDeskSearch() {
    // Front desk Search

    const FIND = (event) => {
      event.preventDefault();
      console.log(event.target);
      let name = event.target.name.value;
      let dob = event.target.dob.value;
      return axios
            // .get("/patient"), {
//        params: {
        // name: name,
//        }
     // }
        .post("http://192.168.1.5:5000/UserStaff/login", {
          'name': name,
          'dob': dob,
          // GET THESE VALUES FROM BACKEND
          // Need to change to get?
        },
        {
          headers: {
          'Content-Type': 'application/json', 
          "Cache-Control": "no-cache, no-store, must-revalidate", 
          "Pragma": "no-cache", 
          "Expires": 0
          }
        })
        .then((response) => {
            if (response) {
                console.log(response); 
                // Create a patient with the response and send it as props
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
            <label htmlFor="name">Patient's Name</label>
            <input type="text" id="name" name="name" placeholder="Name..."/>

            <label htmlFor="dob"> Patient's Date of Birth</label>
            <input type="date" id="dob" name="dob" placeholder="mm/dd/yyyy"/>

            <input type="submit" value="Search"/>
        </form>
    </>
  );
}

export default FrontDeskSearch;
