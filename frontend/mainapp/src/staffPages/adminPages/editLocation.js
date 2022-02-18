import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function EditLocation() {
  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Edit Location
            </h2>
        </div>
    </>
  );
}

export default EditLocation;
