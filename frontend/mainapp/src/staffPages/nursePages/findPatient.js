import './patient.css';
import NavBar from './navBar';
import axios from 'axios';

function FindPatient() {
    // Find a patient

    const FIND = (event) => {
      event.preventDefault();
      console.log(event.target);
      let firstName = event.target.fname.value;
      let lastName = event.target.lname.value;
      let dob = event.target.dob.value;
      let emailAddress = event.target.email.value;
      return axios
        .get("/UserCitizen"), {
          params: {
            first_name: firstName,
            last_name: lastName,
            email: emailAddress
          }
        }
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

            <label htmlFor="dob"> Patient's Date of Birth</label>
            <input type="date" id="dob" name="dob" placeholder="mm/dd/yyyy"/>
        
            <input type="submit" value="Search"/>
        </form>
    </>
  );
}

export default FindPatient;
