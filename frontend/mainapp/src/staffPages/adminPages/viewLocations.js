import './viewLocations.css';
import { Link } from 'react-router-dom';
import NavBar from './navBar';
import { useEffect, useState } from 'react';
import axios from 'axios';
import Cookies from 'universal-cookie';

function ViewLocations() {
    // Admin navigation
    const cookies = new Cookies();
    const [list, setList] = useState(null);

    useEffect(getLocations, []);

    function getLocations() {
      return axios
        .get(`http://68.183.124.193:5002/Location`, {
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
    };


  return (
    <>
        <NavBar />

        <div className='viewLocations'>
            <h2>
                Locations
            </h2>
            
            <table className="locationsTable">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Town</th>
                        <th>Address</th>
                        <th>ID </th> 
                        <th>Edit </th>
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
                        
                        <td>
                          <button 
                            className="regularButton" 
                            onClick={() => console.log(place.location_id)}>
                               Edit 
                          </button>
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