import { Link } from 'react-router-dom';
import Design from './Design';

function Enter()
{
    return(
        <div className='text-more'>
            <h1>VirtualDoctorברוכים הבאים ל</h1>
            <h3>אנחנו מאחלים לך בריאות שלמה תמיד</h3>
            <button className='buttons'><Link to="/information">המשך</Link></button>
        </div>
    )
}
export default Enter;