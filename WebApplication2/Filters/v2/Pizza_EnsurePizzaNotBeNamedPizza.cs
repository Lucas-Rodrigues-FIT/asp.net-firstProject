using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication2.Models;

namespace WebApplication2.Filters.v2
{
    public class Pizza_EnsurePizzaNotBeNamedPizza : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var pizza = context.ActionArguments["pizza"] as Pizza;

            if(pizza != null && pizza.isNamedPizza())
            {
                context.ModelState.AddModelError("name", "The pizza can't contains 'pizza' in the name.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
