import './patient.css';
import NavBar from './navBar';
import {Link} from 'react-router-dom';

function PatientInfo() {
  return (
    <>
        <NavBar />

        <div className="patientInfo">
            <span className="patientname"> props name </span>
            <span className="extra"> props sex | props age</span>

        </div>

        <div className='appointmentInfo'>
            <hr className='fullHR'/>

            Appointment
            <div className='row'>
                <div className='column'>
                    Date of Appointment: props date <br/>
                    Time of Appointment: props time
                </div>
                <div className='column'>
                    Vaccine Type: props type <br/>
                    Manufacturer: props manufacturer
                </div>
                <div className='column'>
                    Eventually going to be buttons
                </div>
            </div>

            <hr className='fullHR'/>
        </div>

        <div className='detailedInfo'>
            Personal Information
            <div className='PIRectangle'>
                <div className='row'>
                    <div className='column'>
                        Address <br/>
                        City/State/Zip
                    </div>
                    <div className='column'>
                        DOB <br/>
                        Phone
                    </div>
                    <div className='column'>
                        Email
                    </div>
                </div>
            </div> <br/>

            Emergency Contact
            <div className='PIRectangle'>
                <div className='row'>
                    <div className='column'>
                        Name <br/>
                        Address
                    </div>
                    <div className='column'>
                        Phone <br/>
                        Email
                    </div>
                    <div className='column'>
                        Relationship to patient
                    </div>
                </div>
            </div> <br/>

            Insurance Information
            <div className='PIRectangle'>
                <div className='row'>
                    <div className='column'>
                        Provider <br/>
                        ID#
                    </div>
                    <div className='column'>
                        Group # <br/>
                        Medicare ID
                    </div>
                    <div className='column'>
                        Uninsured State ID
                    </div>
                </div>
            </div> <br/>

            Vaccine History <Link to="/addVaccineEntry" 
                className='addVaccine'> Add a new vaccine entry</Link>
            <table className="vaccineHistory">
                <tr>
                    <th>Vaccine Type</th>
                    <th>Date </th>
                    <th>Manufacturer </th>
                    <th>Batch Number </th>
                </tr>
                <tr>
                    <td>1</td>
                    <td>2</td>
                    <td>3</td>
                    <td>4</td>
                </tr>
            </table>

            BUTTON
        </div>
    </>
  );
}

export default PatientInfo;
