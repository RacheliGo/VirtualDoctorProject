using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Xml;

namespace textAnalysis
{
    public class ReadText
    {
        public static void OutRoot(string listPath, string pathRoot)
        {
            string[,] kindArr = { { "שם עצם", "A" }, { "שם תואר", "B" }, { "פועל", "C" } };
            //קריאה מקובץ
            try
            {
                // קריאת כל השורות מהקובץ עם הסמפטומים לתוך מערך של מחרוזות
                string readWord = File.ReadAllText(listPath);
                string[] wordsArray = readWord.Split(new char[] { ' ', '\n', '\t' });


                foreach (string word in wordsArray) // מעבר על כל המילים
                {
                    string url = "https://www.pealim.com/he/search/?from-nav=1&q="; // קישור לאתר
                    HtmlWeb web = new HtmlWeb(); // יוצר אוביקט כדי לקבל את הקוד של האתר 
                    HtmlDocument doc = web.Load(url + word); // טוען את התוכן של האתר מהכתובת שקבלנו
                    //חיפוש התגית שיש בתוכה את המילה שורש
                    HtmlNode spanNode = doc.DocumentNode.SelectSingleNode("//span[contains(., 'שורש')]");
                    if (spanNode != null)
                    {
                        HtmlNode aNode = spanNode.NextSibling; //מביא את התגית הבאה שאחריו, בעצם מכילה את השורש
                        //A קבלת הטקסט הפנימי של תגית 
                        string inscription = aNode.InnerText.Trim();
                        inscription = inscription.Replace("-", "").Replace(" ", "");
                        //חיפוש התגית שתכיל את חלק הדיבור
                        HtmlNode spanSpeak = doc.DocumentNode.SelectSingleNode("//span[contains(., 'חלק דיבר')]");
                        string kind = "";
                        // הוצאת הטקסט
                        HtmlNode divNode = spanSpeak.ParentNode;
                        string kindText = divNode.InnerText.Trim();
                        // בדיקת איזה חלק דיבור המילה הזו
                        for (int i = 0; i < kindArr.Length / 2; i++)
                        {
                            if (kindText.Contains(kindArr[i, 0]))
                            {
                                kind = kindArr[i, 1];
                                break;
                            }
                        }
                        UseFile.WriteToFile(pathRoot, inscription + kind);
                    }
                    else
                    {
                        //חיפוש התגית שמכילה את המילה במקור שלה
                        HtmlNode ClassNode = doc.DocumentNode.SelectSingleNode("//*[@class='menukad']");
                        if (ClassNode.InnerText != "שִׁנִּיתָ")
                            UseFile.WriteToFile(pathRoot, ClassNode.InnerText.Trim()); //זימון פונקציה שכותבת לקובץ
                        else
                            UseFile.WriteToFile(pathRoot, word); //זימון פונקציה שכותבת לקובץ
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("אירעה שגיאה: " + ex.Message);
            }
        }
    }
}