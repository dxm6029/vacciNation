import './schedule.css';
import * as React from 'react';
import {Link} from "react-router-dom";
import NavBar from './navBar';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Stack from '@mui/material/Stack';
import axios from 'axios';

function Prescreening(props) {

    const diseases1 = ['Cancer','Chronic kidney disease','Chronic liver disease','Chronic lung disease','Dementia or other neurological conditions','Diabetes','Down Syndrome','Heart Conditions','HIV','Immunocompromised state (weakened immune system)'];

    const diseases2 = ['Mental health conditions','Overweight or obesity','Pregnancy','Sickle cell disease or thalassemia','Smoking, current or former','Solid organ or blood stem cell transplant','Stroke or cerebrovascular disease, which affects blood flow to the brain','Substance use','Tuberculosis'];
    

    const [dose, setDose] = React.useState('');

    const selectChange = (event) => {
        setDose(event.target.value);
    };
    console.log(props);

    const [value, setValue] = React.useState(new Date('2014-08-18T21:11:54'));

    const handleChange = (newValue) => {
        setValue(newValue);
    };

    //DATABASE CONNECTION - SUBMIT PREESCREENING - TODO
    const SUBMIT = (event) => {
        event.preventDefault();
        console.log(event.target);
        let firstName = event.target.fname.value;
        let lastName = event.target.lname.value;
        let dob = event.target.dob.value;
        let emailAddress = event.target.email.value;
        return axios
          .get("/UserCitizen"), {
            params: {
              first_name: firstName,
              last_name: lastName,
              email: emailAddress
            }
          }
          .then((response) => {
              if (response) {
                  console.log(response); 
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
    <div className="scheduler">
        <NavBar 
          information = {["Information"]}
          links = {[
            ["/home", "Home"],
            ["/notices", "Schedule Appointment"],
            ["/faq", "FAQ"],
            ["/report", "Report Reaction"]
          ]}
        />

        

        <div className="schedule">
        <Typography sx={{fontWeight: 'bold'}} variant="h4">Pre-screening Questions</Typography>
            {props.questions.map((question, index) => (
              <div>
                {question[0] === "Date" && 
                    <Box mt={4}>
                        <Typography>{question[1]}</Typography>
                        <TextField  sx={{mt: 2, ml: 3}} label="MM/DD/YYYY" variant="outlined" />
                    </Box>}
                {question[0] === "Radio" &&
                    <Box mt={4}>
                        <Typography>{question[1]}</Typography>
                        {question[1] === 'Do you have any of the following underlying health conditions?' ? (
                            <Stack direction="row" ml={3} mt={2} mb={2} spacing={10}>
                                <Box sx={{maxWidth: 320}}>
                                {diseases1.map((disease) => (
                                    <Typography>{disease}</Typography>
                                ))}
                                </Box>
                                <Box sx={{maxWidth: 350}}>
                                    {diseases2.map((disease) => (
                                        <Typography>{disease}</Typography>
                                    ))}
                                </Box>
                            </Stack>
                        
                        ) : null}
                        <FormControl sx={{ml: 3}}>
                            <RadioGroup row>
                                {question[2].map((radioChoice, ind) => (
                                    <FormControlLabel id={`${radioChoice} for ${question[1]}`} value={radioChoice} control={<Radio />} label={radioChoice} />
                                ))}
                            </RadioGroup>
                        </FormControl>
                        
                    </Box>
                }

                {question[0] === "Dropdown" &&
                    <Box mt={4}>
                        <Typography>{question[1]}</Typography>

                        <Box sx={{ minWidth: 160, mt: 3, ml: 3 }}>
                            <FormControl sx={{width: 160}}>
                                <InputLabel id="demo-simple-select-label">Doses</InputLabel>
                                <Select
                                labelId="demo-simple-select-label"
                                id="demo-simple-select"
                                value={dose}
                                label="Doses"
                                onChange={selectChange}
                                >
                                    {question[2].map((selectChoice, ind) => (
                                        <MenuItem value={selectChoice}>{selectChoice}</MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        </Box>
                    </Box>
                }
              </div>
          ))}
        </div>

        <Link to="/schedule">
            <button> Next </button>
        </Link>
    </div>
  );
}

export default Prescreening;
