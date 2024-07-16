import { Link } from 'react-router-dom';
import Design from './Design';

function Enter()
{
    return(
        <>
            <h1 className='text'>מצטערים הדף אינו נמצא</h1>
            <button><Link to="/information">המשך</Link></button>
        </>
    )
}
export default Enter;