using System.Globalization;

namespace SchoolProject.Data.Common
{
    public class GeneralLocalizableEntity
    {
        public string localize(string textar, string texten)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName.ToLower() == "ar")
            {
                return textar;
            }
            return texten;
        }
    }
}
