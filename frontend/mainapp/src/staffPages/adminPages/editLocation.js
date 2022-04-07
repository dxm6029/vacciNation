import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

//This will be a page to edit a specific location

function EditLocation() {
  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Edit Location
            </h2>

            <form> 
              <label htmlFor="town">Town:</label>
              <input type="text" id="town" name="town"></input>

              <label htmlFor="siteName">Site Name:</label>
              <input type="text" id="siteName" name="siteName"></input>

              <label htmlFor="add">Address:</label>
              <input type="text" id="add" name="add"></input>

              <label htmlFor="active">Active:</label>
              <input type="text" id="active" name="active"></input>

              <label htmlFor="vaxx">Vaccine Provided:</label>
              <input type="text" id="vaxx" name="vaxx"></input>

              <input type="button" value="Add Location"></input>

              <div id="formStatus"></div>
            </form>
        </div>
    </>
  );
}

export default EditLocation;
