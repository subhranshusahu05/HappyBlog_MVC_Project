using System.Text.RegularExpressions;

namespace HappyBlog.Helpers
{
    public class RemoveHtmlTagHelpers
    {
        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            // Use a regular expression to remove HTML tags
            return Regex.Replace(input, "<.*?>|&.*?;", string.Empty);
        }

    }
}
