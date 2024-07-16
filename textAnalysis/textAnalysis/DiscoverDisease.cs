using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace textAnalysis
{
    public class DiscoverDisease
    {
        public static int[] readDisease;//קריאת הסימוטומים הכוללים עבור כל מחלה
        public static int[] simptom;//מערך סימפטומים שהתקבל מהמשתמש
        public static int maxSimptom = 0;
        public static string disease ="";
        //חיזוי מחלה
        public static string FindDisease(string filePath)
        {
            int mone = 0;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; //הגליון הראשון בדף העבודות של אקסל
                // תווך התאים שאותם אנחנו רוצים לקרוא
                int startRow = worksheet.Dimension.Start.Row;
                int endRow = worksheet.Dimension.End.Row;
                int startColumn = worksheet.Dimension.Start.Column;
                int endColumn = worksheet.Dimension.End.Column;

                orderSimptom();//זימון הפונקציה

                readDisease = new int[endRow];
                //עובר על כל המחלות וסופר כמה סימפטומים קיימים
                for (int j = startColumn; j <= endColumn; j++)
                {
                    for (int i = startRow + 1; i <= endRow; i++)
                    {
                        var diseaseS = worksheet.Cells[i, j].Value;
                        int.TryParse(diseaseS.ToString(), out int disease);
                        readDisease[i - 2] = disease;
                    }
                    mone = simptom.Intersect(readDisease).Count();//סופר את מספר המופעים של הסימפטומים בתוך מחלה כלשהי
                    //מציאת המחלה ע"י מספר התסמינים הגבוה ביותר
                    if (mone > maxSimptom)
                    {
                        maxSimptom = mone;
                        disease = (string)worksheet.Cells[1, j].Value;
                    }
                }
            }
            return disease;
        }
        //סידור הסימפטומים
        public static void orderSimptom()
        {
            string readNum = File.ReadAllText("C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\textAnalysis\\finalNumber.txt");
            string[] numArray = readNum.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //העברת הערכים למערך מסוג מספר ומיון המערך
            simptom = numArray.Select(str => int.Parse(str)).ToArray();
            Array.Sort(simptom);
        }
    }
}