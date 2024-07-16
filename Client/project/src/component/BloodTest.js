import { Link } from 'react-router-dom';
import React, { useEffect, useState } from 'react';
import Design from './Design';

function BloodTest() 
{
    const [fileInfo, setFileInfo] = useState('');
    const [okSend, SetOkSend] =useState(false);
    const [message, setMessage] = useState([]);
    const [goodResult, setGoodResult]= useState('')
    const [data, setData]= useState('')

    // מפה לתרגום המפתחות
    const translations = {
        Cholesterol: 'כולסטרול',
        Glucose: 'סוכר',
        Hemoglobin: 'הומגלבין',
    };

    async function sendBloodTest()
    {
        if(okSend)
            {
                const response= await fetch('http://localhost:5000/blood_test', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json',
            },
            body: JSON.stringify(fileInfo)
            });
            if (response.status === 200) {
                const data = await response.json();
                console.log(data);
                GetResult(data);
                setData(data.value)
            }
            else {
                console.error('Error sending markers to server:', response.statusText);
            }
        }
        SetOkSend(false)
    }

    useEffect(()=>{
        sendBloodTest();
    },[okSend])

    /*פונקציה שמחזירה למשתמש את הערך שאוחזר*/ 
    function GetResult(data)
    {
        setGoodResult('')
        let NewMessage=[];
        if(Object.keys(data).length !== 0)
        {
            for (const key in data) 
            {
                const value = data[key];
                const translatedKey = translations[key] || key; // תרגום המפתח אם קיים במפה
                if (value ==1) 
                    NewMessage.push(translatedKey +' '+ 'גבוה')
                else
                    NewMessage.push(translatedKey +' '+ 'נמוך')
            }
            setMessage(NewMessage)
        }  
        else
        {
            setGoodResult('אנו שמחים לבשר לך שתוצאות בדיקות הדם תקינות')
        }      
    }

    const chooseFile = (e) => {
        const file = e.target.files[0];
        if (file) {
            setFileInfo(file.name);
        } else {
            setFileInfo('');
        }
    };

    return (
        <div className='text'>
            <h1>ניתוח תוצאות בדיקות דם</h1>
            <h2>בחר את טופס בדיקות הדם שלך</h2>
            <h2>👇</h2>
            <div style={{ display: "flex", justifyContent: "center", color: "black"}}>
                <input type="file" accept=".pdf" onChange={chooseFile}/>
            </div>
            <br></br>
            <button style={{justifyContent: "center"}} onClick={()=>SetOkSend(true)}>שליחה</button>
            {   
                data!==''&
                goodResult =='' ? (
                    <div>
                        <label style={{fontSize: "22px"}}>:ניתוח תוצאות בדיקות דם הראו שהינך סובל מ</label>
                        {message.map((m, index) => (
                            <ul key={index} className='result'>
                                {m}
                            </ul>
                        ))}
                    </div>
                ) :
                <div>
                    <label style={{fontSize: "22px"}}>{goodResult}</label>
                </div>
            }
        </div>
    )
}

export default BloodTest;