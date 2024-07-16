import logo from './logo.svg';
import './App.css';
import { Routes, Route, Link } from 'react-router-dom';
import Home from './component/Home';
import Information from './component/Information';
import BloodTest from './component/BloodTest';
import Simptom from './component/Simptom';
import ErrorPage from './component/ErrorPage';
import BMI from './component/BMI';
import Design from './component/Design';
import Enter from './component/Enter';
function App() {
  return (
    <>
      <Routes>
        <Route path='' element={<Design></Design>}>
          <Route path='/' element={<Enter></Enter>}></Route>
          <Route path='information' element={<Information></Information>}></Route>
          <Route path='home' element={<Home></Home>}></Route>
            <Route path='bloodTest' element={<BloodTest></BloodTest>}></Route>
            <Route path='simptom' element={<Simptom></Simptom>}></Route>
            <Route path='bmi' element={<BMI></BMI>}></Route>
            <Route path='*' element={<ErrorPage></ErrorPage>}></Route>
          </Route>
      </Routes>
    </>
  );
}

export default App;
