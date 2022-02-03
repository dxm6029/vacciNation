import './App.css';
import UBRoutes from './routes';
import NavBar from './components/navBar';
import Footer from './components/footer';

function App() {
  return (
    <div className="fullWidthHeight">
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
          questions={[
            ["Question 1", "Answer 1"],
            ["Question 2", "Answer 2"],
            ["Question 3", "Answer 3"],
            ["Question 4", "Answer 4"],
          ]}

          prescreeningQs = {[
            ["Date", "Date of Birth"],
            ["Radio", "Are you a healthcare worker?", ["Yes", "No"]],
            ["Radio", "Do you have any of the following health conditions? [blahblahblah]", ["Yes", "No"]],
            ["Dropdown", "How many doses of the vaccine have you received so far?", ["Zero", "One dose", "Two doses"]],
            ["Date", "Date of your last dose"],
            ["Radio", "Please select the brand of your COVID vaccine", ["Pfizer", "Moderna", "J&J", "Other"]]
          ]}
        />

        <Footer />
          
    </div>
  );
}

export default App;
