import React from 'react';
import { Routes, Route, Navigate} from 'react-router-dom';

import Home from './components/Home';
import Notices from './components/notices';
import Schedule from './components/schedule';
import FAQ from './components/faq';
import Prescreening from './components/preescreening';
import Ineligible from './components/ineligible';

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
            </Routes>
        );
    }
}

export default UBRoutes;