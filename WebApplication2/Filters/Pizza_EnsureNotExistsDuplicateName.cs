using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Controllers;
using WebApplication2.DataStore;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Filters
{
    public class Pizza_EnsureNotExistsDuplicateName : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);


            var pizza = context.ActionArguments["pizza"] as Pizza;
            //temporary
            List<Pizza> pizzas = new List<Pizza>();

            if(pizza != null)
            {
                foreach(var pizza1 in pizzas)
                {
                    if(pizza.name == pizza1.name)
                    {
                        context.ModelState.AddModelError("name", "This Pizza Name already exists!");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status400BadRequest,
                        };
                        context.Result = new BadRequestObjectResult(problemDetails);
                    }
                }
            }
        }

    }
}
