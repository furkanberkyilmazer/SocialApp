using Microsoft.AspNetCore.Mvc.Filters;
using SocialApp.BusinessLayer.Abstract;
using System.Security.Claims;

namespace SocialApp.API.Helpers
{
    public class LastActiveActionFilter : IAsyncActionFilter
    {
        //last active i güncellemek için bu methodların içine yazmak yerine araya bir filter atıp bu controllerda ki herhangi methoda istek gelince tetiklicez
     
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext=await next();
            var id=int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var repository = (IUserService)resultContext.HttpContext.RequestServices.GetService(typeof(IUserService));

            // iuser dal kullanılarak herhangi bir işlem yapıldığında

            var user = await repository.GetByIdAsync(id);
            user.LastActive = DateTime.Now;
            await repository.UpdateAsync(user);
          

        }
    }
}
