using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textAnalysis
{
    internal class UseFile
    {
        //כתיבה לקובץ
        public static void WriteToFile(string pathRoot, string word)
        {
            string existingText = File.ReadAllText(pathRoot);//קורא את הטקסט מהקובץ 
            string updatedText = existingText.Trim() + " " + word;//מוסיף את המילה לטקסט
            File.WriteAllText(pathRoot, updatedText);//כותב בחזרה לקובץ את כל הטקסט
        }
        // ניקוי תוכן קובץ
        public static void CleanFile(string filePath)
        {
            File.WriteAllText(filePath, string.Empty);
        }
    }
}
