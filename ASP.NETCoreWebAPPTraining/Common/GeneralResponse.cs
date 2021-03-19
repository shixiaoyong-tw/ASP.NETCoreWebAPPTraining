using System.Collections.Generic;
using System.Linq;

namespace ASP.NETCoreWebAPPTraining.Common
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }

        public T Result { get; set; }

        public Error Errors { get; set; }

        /// <summary>
        /// when method success
        /// </summary>
        public static GeneralResponse<T> Ok(T result)
        {
            return new GeneralResponse<T>
            {
                Success = true,
                Result = result,
                Errors = null
            };
        }

        /// <summary>
        /// when validate error
        /// </summary>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static GeneralResponse<T> ValidationError(List<KeyValuePair<string, string>> validationResults)
        {
            return new GeneralResponse<T>
            {
                Success = false,
                Result = default,
                Errors = new Error
                {
                    ErrorMessage = "user input error",
                    ValidationResults = validationResults.Select(x => new ValidationResult
                    {
                        Field = x.Key,
                        ErrorMessage = x.Value
                    }).ToList()
                }
            };
        }

        /// <summary>
        /// when method throw exception
        /// </summary>
        public static GeneralResponse<T> Exception(string errorMessage)
        {
            return new GeneralResponse<T>
            {
                Success = false,
                Result = default,
                Errors = new Error
                {
                    ErrorMessage = errorMessage
                }
            };
        }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }

        public List<ValidationResult> ValidationResults { get; set; }
    }

    public class ValidationResult
    {
        public string Field { get; set; }

        public string ErrorMessage { get; set; }
    }
}