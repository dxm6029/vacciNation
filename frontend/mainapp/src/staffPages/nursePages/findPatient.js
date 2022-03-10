import './patient.css';
import NavBar from './navBar';

function FindPatient() {
    // Find a patient
  return (
    <>
        <NavBar />

        <h1>Find Patient</h1> 

        <form className="patientForm">
            <label htmlFor="name">Patient's Name</label>
            <input type="text" id="name" name="name" placeholder="Name..."/>

            <label htmlFor="dob"> Patient's Date of Birth</label>
            <input type="date" id="dob" name="dob" placeholder="mm/dd/yyyy"/>
        
            <input type="submit" value="Search"/>
        </form>
    </>
  );
}

export default FindPatient;
