using OfficeOpenXml;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System;
using System.Reflection;

namespace textAnalysis
{
    internal class WordAutomat
    {
        public static string automatPath = "C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\AutomatWord.xlsx";

        public static List<Tuple<string, int, int>>[] list;
        public static void Words(string pathRoot)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(automatPath)))
            {
                //הגליון הראשון בדף העבודות של אקסל
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // תווך התאים שאותם אנחנו רוצים לקרוא
                int startRow = worksheet.Dimension.Start.Row;
                int endRow = worksheet.Dimension.End.Row;
                int startColumn = worksheet.Dimension.Start.Column;
                int endColumn = worksheet.Dimension.End.Column;

                //מציאת המצב הגדול ביותר ע"י לקיחת כל הערכים בעמודה הראשונה והוצאה מהם את המקסימום
                var columnValues = worksheet.Cells[startRow, startColumn, endRow, startColumn].Select(cell => Convert.ToInt16(cell.Value));
                int maxNumber = columnValues.Max();

                // בניית מבנה האוטומט
                // מערך שכל איבר בו הוא רשימה
                list = new List<Tuple<string, int, int>>[maxNumber + 1];
                //אתחול כל איבר במערך להיות רשימה
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = new List<Tuple<string, int, int>>();
                    list[i].Add(new Tuple<string, int, int>("", -1, 0));
                }
                // לולאה שעוברת על כל התאים בדף
                for (int row = startRow; row <= endRow; row++)
                {
                    //מצב שמצין את המיקום במערך
                    var placeValue = worksheet.Cells[row, 1].Value;
                    int.TryParse(placeValue.ToString(), out int index);
                    //הערך
                    string cellValue = (string)worksheet.Cells[row, 2].Value;
                    //מצב הבא
                    var nextS = worksheet.Cells[row, 3].Value;
                    int.TryParse(nextS.ToString(), out int next);
                    //מצב סופי?
                    var isFainel = worksheet.Cells[row, 4].Value;
                    int.TryParse(isFainel.ToString(), out int isF);
                    //הכנסה האיבר, המצב הבא והאם מצב סופי לתוך הרשימה במערך
                    list[index].Add(new Tuple<string, int, int>(cellValue, next, isF));
                }
            }
        }
        //הרצת האוטמט
        public static void runAutomat(string pathRoot)
        {
            //חילוץ טקסט מהקובץ
            string readWord = File.ReadAllText(pathRoot);
            string[] wordsArray = readWord.Split(" ");
            int moneNegative = 0; //מונה שיסכום לי את מספר השלילה

            //מצביע למיקום באוטמט של הסימפטומים
            int situationSimptom = 0;
            // הפעלת האוטמט
            foreach (string word in wordsArray)// עובר על כל הטקסט שיתקבל
            {
                int situation = 0;
                for (int i = 0; i < word.Length; i++)// עובר על כל המילה
                {
                    string thisC = word[i].ToString();
                    Tuple<string, int, int> result = SymptomAutomat.BinarySearch(list[situation], thisC);
                    //נמצא המילה
                    if(result != null) 
                    {
                        // אם הגיע למצב של שלילה לעלות את המונה
                        moneNegative = situation == 138 ? moneNegative += 1 : moneNegative;

                        situation = result.Item2;
                        if (result.Item3 == 1)
                        {
                            // זימון אוטמט הסימפטומים
                            SymptomAutomat.FindSimptom(situation, ref situationSimptom, ref moneNegative);
                        }
                    }
                }
            }
        }
    }
}