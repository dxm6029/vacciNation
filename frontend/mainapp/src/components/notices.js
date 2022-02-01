import './schedule.css';
import {Link} from "react-router-dom";

function Notices(props) {

  console.log(props.information);

  return (
    <div className="scheduler">

        <div className="schedule">
            {props.information.map((info, index) => (
              <div>
                <h2 className="" key={`information ${index}`}>{info[0]}</h2>
                <div className="" key={`answer ${index}`}> {info[1]} </div>
              </div>
          ))}
        </div>

        <Link to="/schedule">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default Notices;
