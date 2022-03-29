import { Link } from 'react-router-dom';
import '../staffPages/login.css';
import { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'universal-cookie';

function LoginComponent(props) {

const cookies = new Cookies();

const LOGIN = (event) => {
  event.preventDefault();
  console.log(event.target);
  let username = event.target.username.value;
  let password = event.target.password.value;
  return axios
      .post("http://localhost:5002/UserStaff/login", {
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
              cookies.set('token', response.data.token, { path: '/' });
              console.log(cookies.get('token')); 
              alert("You're successfully signed in!");
          } else {
              console.log('API failed: No data received!');
              alert("There has been an error logging in, please try again.");
              return null;
          }
      }).catch((err) => {
          console.log(' API Call Failed ')
          console.log(err)
          alert("There has been an error logging in, please try again.");
          console.log(err.toString())
          return null;
      });
}

return (
  <>
      <h1>{props.title}</h1>

      <form onSubmit={e => {LOGIN(e)}}>
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" name="username"></input>

          <div>
              <label htmlFor="pass">Password:</label>
              <input type="password" id="pass" name="password"
                     minLength="8" required/>
          </div>
          <Link to="/forgotPassword">Forgot password?</Link>

          <button type="submit" value="Sign in">Sign in</button>

          <div id="formStatus"></div>
      </form>

  </>
);
}
export default LoginComponent;