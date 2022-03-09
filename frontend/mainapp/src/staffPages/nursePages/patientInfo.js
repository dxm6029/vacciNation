import './patient.css';
import NavBar from './navBar';

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
    </>
  );
}

export default PatientInfo;
