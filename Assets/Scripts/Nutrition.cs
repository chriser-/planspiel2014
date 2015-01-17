using System.Collections;

public enum Genders
{
    Male,
    Female
}

[System.Serializable]
public class Nutrition {

    //http://de.wikipedia.org/wiki/Guideline_Daily_Amount

    /// <summary>
    /// Energie in kcal
    /// </summary>
    public int Energy;
    /// <summary>
    /// Eiweiß in Gramm
    /// </summary>
    public float Protein;
    /// <summary>
    /// Kohlenhydrate in Gramm
    /// </summary>
    public float Carbohydrate;
    /// <summary>
    /// Zucker in Gramm
    /// </summary>
    public float Sugars;

    [System.Serializable]
    public class Fat_
    {
        /// <summary>
        /// Fett in Gramm
        /// </summary>
        public float Fat;
        /// <summary>
        /// Gesättigte Fettsäuren in Gramm
        /// </summary>
        public float SaturatedFat;
        /// <summary>
        /// Mehrfach ungesättigte Fettsäuren in Gramm
        /// </summary>
        public float PolyunsaturatedFat;
        /// <summary>
        /// Einfach ungesättigte Fettsäuren in Gramm
        /// </summary>
        public float MonounsaturatedFat;
    };

    public Fat_ Fat;
    /// <summary>
    /// Ballaststoffe in Gramm
    /// </summary>
    public float Fibre;
    /// <summary>
    /// Sodium (Salz*0.4) in Gramm
    /// </summary>
    public float Sodium;


    //GDA for men
    private static Nutrition MenGDA = new Nutrition
    {
        Energy = 2500,
        Protein = 60f,
        Carbohydrate = 340f,
        Sugars = 110f,
        Fat = new Fat_
        {
            Fat = 80f,
            SaturatedFat = 30f,
            MonounsaturatedFat = 29f,
            PolyunsaturatedFat = 21f,
        },
        Fibre = 25f,
        Sodium = 2.4f,
    };
    //GDA for Women
    private static Nutrition WomenGDA = new Nutrition
    {
        Energy = 2000,
        Protein = 50f,
        Carbohydrate = 270f,
        Sugars = 90f,
        Fat = new Fat_
        {
            Fat = 70f,
            SaturatedFat = 20f,
            MonounsaturatedFat = 34f,
            PolyunsaturatedFat = 16f,
        },
        Fibre = 25f,
        Sodium = 2.4f,
    };

    public static Nutrition GDA
    {
        get
        {
            return (Gender == Genders.Male ? MenGDA : WomenGDA);
        }
    }

    public static Genders Gender = Genders.Male;

}