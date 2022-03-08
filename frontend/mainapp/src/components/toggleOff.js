import './toggleOff.css';

function ToggleOff(props) {
  return (
    <>
      <input
        checked={props.isOn}
        onChange={props.handleToggle}
        className="react-switch-checkbox"
        id={`react-switch-new`}
        type="checkbox"
      />
      <label
        className="react-switch-label"
        style={{ background: props.isOn && '#53E92E' }}
        htmlFor={`react-switch-new`}
      >
        <span className={`react-switch-button`} />
      </label>
    </>
  );
}

export default ToggleOff;
