import './patient.css';
import NavBar from './navBar';

function AddVaccineEntry() {
    // Input new shot
  return (
    <>
        <NavBar />

        <h1> Enter a New Vaccine </h1>
        <div className='inputBox'>
            <form className="patientForm">
                <label htmlFor="vax">Vaccine Type</label>
                <input type="text" id="vax" name="vax" placeholder="Vaccine Type"/>

                <label htmlFor="dob"> Date of Shot</label>
                <input type="date" id="dob" name="dob" placeholder="mm/dd/yyyy"/>

                <label htmlFor="man">Manufacturer</label>
                <input type="text" id="man" name="man" placeholder="Manufacturer"/>

                <label htmlFor="batch"> Batch Number</label>
                <input type="text" id="batch" name="batch" placeholder="Batch Number"/>
            

                Link to cancel here
                <input type="submit" value="Submit"/>
            </form>
        </div>

    </>
  );
}

export default AddVaccineEntry;
