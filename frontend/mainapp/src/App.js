import './App.css';
import Home from './components/Home';
import Schedule from './components/schedule';
import { BrowserRouter, Link, Switch, Route } from 'react-router-dom';
import UBRoutes from './routes';
import NavBar from './components/navBar';

function App() {
  return (
    <div>
      <div>
      <NavBar 
          information = {["Information"]}
          links = {[
            ["Home", "Home"],
            ["notices", "Schedule Appointment"],
            ["faq", "FAQ"],
            ["report", "Report Reaction"]
          ]}
        />

      </div>
        <UBRoutes
          information = {[
            ["Important Information", "View our privacy policy and view the HIPPA policy."],
            ["What will I need to schedule an appointment?", "In order to schedule your appointment, you will need the following information available readily:- Name- Date of birth- Address- Your most recent vaccination information (if applicable)- Insurance information or Medicare number (if applicable)"],
            ["How do I determine if I am eligible?", "Everyone over the age of 12 is eligible for a COVID-19 booster." + 
            "- Patients 12 and over who received Pfizer as their primary vaccine series may receive a booster dose 5 months after completeion of the 2nd dose" + 
            "- Patients 18 and over who previously received a primary series of Moderna vaccine are eligible for a booster dose 5 months after the 2nd dose" +
            "- Patients 18 and over who previously received the J&J vaccine are eligible for a booster dose 2 months or more after the 1st dose " +
            
           "Patients 18 and over may choose any available COVID vaccine for the booster dose." +
            
            "Pfizer Pediatric (Ages 5-11) " +
            
            "Supply of Pfizer Pediatric vaccine is limited and is only available at select locations. Pediatric doses will be given by appointment only." +
            
            "Pfizer and Moderna Additional Dose" +
            "The CDC recommends a third dose of Pfizer (5 and older) or Moderna (18 and older) COVID vaccines for people who are moderately to severely immunocompromised and have completed the initial series at least 28 days ago" +
            
            "Additional Information" +
            "COVID 19 vaccines for individuals previously unvaccinated are available in the US for all individuals age 5 and over" +
            "Only Pfizer vaccine is authorized for individuals age 5 - 17" +
            "Both Moderna and J&J vaccines are authorized for individuals age 18 and over"]
          ]}
          otherInfo = {""}
        />
          
    </div>
  );
}

export default App;
