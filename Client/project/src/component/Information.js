import './style.css'
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import Design from './Design';

function Information()
{
    const navigate = useNavigate();
    const[nameP, setNameP]=useState(null)
    const[height, setHigh]=useState(null)
    const[weight, setWidth]=useState(null)
    const[age, setAge]=useState(null)
    const[kind, setKind]=useState(null)
    const[ok, setOk]= useState(false)
    //הגדרת השנה 
    const currentYear = new Date().getFullYear();
    const years = Array.from({ length: 121 }, (_, index) => currentYear - index - 1);

    // שמירת נתונים הלקוח
    function setClientData()
    {
        //מחיקת נתוני משתמש קודם
        sessionStorage.clear();
        //הכנסת נתוני משתמש
        sessionStorage.setItem('firstName', nameP);
        sessionStorage.setItem('kind', kind);
        sessionStorage.setItem('height', height);
        sessionStorage.setItem('weight', weight);
        sessionStorage.setItem('age', age);
    }

    function connect()
    {
        if(height && weight && age)
            {
                setOk(true);
                setClientData();
            }
        else
            setOk(false);
    }

    return(
        <>
        <br></br>
        <h3 className='text'>....הכנס את פרטיך האישיים כדי להמשיך</h3>
        <br></br>
        <div className={'div'}>
            <label>שם פרטי</label><br></br>
            <input type="text" onBlur={e=>setNameP(e.target.value)}></input>
            <br></br>
            <label>שם משפחה</label><br></br>
            <input type="text"></input>
            <br></br>
            <label>זכר</label>
            <input type="radio" name="kind" onChange={(e)=>setKind(e.target.value)}></input>
            <label>נקבה</label>
            <input type="radio" name="kind" onChange={(e)=>setKind(e.target.value)}>
            </input>{!kind && <label className='notHid'> ! </label>}
            <br></br>
            <label>גובה</label>{!height && <label className='notHid'> ! </label>}
            <br></br>
            <input type="text" onBlur={e=>setHigh(e.target.value)}></input><br></br>
            <label>משקל</label> {!weight && <label className='notHid'> ! </label>} 
            <br></br>
            <input type="text" onChange={e=>setWidth(e.target.value)}></input>
            <br></br>
            <label>תאריך לידה</label>{!age && <label className='notHid'> ! </label>}
            <br></br>
            <div>
                <select>
                    <option value="">יום</option> {[...Array(31).keys()].map((number) => 
                    (<option key={number} value={number + 1}>{number + 1}</option>))}
                </select> / 
                <select >
                    <option value="">חודש</option> {[...Array(12).keys()].map((number) => 
                    (<option key={number} value={number + 1}>{number + 1}</option>))}
                </select> / 
                <select  onChange={e=>setAge(currentYear-(e.target.value))}>
                    <option value="">שנה</option> {[...years].map((number) => 
                    (<option key={number} value={number + 1}>{number + 1}</option>))}
                </select>
                <br></br> 
                <button className='buttons' style={{height: "20px", color: "rgb(6, 195, 195)"}} onClick={()=>connect()}>התחברות</button>
                { ok && navigate(`/home`) }
                <br></br>
            </div>
        </div>
        </>
    )
}
export default Information;