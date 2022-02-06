import './ineligible.css';
import NavBar from './navBar';

import Alert from '../media/alertVector.png'

function Ineligible() {
  return (
    <>
        <NavBar 
          information = {["Information"]}
          links = {[
            ["/home", "Home"],
            ["/notices", "Schedule Appointment"],
            ["/faq", "FAQ"],
            ["/report", "Report Reaction"]
          ]}
        />

      <div className="alert">
          <img src={Alert} alt="Alert symbol"></img>
        The patient is currently ineligible to receive the vaccine.
      </div>

    </>
  );
}

export default Ineligible;
