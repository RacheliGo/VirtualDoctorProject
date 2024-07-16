namespace textAnalysis
{
    public static class BMIresults
    {
        //הגדרת משתנים גלובלים
        //מערך שיכיל את טווחי הבמי לפי גיל
        public static double[,] bmiArr = new double[,] { { 0, 18.5, 24.9 }, { 65, 22, 26.9 }, { 75, 23, 27.9 } };
        public static decimal bmi;
        public static decimal height;
        public static decimal weight;
        public static int age;

        //BMI פונקציה שמחשבת
        public static (decimal, int) CalculationBMI(decimal Height, decimal Weight, int Age)
        {
            int RetingBMI = 0;
            height = Height / 100;
            weight = Weight;
            age = Age;
            bmi = weight / height;
            bmi /= height;
            RatingBMI(ref RetingBMI);
            return (decimal.Parse(bmi.ToString("F2")), RetingBMI);
        }

        //פונקציה שבודקת האם יש השמנת יתר
        public static void RatingBMI(ref int RetingBMI)
        {
            int index = 0;
            for (int i = bmiArr.GetLength(0) - 1; i >= 1; i--)
            {
                if (bmiArr[i, 0] < age)
                {
                    index = i;
                }
            }
            decimal highV = (decimal)bmiArr[index, 2];
            //השמנת יתר
            if (bmi > highV)
            {
                RetingBMI = 1;
                SymptomAutomat.writeToFile(56);
            }
            else
            {
                highV = (decimal)bmiArr[index, 1];
                if (bmi < highV)
                    RetingBMI = -1;
            }
            //מבוגר
            if (age > 65)
                SymptomAutomat.writeToFile(57);
        }
    }
}