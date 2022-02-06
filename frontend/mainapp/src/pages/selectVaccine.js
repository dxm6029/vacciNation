import './selectVaccine.css';
import NavBar from './navBar';

import Info from '../media/info.png'

function SelectVaccine() {
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
          <img src={Info} alt="Information symbol"></img>
          Welcome to the Monroe County Vaccination Scheduling site. Please proceed to
schedule your [campain name] vaccination at your earliest convenience. You will
find additional information on [campaign name] from the Monroe County
Department of Public Health at https://www.monroecounty.gov/health.

      </div>

      
        <label for="vaccineType"> Select Vaccine</label>

        <select name="vaccineType" id="vaccineType">
            <option></option>
            <option value="COVID-19">COVID-19</option>
            <option value="flu">Influenza</option>
            <option value="HPV">HPV</option>
            <option value="chickenPox">Chicken Pox</option>

        </select>

    </>
  );
}

export default SelectVaccine;
