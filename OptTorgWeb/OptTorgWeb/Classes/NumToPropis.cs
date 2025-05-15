namespace OptTorgWeb.Classes
{
    public class NumToPropis
    {
        public static string NumberToWord(decimal number)
        {
            if (number == 0)
                return "ноль";

            string[] units = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
            string[] teens = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
                      "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
            string[] tens = { "", "", "двадцать", "тридцать", "сорок", "пятьдесят",
                     "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
            string[] hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот",
                         "шестьсот", "семьсот", "восемьсот", "девятьсот" };
            string[] thousands = { "", "тысяча", "тысячи", "тысяч" };
            string[] millions = { "", "миллион", "миллиона", "миллионов" };
            string[] billions = { "", "миллиард", "миллиарда", "миллиардов" };

            string result = "";
            long integerPart = (long)Math.Floor(number);

            if (integerPart < 0)
            {
                result += "минус ";
                integerPart = Math.Abs(integerPart);
            }

            if (integerPart >= 1000000000)
            {
                int billionsCount = (int)(integerPart / 1000000000);
                result += ConvertThreeDigitGroup(billionsCount, hundreds, tens, units) + " ";
                result += GetProperNounForm(billionsCount, billions) + " ";
                integerPart %= 1000000000;
            }

            if (integerPart >= 1000000)
            {
                int millionsCount = (int)(integerPart / 1000000);
                result += ConvertThreeDigitGroup(millionsCount, hundreds, tens, units) + " ";
                result += GetProperNounForm(millionsCount, millions) + " ";
                integerPart %= 1000000;
            }

            if (integerPart >= 1000)
            {
                int thousandsCount = (int)(integerPart / 1000);
                result += ConvertThreeDigitGroup(thousandsCount, hundreds, tens, units, true) + " ";
                result += GetProperNounForm(thousandsCount, thousands) + " ";
                integerPart %= 1000;
            }

            if (integerPart > 0)
            {
                result += ConvertThreeDigitGroup((int)integerPart, hundreds, tens, units);
            }

            // Обработка дробной части (копейки/центы)
            decimal fractionalPart = Math.Round((number - (long)number) * 100);
            if (fractionalPart > 0)
            {
                result += " и " + ConvertTwoDigitGroup((int)fractionalPart, tens, units);
            }

            return result.Trim();
        }

        private static string ConvertThreeDigitGroup(int num, string[] hundreds, string[] tens, string[] units, bool isFemale = false)
        {
            string result = "";
            if (num >= 100)
            {
                result += hundreds[num / 100] + " ";
                num %= 100;
            }

            result += ConvertTwoDigitGroup(num, tens, units, isFemale);
            return result.Trim();
        }

        private static string ConvertTwoDigitGroup(int num, string[] tens, string[] units, bool isFemale = false)
        {
            if (num == 0)
                return "";

            if (num < 10)
                return isFemale ? units[num].Replace("одина", "одна").Replace("две", "две") : units[num];

            if (num < 20)
                return tens[num - 10];

            string result = tens[num / 10];
            if (num % 10 != 0)
            {
                result += " " + (isFemale ? units[num % 10].Replace("одина", "одна").Replace("две", "две") : units[num % 10]);
            }
            return result;
        }

        private static string GetProperNounForm(int number, string[] forms)
        {
            number %= 100;
            if (number >= 11 && number <= 19)
                return forms[3];

            switch (number % 10)
            {
                case 1: return forms[1];
                case 2:
                case 3:
                case 4: return forms[2];
                default: return forms[3];
            }
        }
    }

    //public static class NumberToWordsConverter
    //{
    //    public static string ConvertDecimalToWords(decimal number, string currencyUnit = "рубль", string fractionalUnit = "копейка")
    //    {
    //        if (number == 0)
    //            return $"ноль {GetCurrencyForm(0, currencyUnit)}";

    //        long integerPart = (long)Math.Truncate(number);
    //        int fractionalPart = (int)((Math.Abs(number) - Math.Abs(integerPart)) * 100);

    //        string result = NumToPropis.NumberToWord(integerPart) + " " + GetCurrencyForm(integerPart, currencyUnit);

    //        if (fractionalPart > 0)
    //        {
    //            result += " " + NumToPropis.NumberToWord(fractionalPart) + " " + GetFractionalForm(fractionalPart, fractionalUnit);
    //        }

    //        return result.Trim();
    //    }

    //    private static string GetCurrencyForm(long number, string unit)
    //    {
    //        string[] forms = GetUnitForms(unit);
    //        return forms[GetProperFormIndex(number)];
    //    }

    //    private static string GetFractionalForm(int number, string unit)
    //    {
    //        string[] forms = GetUnitForms(unit);
    //        return forms[GetProperFormIndex(number)];
    //    }

    //    private static int GetProperFormIndex(long number)
    //    {
    //        number = Math.Abs(number) % 100;
    //        if (number > 10 && number < 20) return 2;
    //        switch (number % 10)
    //        {
    //            case 1: return 0;
    //            case 2:
    //            case 3:
    //            case 4: return 1;
    //            default: return 2;
    //        }
    //    }

    //    private static string[] GetUnitForms(string unit)
    //    {
    //        return unit switch
    //        {
    //            "рубль" => new[] { "рубль", "рубля", "рублей" },
    //            "копейка" => new[] { "копейка", "копейки", "копеек" },
    //            "доллар" => new[] { "доллар", "доллара", "долларов" },
    //            "цент" => new[] { "цент", "цента", "центов" },
    //            _ => throw new ArgumentException("Неизвестная единица измерения")
    //        };
    //    }
    //}
}
