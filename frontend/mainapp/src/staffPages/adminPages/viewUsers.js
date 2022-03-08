import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';
import ToggleOff from '../../components/toggleOff';
import { useState } from 'react';

function ViewUsers() {
    const [value, setValue] = useState(true);

  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                All Users
            </h2>

            <div className="buttonContainer">
                <Link to="/addUser" className='editLocation'> Add User</Link>
            </div>
            

            <table class="locationsTable">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>ID#</th>
                        <th>Role</th>
                        <th>Location</th>
                        <th>Active?</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Name</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <ToggleOff 
                                isOn={value}
                                handleToggle={() => setValue(!value)}
                            />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td></td>
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

export default ViewUsers;
