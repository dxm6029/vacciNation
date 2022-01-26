import './navBar.css';
import { BrowserRouter, Link, Routes, Route } from 'react-router-dom';
import Schedule from './schedule';

function NavBar(props) {

  return (
    <div className="navBar">
        <BrowserRouter>
          {props.links.map((link, index) => (
              <Link to={link[0]} className="link">{link[1]}</Link>
          ))}

          <Routes>
            <Route path="/Schedule" element={<Schedule />} />
            
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default NavBar;


/* 
{props.links.map((link) => (
                <li> 
                    <a href={link.href}> {link.title} </a>
                </li>
            ))} */