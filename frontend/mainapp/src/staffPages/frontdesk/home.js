import './home.css';
import NavBar from './navbar';
import Calendar from './calendar.js';

function FrontDeskHome() {
    // Front desk home
  return (
    <>
        <NavBar />

        <h1> Appointments </h1>

        <div className="calendar">
          <Calendar />
        </div>
        
    </>
  );
}

export default FrontDeskHome;
