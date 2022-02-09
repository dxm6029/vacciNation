import { Link } from 'react-router-dom';
import '../staffPages/login.css';

function LoginComponent(props) {
  return (
    <>
        <h1>{props.title}</h1>
        
        <label for="username">Username:</label>
        <input type="text" id="username" name="username"></input>

        <div>
            <label for="pass">Password:</label>
            <input type="password" id="pass" name="password"
                minlength="8" required/>
        </div>
        <Link to="/forgotPassword">Forgot password?</Link>

        <input type="submit" value="Sign in"></input>
        
    </>
  );
}

export default LoginComponent;
