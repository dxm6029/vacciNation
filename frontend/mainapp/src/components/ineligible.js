import './ineligible.css';

import Alert from '../media/alertVector.png'

function Ineligible() {
  return (
    <div className="alert">
        <img src={Alert} alt="Alert symbol"></img>
      The patient is currently ineligible to receive the vaccine.
    </div>
  );
}

export default Ineligible;
