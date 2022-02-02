import './schedule.css';
import {Link} from "react-router-dom";

function FAQ(props) {

  return (
    <div className="scheduler">
      <div className="schedule">
          {props.questions.map((quest, index) => (
            <div>
              <h2 className="" key={`question ${index}`}>{quest[0]}</h2>
              <div className="" key={`answer ${index}`}> {quest[1]} </div>
            </div>
        ))}
      </div>

      <Link to="/schedule">
          <button> Next </button>
      </Link>
    </div>
  );
}

export default FAQ;
