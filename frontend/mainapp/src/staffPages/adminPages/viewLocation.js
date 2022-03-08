import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function ViewLocation() {
  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Location - locationName
            </h2>

            <div className="columns">
                <div className="column">
                    Vaccine Provided
                </div>

                <div className="column">
                    Dates available at this location
                </div>

                <div className="column">
                    Times available at this location
                </div>
            </div>
            
        </div>
    </>
  );
}

export default ViewLocation;
