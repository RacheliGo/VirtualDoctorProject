namespace textAnalysis
{
    public class Program
    {
        public static void Main()
        {
            string pathRoot = "C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\textAnalysis\\root.txt";
            string listPath = "C:\\Users\\רחלי גורנשטיין\\Desktop\\Server\\clientText.txt";
            string pathFinalNumber = "C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\textAnalysis\\finalNumber.txt";
            string simptomPath = "C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\AutomatSimptom.xlsx";

            UseFile.CleanFile(pathRoot); // ניקוי תוכן הקובץ של השורשים
            ReadText.OutRoot(listPath, pathRoot); //זימון פונקציה להוצאת שורשים
            SymptomAutomat.Symptoms(simptomPath); //זימון הפעלת האוטומט
            UseFile.CleanFile(pathFinalNumber); //ניקוי תוכן הקובץ של המצבים הסופיים 
            WordAutomat.Words(pathRoot);
            WordAutomat.runAutomat(pathRoot);
        }
    }
}