import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';
import { useState } from 'react';

function ViewLocations() {
    // Admin navigation

    const [list, setList] = useState(null);

    const GET = (event) => {
        event.preventDefault();
        let name = event.target.siteName.value;
        let zip = event.target.zip.value;
        let street = event.target.add.value;
        let town = event.target.town.value;
    
        return axios
          .get(`http://localhost:5002/Location`, {
            headers: { 
                'Content-Type': 'application/json',
                'authorization': cookies.get('token')
            },
        })
          .then((response) => {
              if (response) {
                  console.log(response);
                  setList(response.data);
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
                Locations
            </h2>

            <div className="buttonContainer">
                <Link to="/editLocation" className='editLocation'> Edit Locations</Link>
                <Link to="/addLocation" className='editLocation'> Add Locations</Link>
            </div>
            
            <table className="locationsTable">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Town</th>
                        <th>Address</th>
                        <th>ID </th> 
                    </tr>
                </thead>
              <tbody>
                {list && 
                  list.map((place, index) => (
                    <tr 
                      className="" 
                      key={`location ${index}`} 
                      //onClick={() => selectLocation(place.location_id, place.appointment_id)}
                    >
                        <td>
                            {place.name} 
                        </td>

                        <td>
                            {place.city} 
                        </td>

                        <td>
                            {place.street}, {place.street2}
                        </td>

                        <td>
                            {place.location_id} 
                        </td>
                    </tr>
                  ))
                }
              </tbody>
            </table>

          
        </div>
    </>
  );
}

export default ViewLocations;

/*

<table id="customers">
              <thead>
                <tr>
                  <th>Location</th>
                  <th>Date and Time</th>
                </tr>
              </thead>
              <tbody>
                {list && 
                  list.map((place, index) => (
                    <tr 
                      className="" 
                      key={`location ${index}`} 
                      onClick={() => selectLocation(place.location_id, place.appointment_id)}
                    >
                      <td>
                        {place.location_name} 
                      </td>

                      <td>{place.date} </td>
                    </tr>
                  ))
                }
              </tbody>
            </table>*/