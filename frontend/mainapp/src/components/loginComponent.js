import { Link } from 'react-router-dom';
import '../staffPages/login.css';
import { useState, useEffect } from 'react';

function LoginComponent(props) {
  const handleSubmit = async(event) => {
    console.log("logging in")
        function setFailure() {
            document.getElementById("FormStatus").innerText = "Form Submission Failed, Please Try Again";
        }

        let username = event.target.username.value;
        let password = event.target.password.value;
        event.preventDefault()
        const response = await fetch('http://192.158.1.113:5000/UserStaff/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                'username': username,
                'password': password,
            })
        })
        const responseJSON = await response.json()
        console.log("response " + responseJSON);
        if (response.status == 202) {
            console.log("login successful")
        }
        if (response.status > 200) {
            console.log("Login failure: " + response.status)
            setFailure()
            return
        }

  }

  return (
    <>
        <h1>{props.title}</h1>
    
        <form onSubmit={handleSubmit}> 
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" name="username"></input>

          <div>
              <label htmlFor="pass">Password:</label>
              <input type="password" id="pass" name="password"
                  minLength="8" required/>
          </div>
          <Link to="/forgotPassword">Forgot password?</Link>

          <input type="submit" value="Sign in"></input>

          <div id="formStatus"></div>
        </form>
        
    </>
  );
}

export default LoginComponent;
