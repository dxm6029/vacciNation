import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function AddLocation() {
  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Add Location
            </h2>
        </div>
    </>
  );
}

export default AddLocation;
