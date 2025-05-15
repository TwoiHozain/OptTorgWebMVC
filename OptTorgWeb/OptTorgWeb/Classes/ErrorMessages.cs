using System.ComponentModel.DataAnnotations;

namespace OptTorgWeb.Classes
{
    public class ErrorMessages
    {

        //[Required(ErrorMessage = ErrorMessages.IsRequired)]
        public const string IsRequired = "Это обязательное поле";

        //[RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyLetters)]
        public const string OnlyRuLetters = "Это поле может содержать только буквы кириллицы";
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = ErrorMessages.OnlyKirilicaLetters] 
        public const string OnlyKirilicaLetters = "Это поле может содержать только латинские буквы (A-Z, a-z)";
        
        //[RegularExpression(@"^ул\. [А-Яа-яЁёA-Za-z\s\-]+, (д\.|корп\.) [А-Яа-яЁёA-Za-z0-9]+(, (д\.|корп\.) [А-Яа-яЁёA-Za-z0-9]+)?$",ErrorMessage = ErrorMessages.WrongAdressFormat)]
        public const string WrongAdressFormat = "Адрес должен быть в формате: 'ул. [Название улицы], д. Номер' или " +
            "'ул. [Название улицы], корп. Номер' (можно оба)";

        //[Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше 0.")]
        public const string MoreThanZero = "Значение должно быть больше 0";
        //[RegularExpression(@"^\d+$", ErrorMessage = "Допустимы только цифры.")]
        public const string NumbersOnly = "Допустимы только цифры";

        //[RegularExpression(@"^\d{6}$", ErrorMessage = ErrorMessages.WrongPostIndexFormat)]
        public const string WrongPostIndexFormat = "Почтовый индекс должен состоять из 6 цифр";
        public const string WrongPhoneFormat = "Некорректный формат номера";
        public const string WrongFaxFormat = "Некорректный формат факса";

        //[RegularExpression(@"^\d{10}(\d{2})?$", ErrorMessage = "ИНН должен содержать 10 цифр (для юрлиц) или 12 цифр (для физлиц/ИП)")]

        //[MinLength(3, ErrorMessage = "Введенное значение слишком короткое. Минимальная длинна - 3 символа")]

        public static string MinLengthAttribute(int l)
        {
            return $"введенное значение слишком короткое. Минимальная длинна - {l}";
        }
    }
}
