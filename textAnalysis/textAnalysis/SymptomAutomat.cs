using OfficeOpenXml;
using System;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace textAnalysis
{
    internal class SymptomAutomat
    {
        public static List<Tuple<int, int, int>>[] list;
        public static void Symptoms(string simptomPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(simptomPath)))
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
                // מערך שכל איבר בו הוא רשימה
                list = new List<Tuple<int, int, int>>[maxNumber + 1];
                //אתחול כל איבר במערך להיות רשימה
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = new List<Tuple<int, int, int>>();
                    list[i].Add(new Tuple<int, int, int>(0, -1, 0));
                }
                // לולאה שעוברת על כל התאים בדף
                for (int row = startRow; row <= endRow; row++)
                {
                    //מצב שמצין את המיקום במערך
                    var placeValue = worksheet.Cells[row, 1].Value;
                    int.TryParse(placeValue.ToString(), out int index);
                    //הערך
                    var cellValue = worksheet.Cells[row, 2].Value;
                    int.TryParse(cellValue.ToString(), out int value);
                    //מצב הבא
                    var nextS = worksheet.Cells[row, 3].Value;
                    int.TryParse(nextS.ToString(), out int next);
                    //מצב סופי?
                    var isFainel = worksheet.Cells[row, 4].Value;
                    int.TryParse(isFainel.ToString(), out int isF);
                    //הכנסה האיבר והמצב הבא לתוך הרשימה במערך
                    list[index].Add(new Tuple<int, int, int>(value, next, isF));
                }
            }
        }
        //  הרצת האוטומט
        public static void FindSimptom(int situationWord, ref int situationSimptom, ref int moneNegative)
        {
            Tuple<int,int,int> result = BinarySearch(list[situationSimptom], situationWord);
            if (result != null) 
            {
                situationSimptom = result.Item2;
                if (result.Item3 == 1)// בדיקה האם הגיעה למצב סופי
                {
                    if (moneNegative % 2 == 0)// בדיקה שהשלילה מבטל לי את הסמפטום
                    {
                        writeToFile(situationSimptom);
                    }
                    situationSimptom = 0;
                    moneNegative = 0;
                }
            }     
            else
            {
                situationSimptom = 0;
                moneNegative = 0;
            }
        }
        public static void writeToFile(int value)
        {
            string pathFinalNumber = "C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\textAnalysis\\finalNumber.txt";
            using (StreamWriter writer = File.AppendText(pathFinalNumber))
            {
                writer.WriteLine(value);
            }
        }

        public static Tuple<T1,T2,T3> BinarySearch<T1,T2,T3>(List<Tuple<T1, T2, T3>> sortedList, T1 target) where T1 : IComparable<T1>
        {
            int left = 0;
            int right = sortedList.Count-1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;//חישוב האינדקס האמצעי
                // אם נמצא 
                if (sortedList[mid].Item1.CompareTo(target) == 0)
                    return sortedList[mid];
                // אם הערך גדול 
                if (sortedList[mid].Item1.CompareTo(target) < 0)
                    left = mid + 1;
                // אם הערך קטן
                else
                    right = mid - 1;
            } 
            return null; // הערך לא נמצא ברשימה
        }
    }
}