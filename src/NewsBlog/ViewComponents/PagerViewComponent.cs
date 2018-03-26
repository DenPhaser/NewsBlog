using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsBlog.Models.Common;

namespace NewsBlog.ViewComponents
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            int page,
            int count)
        {
            var model = new PagerViewModel()
            {
                Page = page,
                PageCount = count
            };

            return View(model);
        }
    }
}
