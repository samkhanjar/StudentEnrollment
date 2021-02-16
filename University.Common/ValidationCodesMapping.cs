using System.Collections.Generic;

namespace University.Common
{
    public static class ValidationCodesMapping
    {
        //TODO: think about localization
        private static readonly Dictionary<string, string> ErrorsDescriptions = new Dictionary<string, string>()
        {
            { ValidationCodes.InvalidDate, "Invalid date" },
        };

        public static string GetValidationDescription(string validationCode)
        {
            string description;
            if (!ErrorsDescriptions.TryGetValue(validationCode, out description))
            {
                description = "Something went wrong";
            }
            return description;
        }
    }
}
