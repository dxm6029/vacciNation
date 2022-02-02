import './Home.css';
import image from '../media/background-image.jpg';
import image2 from '../media/covidbanner2.jpg';
import {Link} from "react-router-dom";

function Home() {
  return (
    <div className="homepage">
      <div className="welcome">
      Welcome to the Monroe County Vaccination Scheduling site. Please proceed to
      schedule your COVID-19 vaccination at your earliest convenience. You will
      find additional information on COVID-19 from the Monroe County
      Department of Public Health at https://www.monroecounty.gov/health.


      </div>
      <div className="homepageImage">
        <img src={image} alt="image"></img>
        <Link to="/notices">
              <button> Schedule Appointment </button>
        </Link>
      </div>
      
    </div>
  )  
}

export default Home;
