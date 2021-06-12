namespace Infrastructure
{
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected ControllerBase() : base()
        {
        }

        protected Microsoft.AspNetCore.Mvc.IActionResult
            FluentResult<T>(FluentResults.Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(value: result);
            }
            else
            {
                return BadRequest(error: result.ToResult());
            }
        }

        protected Microsoft.AspNetCore.Mvc.IActionResult
            FluentResult(FluentResults.Result result)
        {
            if (result.IsSuccess)
            {
                return Ok(value: result);
            }
            else
            {
                return BadRequest(error: result);
            }
        }

        protected Microsoft.AspNetCore.Mvc.IActionResult
            ModelStateResult()
        {
            var result = new FluentResults.Result();

            foreach (var modelState in ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    result.WithError(modelError.ErrorMessage);
                }
            }

            return BadRequest(error: result);
        }
    }
}
