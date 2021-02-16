using System.Runtime.CompilerServices;

namespace University.Common
{
    public static class ValidationCodes
    {
        public static string InvalidDate => NameOfThisProperty();

        private static string NameOfThisProperty([CallerMemberName] string callerMemberName = "")
        {
            return callerMemberName;
        }
    }
}
