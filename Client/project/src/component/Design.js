import { useEffect, useState } from 'react';
import logo from './logo.png';
import './style.css'
import { Link, useParams, Params,Outlet, useLocation } from 'react-router-dom';

function Design(prop)
{
    const {numP}=prop;
    console.log(numP)
    const {pathname} = useLocation();
    const [showButton, setShowButton]= useState(false);

    useEffect(() => {
        if (pathname === '/' || pathname === '/information') {
            setShowButton(false);
        } else {
            setShowButton(true);
        }
    }, [pathname]);

    return(
        <>
            <div className="header" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <img src={logo} className="logo" alt="לוגו" />
                <div style={{margin: "auto", display: "flex"}}>
                    {/* <button className='button'><Link to="/about">אודותנו</Link></button> */}
                    <button className='top-corner-button'><Link to="/">יציאה ➡️</Link></button>
                </div>
            </div>
            <div className='line'></div>
            <div className='button-containers'>
            {showButton && <button className='custom-button'><Link to="/home">דף הבית</Link></button>}
            </div>
            <Outlet></Outlet>
        </>
    )
}
export default Design;