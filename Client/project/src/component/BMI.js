import Design from "./Design";
import { useEffect, useState } from 'react';

function BMI()
{
  const height= sessionStorage.getItem('height');
  const weight= sessionStorage.getItem('weight');
  const age= sessionStorage.getItem('age');
  const[bmi, setBMI]=useState();
  const[bmiReting, setBMIReting]=useState();
  const[colorText, setColorText]= useState()
  const[tip, setTip]=useState()

  /*קריאה לשרת, ובתוכו לפונקציה לקבלת BMI.*/ 
  useEffect(() => {
    const url = 'https://localhost:44369/api/GetBMI';

    // Constructing URL with parameters
    const urlWithParams = `${url}?height=${height}&weight=${weight}&age=${age}`;

    const requestOptions = {
        method: 'POST', 
        headers: {
            'Content-Type': 'application/json' // Specify the content type as JSON
        },
      };

    fetch(urlWithParams, requestOptions)
      .then(response => {
          if (!response.ok) {
              throw new Error('Network response was not ok');
          }
          return response.json();})
      .then(data => {
            setBMI(data[0])
            setBMIReting(data[1])
        })
      .catch(error => {
          console.error('There was a problem with your fetch operation:', error);
        });
  }, []);

  useEffect(()=>{
    GetColor()
  },[bmiReting])

  //פונקציה שמגדירה את צבע הטקסט
  function GetColor()
  {
    if(bmiReting<0)
    {
      setColorText("BMI_Low")
      console.log("1")
    }
    else
    {
      if(bmiReting>0)
      {
        setColorText("BMI_High")
        console.log("2")
      }
      else
      {
        setColorText("BMI_OK")
        console.log("3")
      }
    }
  }

  return(
      <>
        <div className="text">
          <h1>BMI- מדד מסת הגוף</h1>
          <h2>:שלך BMI</h2>
        </div>
        <h2 className={colorText}>{bmi}</h2>
        <div className="info">
          {bmiReting>0&& <h2>🙁 הינך סובל מהשמנת יתר
            <br></br>
            .יש סיכון מוגבר לחלות במחלות לב, סוכרת וכלי דם
            <br></br>
            .זה יכול גם להוביל ליתר לחץ דם 
          </h2>}
          {bmiReting==0&&<h2>😀 משקלך תקין</h2>}
          {bmiReting<0&&<h2>🙁 הינך סובל מתת משקל</h2>}
        </div>
        <div className="text">
          <button  onClick={()=>setTip(bmiReting)}>המלצות וטיפים</button>
        </div>
        {tip>0&&<h2 className="more-info">
          .הגברת צריכת פירות, ירקות, דגנים מלאים וחלבונים רזים 
          <br></br>
          .הגבל צריכת מזון מעובד עתיר סוכר, שומנים לא בריאים וקלוריות ריקות
          <br></br>
          .בחר לשתות מים או משקאות לא ממותקים על פני משקאות ממותקים
          <br></br>
          .בחר בשיטות בישול כמו צלייה, אפייה או אידוי במקום טיגון
          <br></br>
          .עסוק בפעילות גופנית סדירה כדי לשרוף עודף קלוריות ולשפר את הבריאות הכללית
        </h2>}
        {tip==0&&<h2 className="more-info">
          .שמור על אורך חיים בריא
          <br></br>
          .השתדל לעשות פעילות גופנית לפחות פעם בשבוע
        </h2>}
        {tip<0&&<h2 className="more-info">
          .כלול מזון עשיר בחלבון בכל ארוחה
          <br></br>
          .הגברת צריכת מזונות עתירי רכיבים תזונתיים כגון פירות, ירקות, אגוזים, זרעים ודגנים מלאים
          <br></br>
          .התייעץ עם ספק שירותי בריאות או תזונאי לקבלת המלצות תזונתיות מותאמות אישית
        </h2>}
      </>
    )
}
export default BMI;