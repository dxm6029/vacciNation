import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';

function AddLocation() {

  const ADD = (event) => {
    event.preventDefault();
    let name = event.target.siteName.value;
    let zip = event.target.zip.value;
    let street = event.target.add.value;
    let town = event.target.town.value;

    return axios
      .post(`http://localhost:5002/Location`, {
        "name": name,
        "zip": zip,
        "street": street,
        "city": town,
        "state": "New York",

        headers: { 
            'Content-Type': 'application/json',
            'authorization': cookies.get('token')
        },
    })
      .then((response) => {
          if (response) {
              alert("Location has been added!");
          } else {
              console.log('API failed: No data received!');
              return null;
          }
      }).catch((err) => {
          console.log('*** API Call Failed ***')
          console.log(err)
          console.log(err.toString())
          return null;
      });
  }

  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Add Location
            </h2>

            <form onSubmit={ADD}> 
              <label htmlFor="siteName">Site Name:</label>
              <input type="text" id="siteName" name="siteName"></input>

              <label htmlFor="add">Address:</label>
              <input type="text" id="add" name="add"></input>

              <label htmlFor="town">Town:</label>
              <input type="text" id="town" name="town"></input>

              <label htmlFor="zip">Zip:</label>
              <input type="text" id="zip" name="zip"></input>

              <input type="submit" value="Add Location"></input>
            </form>
        </div>
    </>
  );
}

export default AddLocation;
