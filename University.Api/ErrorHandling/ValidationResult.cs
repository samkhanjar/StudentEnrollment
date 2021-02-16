using University.Common;

namespace University.Api.ErrorHandling
{
    public class ValidationResult
    {
        public ValidationResult(string code)
        {
            Code = code;
            Message = ValidationCodesMapping.GetValidationDescription(code);
        }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
