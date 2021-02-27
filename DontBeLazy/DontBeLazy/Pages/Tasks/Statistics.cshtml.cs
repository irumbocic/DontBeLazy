using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontBeLazy.Core;
using DontBeLazy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DontBeLazy.Pages.Tasks
{
    public class StatisticsModel : PageModel
    {
        private readonly ITaskData taskData;
        

        public IEnumerable<TaskStatistics> TaskStat { get; set; }

        public StatisticsModel(ITaskData taskData)
        {
            this.taskData = taskData;
        }

        public void OnGet()
        {
            TaskStat = taskData.GetStatistics();
        }
    }
}
