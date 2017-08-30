using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Components
{
    public class CustomizeViewComponent: ViewComponent
    {
         public async Task<IViewComponentResult> InvokeAsync()
        {
           

            return View();
        }
    }
}
