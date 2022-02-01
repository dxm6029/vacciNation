import './schedule.css';
import {Link} from "react-router-dom";

function FAQ() {

  return (
    <div className="scheduler">
        Here is a question? 
        < br/> Yes.

        <Link to="/schedule">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default FAQ;
