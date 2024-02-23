using System.Text.RegularExpressions;

namespace WhoIsApp.services
{
    static class NormalizationService
    {
        public static string NormalizeStars(string text)
        {
            string[] parameters = { "*", "%", "#" };
            string pattern = $"^({string.Join("|", parameters.Select(Regex.Escape))}).*$";
            string filteredText = Regex.Replace(text, pattern, string.Empty, RegexOptions.Multiline);
            return filteredText.Trim();
        }

        public static string NormalizeNewLineDividers(string text)
        {
            // normalize newline dividers
            text = text.Replace("\r\n", "\n");
            return text;
        }

        public static string FormatDateTime(this string input)
        {
            // datetime with timezone
            if (DateTimeOffset.TryParse(input, out DateTimeOffset dto))
            {
                //check if the input string contains hours, minutes and seconds
                if (Regex.IsMatch(input, @"\d{2}:\d{2}:\d{2}"))
                {
                    return dto.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return dto.ToString("yyyy-MM-dd");
                }
            }
            // datetime without timezone
            else if (DateTime.TryParse(input, out DateTime dt))
            {
                //check if the input string contains hours, minutes and seconds
                if (Regex.IsMatch(input, @"\d{2}:\d{2}:\d{2}"))
                {
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return dt.ToString("yyyy-MM-dd");
                }
            }
            //return an empty message if the input string is not a valid datetime
            else
            {
                return string.Empty;
            }
        }
    }
}
