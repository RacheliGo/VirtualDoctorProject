import { Link, Params } from 'react-router-dom';
import Design from './Design';
function Home()
{
    const nameP = sessionStorage.getItem("firstName");
    return(
        <>
            <div className='text'>
                <h2>שלום {nameP}</h2>
                <h3>...בא תספר לנו כיצד אפשר לעזור לך </h3>
                <br></br>
                <div className='button-container' style={{height: "90px", width:"500px", margin: "auto"}}>
                    <button><Link to="/simptom">אבחון מחלה</Link></button>
                    <button><Link to="/bloodTest">ניתוח תוצאות בדיקות דם</Link></button>
                    <button><Link to={`/bmi`}>שלי BMI</Link></button>
                </div>
            </div>
            
            {/* <div className='app'></div> */}
        </>
    )
}
export default Home;