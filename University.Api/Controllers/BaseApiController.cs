using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Api.ErrorHandling;
using University.Common.Exceptions;

namespace University.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected ApiResponse Success()
        {
            return new ApiResponse
            {
                ErrorCode = ApiResponseCode.Success,
            };
        }

        protected ApiResponse<T> Success<T>(T result)
        {
            return new ApiResponse<T>
            {
                ErrorCode = ApiResponseCode.Success,
                Result = result,
            };
        }

        protected FailedApiResponse Failed()
        {
            return new FailedApiResponse
            {
                ErrorCode = ApiResponseCode.Error,
            };
        }

        protected FailedApiResponse Failed(string message)
        {
            return new FailedApiResponse
            {
                ErrorCode = ApiResponseCode.Error,
                ErrorMessage = message,
            };
        }

        protected FailedApiResponse Failed(ValidationResult[] validationResult)
        {
            return new FailedApiResponse
            {
                ErrorCode = ApiResponseCode.Error,
                ValidationResult = validationResult
            };
        }

        protected FailedApiResponse Failed(int errorCode, string message)
        {
            return new FailedApiResponse
            {
                ErrorCode = (ApiResponseCode)errorCode,
                ErrorMessage = message,
            };
        }

        protected FailedApiResponse Forbidden(string message)
        {
            return new FailedApiResponse
            {
                ErrorCode = ApiResponseCode.Forbidden,
                ErrorMessage = message,
            };
        }

        protected ApiResponse ExecuteWithErrorHandling(Func<ApiResponse> action)
        {
            if (action != null)
            {
                try
                {
                    return action();
                }
                catch (ValidationException ex)
                {
                    return ProcessValidationException(ex);
                }
                catch (BusinessException ex)
                {
                    return ProcessBusinessException(ex);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return ProcessForbiddenException(ex);
                }
                catch (Exception ex)
                {
                    return ProcessException(ex);
                }
            }

            return Success();
        }

        protected async Task<ApiResponse> ExecuteWithErrorHandlingAsync(Func<ApiResponse> action)
        {
            if (action != null)
            {
                try
                {
                    return await Task.Run(action).ConfigureAwait(false);
                }
                catch (ValidationException ex)
                {
                    return ProcessValidationException(ex);
                }
                catch (BusinessException ex)
                {
                    return ProcessBusinessException(ex);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return ProcessForbiddenException(ex);
                }
                catch (Exception ex)
                {
                    return ProcessException(ex);
                }
            }

            return Success();
        }

        protected async Task<ApiResponse> ExecuteWithErrorHandlingAsync(Func<Task<ApiResponse>> action)
        {
            if (action != null)
            {
                try
                {
                    return await action().ConfigureAwait(false);
                }
                catch (ValidationException ex)
                {
                    return ProcessValidationException(ex);
                }
                catch (BusinessException ex)
                {
                    return ProcessBusinessException(ex);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return ProcessForbiddenException(ex);
                }
                catch (Exception ex)
                {
                    return ProcessException(ex);
                }
            }

            return Success();
        }

        private ApiResponse ProcessValidationException(ValidationException ex)
        {
            return Failed(ex.ErrorCodes.Select(x => new ValidationResult(x)).ToArray());
        }

        private ApiResponse ProcessBusinessException(BusinessException ex)
        {
            return Failed(ex.Message);
        }

        private ApiResponse ProcessException(Exception ex)
        {
            Log.Error(ex, "Something went wrong during request processing");

            return Failed(ex.Message);
        }

        private ApiResponse ProcessForbiddenException(UnauthorizedAccessException ex)
        {
            return Forbidden(ex.Message);
        }
    }
}