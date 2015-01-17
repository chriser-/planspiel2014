using System.Collections;

public enum Genders
{
    Male,
    Female
}

[System.Serializable]
public class Nutrition {

    //http://de.wikipedia.org/wiki/Guideline_Daily_Amount
    //Get data from http://nutritiondata.self.com/

    /// <summary>
    /// Kalorien in kcal
    /// </summary>
    public int Calories;
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
        /// Einfach ungesättigte Fettsäuren in Gramm
        /// </summary>
        public float MonounsaturatedFat;
        /// <summary>
        /// Mehrfach ungesättigte Fettsäuren in Gramm
        /// </summary>
        public float PolyunsaturatedFat;
        /// <summary>
        /// Trans-Fettsäuren in Gramm
        /// </summary>
        public float TransFat;
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

    public Nutrition()
    {
        Fat = new Fat_();
    }

    public static Nutrition operator +(Nutrition n1, Nutrition n2)
    {
        return new Nutrition
        {
            Carbohydrate = n1.Carbohydrate + n2.Carbohydrate,
            Calories = n1.Calories + n2.Calories,
            Fat = new Fat_
            {
                Fat = n1.Fat.Fat + n2.Fat.Fat,
                MonounsaturatedFat = n1.Fat.MonounsaturatedFat + n2.Fat.MonounsaturatedFat,
                PolyunsaturatedFat = n1.Fat.PolyunsaturatedFat + n2.Fat.PolyunsaturatedFat,
                SaturatedFat = n1.Fat.SaturatedFat + n2.Fat.SaturatedFat,
                TransFat = n1.Fat.TransFat + n2.Fat.TransFat,
            },
            Fibre = n1.Fibre + n2.Fibre,
            Protein = n1.Protein + n2.Protein,
            Sodium = n1.Sodium + n2.Sodium,
            Sugars = n1.Sugars + n2.Sugars,
        };
    }

    public override string ToString()
    {
        return
            "Kalorien: " + Calories + "kcal" +
            ", Eiweiß: " + Protein + "g" +
            ", Kohlenhydrate: " + Carbohydrate + "g" +
            ", Zucker: " + Sugars + "g" +
            ", Fett: " + Fat.Fat + "g" +
            ", Gesättigte Fettsäuren: " + Fat.SaturatedFat + "g" +
            ", Einfach ungesättigte Fettsäuren: " + Fat.MonounsaturatedFat + "g" +
            ", Mehrfach ungesättigte Fettsäuren: " + Fat.PolyunsaturatedFat + "g" +
            ", Trans-Fettsäuren: " + Fat.TransFat + "g" +
            ", Ballaststoffe: " + Fibre + "g" +
            ", Sodium: " + Sodium + "g";
    }


    //GDA for men
    private static Nutrition MenGDA = new Nutrition
    {
        Calories = 2500,
        Protein = 60f,
        Carbohydrate = 340f,
        Sugars = 110f,
        Fat = new Fat_
        {
            Fat = 80f,
            SaturatedFat = 30f,
            MonounsaturatedFat = 29f,
            PolyunsaturatedFat = 21f,
            TransFat = 0f,
        },
        Fibre = 25f,
        Sodium = 2.4f,
    };
    //GDA for Women
    private static Nutrition WomenGDA = new Nutrition
    {
        Calories = 2000,
        Protein = 50f,
        Carbohydrate = 270f,
        Sugars = 90f,
        Fat = new Fat_
        {
            Fat = 70f,
            SaturatedFat = 20f,
            MonounsaturatedFat = 34f,
            PolyunsaturatedFat = 16f,
            TransFat = 0f,
        },
        Fibre = 25f,
        Sodium = 2.4f,
    };

    public static Nutrition Recommended
    {
        get
        {
            return (Gender == Genders.Male ? MenGDA : WomenGDA);
        }
    }

    public static Genders Gender = Genders.Male;

}