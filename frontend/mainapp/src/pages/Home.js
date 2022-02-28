import './Home.css';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import image from '../media/background-image.jpg';
import InfoIcon from '@mui/icons-material/Info';
import image2 from '../media/covidbanner2.jpg';
import {Link} from "react-router-dom";
import NavBar from './navBar';
import { useState, useEffect } from 'react';
import { Stack } from '@mui/material';


function Home() {
  /*const [postId, setPostId] = useState(null);
../media/bannerimage.jpg
  useEffect(() => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: 'React Hooks POST Request Example' })
    };
    fetch('https://reqres.in/api/posts', requestOptions)
        .then(response => response.json())
        .then(data => setPostId(data.id));
}, []);
*/
  return (
    <div>
      <div>
        <NavBar 
            information = {["Information"]}
            links = {[
              ["/home", "Home"],
              ["/notices", "Schedule Appointment"],
              ["/faq", "FAQ"],
              ["/report", "Report Reaction"]
            ]}
          />
      </div>
      <Container maxWidth="md">
        <Box pt={6} pb={6} pl={3} pr={3} mb={5} sx={{backgroundColor: '#004494', borderRadius: '40px'}}>
          <Stack direction="row">
            <Box pt={2} mr={3}>
              <InfoIcon sx={{color: 'white', fontSize: 60}} />
            </Box>
            
            <Typography sx={{color: 'white'}}>
              Welcome to the Monroe County Vaccination Scheduling site. Please proceed to
              schedule your COVID-19 vaccination at your earliest convenience. You will
              find additional information on COVID-19 from the Monroe County
              Department of Public Health at <Link style={{color: 'white'}} to={{pathname: 'https://www.monroecounty.gov/health'}}>https://www.monroecounty.gov/health.</Link>
            </Typography>
          </Stack>
          
        </Box>
      
      <Box sx={{width: '100%', display: 'flex', justifyContent: 'center'}} className="homepageImage">
        <img src={image2} alt="img"/>
        <Box>
        <Link to="/notices">
              <Button sx={{textTransform: 'none', backgroundColor: 'white'}}> Schedule Appointment </Button>
        </Link>
        </Box>
        
      </Box>
      </Container>
      
      
    </div>
  )  
}

export default Home;
