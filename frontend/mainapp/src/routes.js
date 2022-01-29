import React from 'react';
import { Routes, Route, Navigate} from 'react-router-dom';

import Home from './components/Home';
import Schedule from './components/schedule';

/**
 * All routes go here.
 * Don't forget to import the components above after adding new route.
 */
class UBRoutes extends React.Component {
    render() {
        return (
            <Routes>
                <Route path="/home" element={<Home />} />
                <Route path="/schedule" element={<Schedule/>}/>
            </Routes>
        );
    }
}

export default UBRoutes;