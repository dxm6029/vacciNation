import './generalHeader.css';
import ToggleSwitch from './toggleSwitch'
import { BrowserRouter, Link, Routes, Route } from 'react-router-dom';
import Home from './Home';
import FakeComponent from './fakeComponent';

function GeneralHeader() {
  return (
    <div className="headerGen">
        <div className="toggle"> 
          <ToggleSwitch 
            leftLabel="English"
            rightLabel="Español" />
        </div>

        <div className="links">
          <BrowserRouter>
              <Link to='FakeComponent' className="link">Home</Link>
            <Routes>
              <Route path="/FakeComponent" element={<FakeComponent />} />
            </Routes>
          </BrowserRouter>
        </div>
    </div>
  );
}

export default GeneralHeader;
