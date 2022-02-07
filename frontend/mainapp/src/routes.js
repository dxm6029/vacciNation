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

import LogIn from './staffPages/login';
import StaffLogin from './staffPages/stafflogin';
import AdminLogin from './staffPages/adminlogin';
import SiteManagerLogin from './staffPages/sitemanagerlogin';
import NurseLogin from './staffPages/nurselogin';

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
                <Route path="/personalInfo" element={<PersonalInfo />} />
                <Route path="/report" element={<Report />} />
                <Route path="/select" element={<SelectVaccine />}/>


                <Route path="/login" element={<LogIn />}/>
                <Route path="/stafflogin" element={<StaffLogin />}/>
                <Route path="/adminlogin" element={<AdminLogin />}/>
                <Route path="/sitemanagerlogin" element={<SiteManagerLogin />}/>
                <Route path="/nurselogin" element={<NurseLogin />}/>
                
            </Routes>
        );
    }
}

export default UBRoutes;