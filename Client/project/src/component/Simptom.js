import React, { useEffect, useState } from 'react';

function Simptom()
{
    const [isRecording, setIsRecording] = useState(false);
    const [okSend, SetOkSend] =useState(false);
    const [statusData, setStatusData] = useState('')
    const [disease, setDisease]= useState('');

    async function startRecording()
    {
        setIsRecording(true);
        try {
            const response = await fetch('http://localhost:5000/open_recording', {
                method: 'GET',
            });
        if (response.ok) {
            console.log('Recording started');
        } else {
            console.error('Error starting recording:', response.statusText);
        }} 
        catch (error) {
            console.error('Error starting recording:', error);}
    };

    async function stopRecording(){
        setIsRecording(false);
        try {
            const response = await fetch('http://localhost:5000/close_recording', {
            method: 'GET',
            });
            if (response.ok) {
                console.log('Recording stopped');
            } else {
                console.error('Error stopping recording:', response.statusText);
            }} 
        catch (error) {
            console.error('Error stopping recording:', error);
    }};

    async function SendAudio(){
        try {
            const response = await fetch('http://localhost:5000/send', {
            method: 'GET',
            });
            if (response.status==200) {
                console.log(response.status);
                setStatusData(response.status)
                console.log(statusData);
                const data = await response.json();
                console.log(data);
            } else {
                console.error('Error stopping recording:', response.statusText);
            }} 
        catch (error) {
            console.error('Error stopping recording:', error);
        }
    };

    function SendToAnalysis()
    {
        const url = 'https://localhost:44369/api/DiagnosisDisease';
        const urlWithParams = url;
        const requestOptions = {
            method: 'POST', 
            headers: {
                'Content-Type': 'application/json' // Specify the content type as JSON
            },};
        
        fetch(urlWithParams, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
                return response.json();})
            .then(data => {
                const diseaseName = data.disease;
                console.log(diseaseName);
                setDisease(diseaseName);
                console.log(disease);
            })
            .catch(error => {
              console.error('There was a problem with your fetch operation:', error);
            });
    }

    useEffect(()=>{
        if(okSend)
            {
                console.log('aaaaaaaaaaaaaaaaaaaa');
                SendAudio();
                console.log(statusData);
                        console.log('wowwww');
                        SendToAnalysis();
                        SetOkSend(false);
            }
    },[okSend])

    return (
        <div className='text'>
            <h1>אבחון מחלה</h1>
            <h2>הקלט את הסימפטומים אותם אתה חש</h2>
            <div style={{display: "flex", justifyContent: "space-around", height: "80px", width:"250px", margin: "auto"}}>
                {isRecording ? (<button onClick={stopRecording}>סיום הקלטה</button>) 
                : 
                (<button onClick={startRecording}>התחלת הקלטה</button>)}
            <button onClick={()=>SetOkSend(true)}>שליחה</button>
            </div>
            <br></br>
                {
                    disease !=='' &&( 
                        <div>
                            <label style={{fontSize: "22px"}}>:המחלה שממנה אנו חוששים שהינך סובל היא</label>
                            <h2 className='result'>{disease}</h2>
                        </div>)
                }
        </div>
  );
};

export default Simptom;