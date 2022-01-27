import './toggleSwitch.css';

function ToggleSwitch(props) {
  return (
    <div className="container">
    <span className="textLabel"> {props.leftLabel} {" "} </span>
      <div className="toggle-switch">
        <input type="checkbox" className="checkbox" name={props.leftLabel} 
            id={props.leftLabel} />
            
        <label className="label" htmlFor={props.leftLabel}>
          <span className="inner" />
          <span className="switch" />
        </label>
      </div>
      <span className="textLabel">{" "} {props.rightLabel}  </span>
    </div>
  );
}

export default ToggleSwitch;
