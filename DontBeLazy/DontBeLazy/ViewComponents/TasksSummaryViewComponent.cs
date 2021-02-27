using DontBeLazy.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontBeLazy.ViewComponents
{
    public class TasksSummaryViewComponent : ViewComponent
    {
        private readonly ITaskData taskData;

        public TasksSummaryViewComponent(ITaskData taskData)
        {
            this.taskData = taskData;
        }

        public IViewComponentResult Invoke()
        {
            var result = taskData.TasksSummary();
            return View(result);
        }
    }
}
