import './navBar.css';

function NavBar(props) {
  return (
    <div className="navBar">
        <ul>
            {props.links.map((link, index) => (
                <li key={index}> 
                    <a href={link[0]} key={index}> {link[1]} </a>
                </li>
            ))}
        </ul>  
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