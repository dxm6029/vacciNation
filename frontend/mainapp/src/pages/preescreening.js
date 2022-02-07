import './schedule.css';
import {Link} from "react-router-dom";
import NavBar from './navBar';

function Prescreening(props) {
console.log(props);
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

        <div className="schedule">
            {props.questions.map((question, index) => (
              <div>
                {question[0] == "Date" && 
                    <div>
                        <label> {question[1]}</label>
                        <input 
                            type="date"
                        />
                    </div>}
                {question[0] == "Radio" &&
                    <div>
                        <label> {question[1]} </label>
                        {question[2].map((radioChoice, ind) => (
                            <>
                                <input 
                                    type="radio"
                                    id={`${radioChoice} for ${question[1]}`}
                                    name={`${radioChoice} for ${question[1]}`}
                                    value={`${radioChoice} for ${question[1]}`}
                                />
                                <label htmlFor={`${radioChoice} for ${question[1]}`}> {radioChoice}</label>
                            </>
                        ))}
                    </div>
                }

                {question[0] == "Dropdown" &&
                    <>
                        <label>{question[1]}</label>
                        <select name={question[1]} >
                            {question[2].map((selectChoice, ind) => (
                                <option value={selectChoice}>{selectChoice}</option>
                            ))}
                        </select>
                    </>
                }
              </div>
          ))}
        </div>

        <Link to="/schedule">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default Prescreening;
