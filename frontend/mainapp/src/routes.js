import React from 'react';
import { Routes, Route, Navigate} from 'react-router-dom';

import Home from './pages/Home';
import Notices from './pages/notices';
import Schedule from './pages/schedule';
import FAQ from './pages/faq';
import Prescreening from './pages/preescreening';
import Ineligible from './pages/ineligible';
import PersonalInfo from './pages/personalInfo';
import Report from './pages/report';
import SelectVaccine from './pages/selectVaccine';
import Success from './pages/success';

import LogIn from './staffPages/login';
import ForgotPassword from './staffPages/forgotPassword';

import AdminHome from './staffPages/adminPages/home';
import ViewLocations from './staffPages/adminPages/viewLocations';
import AddLocation from './staffPages/adminPages/addLocation';
import EditLocation from './staffPages/adminPages/editLocation';
import ViewLocation from './staffPages/adminPages/viewLocation';
import ViewUsers from './staffPages/adminPages/viewUsers';
import AddUser from './staffPages/adminPages/addUser';
import AddTimeslots from './staffPages/adminPages/addTimeslots';

import NurseHome from './staffPages/nursePages/home';
import FindPatient from './staffPages/nursePages/findPatient';
import PatientInfo from './staffPages/nursePages/patientInfo';
import AddVaccineEntry from './staffPages/nursePages/addVaccineEntry';

import FrontDeskHome from './staffPages/frontdesk/home';
import FrontDeskSearch from './staffPages/frontdesk/search';

class UBRoutes extends React.Component {
    render() {
        return (
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/home" element={<Home />} />
                <Route path="/notices" element={<Notices information={this.props.information}/>}/>
                <Route path="/schedule" element={<Schedule/>}/>
                <Route path="/faq" element={<FAQ questions={this.props.questions}/>} />
                <Route path="/prescreening" element={<Prescreening questions={this.props.prescreeningQs}/>}/>
                <Route path="/ineligible" element={<Ineligible />} />
                <Route path="/personalInfo" element={<PersonalInfo/>} />
                <Route path="/report" element={<Report />} />
                <Route path="/select" element={<SelectVaccine />}/>
                <Route path="/success" element={<Success />}/>


                <Route path="/login" element={<LogIn />}/>
                <Route path="/forgotPassword" element={<ForgotPassword />}/>


                <Route path="/adminHome" element={<AdminHome />} />
                <Route path="/viewLocations" element={<ViewLocations />} />
                <Route path="/viewLocation" element={<ViewLocation />} />
                <Route path="/addLocation" element={<AddLocation />}/>
                <Route path="/editLocation" element={<EditLocation />}/>
                <Route path="/viewUsers" element={<ViewUsers />}/>
                <Route path="/addUser" element={<AddUser />}/>
                <Route path="/addTimeslots" element={<AddTimeslots />}/>


                <Route path="/nurseHome" element={<NurseHome />} />
                <Route path="/findPatient" element={<FindPatient />} />
                <Route path="/patientInfo" element={<PatientInfo />}/>
                <Route path="/addVaccineEntry" element={<AddVaccineEntry/>}/>


                <Route path="/frontdeskHome" element={<FrontDeskHome />}/>
                <Route path="/frontdeskSearch" element={<FrontDeskSearch />}/>
            </Routes>
        );
    }
}

export default UBRoutes;