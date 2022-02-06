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

/**
 * All routes go here.
 * Don't forget to import the components above after adding new route.
 */
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
            </Routes>
        );
    }
}

export default UBRoutes;