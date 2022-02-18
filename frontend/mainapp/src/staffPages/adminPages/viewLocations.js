import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function ViewLocations() {
    // Admin navigation
  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Locations
            </h2>

            <div className="buttonContainer">
                <Link to="/editLocation" className='editLocation'> Edit Locations</Link>
                <Link to="/addLocation" className='editLocation'> Add Locations</Link>
            </div>
            

            <table class="locationsTable">
                <thead>
                    <tr>
                        <th>Town</th>
                        <th>Location</th>
                        <th>Address</th>
                        <th>Vaccines</th>
                        <th>Active?</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Rochester</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Webster</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Pittsford</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    
                </tbody>
            </table>
        </div>
    </>
  );
}

export default ViewLocations;
