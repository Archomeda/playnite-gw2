using System.IO;
using System.Text.RegularExpressions;

namespace PlayniteGw2
{
    internal static class Validation
    {
        public static bool IsValidGw2Executable(string path, bool notEmpty = true)
        {
            if (!notEmpty && string.IsNullOrEmpty(path))
                return true;

            bool valid = !string.IsNullOrEmpty(path);
            valid = valid && File.Exists(path);
            valid = valid && Path.GetExtension(path) == ".exe";
            return valid;
        }

        public static bool IsValidAccountId(string id) =>
            id != null && Regex.IsMatch(id ?? "", "^[a-zA-Z0-9]+$");
    }
}
