import { Link } from 'react-router-dom';
import '../staffPages/login.css';
import { useState, useEffect } from 'react';
import axios from 'axios';

function LoginComponent(props) {
//   const handleSubmit = async(event) => {
//     console.log("logging in")
//         function setFailure() {
//             document.getElementById("FormStatus").innerText = "Form Submission Failed, Please Try Again";
//         }

//         let username = event.target.username.value;
//         let password = event.target.password.value;
//         console.log(username);
//         console.log(password);
//         console.log(" ^ password and username ");
//         event.preventDefault()
//         const response = await fetch('http://192.168.1.5:5000/UserStaff/login', {
//             method: 'POST',
//             headers: {
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify({
//                  'username': username,
//                  'password': password,
//             })
//         })
//         const responseJSON = await response.json()
//         console.log("response " + responseJSON);
//         if (response.status == 202) {
//             console.log("login successful")
//         }
//         if (response.status > 200) {
//             console.log("Login failure: " + response.status)
            
//             return
//         }

//   }

  const LOGIN = (event) => {
    console.log(event.target);
    let username = event.target.username.value;
         let password = event.target.password.value;
    return axios
        .post("http://192.168.1.5:5000/UserStaff/login", {
          'username': username,
          'password': password,
        },
        {
          headers: {
          'Content-Type': 'application/json', 
          "Cache-Control": "no-cache, no-store, must-revalidate", 
          "Pragma": "no-cache", 
          "Expires": 0
          }
        })
        .then((response) => {
            if (response) {
                console.log(response); 
            } else {
                console.log('API failed: No data received!');
                return null;
            }
        }).catch((err) => {
            console.log('*** API Call Failed ***')
            console.log(err.toString())
            return null;
        });
}

  return (
    <>
        <h1>{props.title}</h1>
    
        <form > 
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" name="username"></input>

          <div>
              <label htmlFor="pass">Password:</label>
              <input type="password" id="pass" name="password"
                  minLength="8" required/>
          </div>
          <Link to="/forgotPassword">Forgot password?</Link>

          <button type="button" value="Sign in" onClick={LOGIN}></button>

          <div id="formStatus"></div>
        </form>
        
    </>
  );
}

export default LoginComponent;