import './schedule.css';
import NavBar from './navBar';

function Success() {
  return (
    <div className="scheduler">
        <NavBar 
          information = {["Information"]}
          links = {[
            ["/home", "Home"],
            ["/notices", "Schedule Appointment"],
            ["/faq", "FAQ"],
            ["/report", "Report Reaction"]
          ]}
        />

          <h1> Great! Your appointment has been scheduled. Thanks!</h1>

    </div>
  );
}

export default Success;
