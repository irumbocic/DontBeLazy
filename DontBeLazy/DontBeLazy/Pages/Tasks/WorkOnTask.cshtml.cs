using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DontBeLazy.Core;
using DontBeLazy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DontBeLazy.Pages.Tasks
{
    public class WorkOnTaskModel : PageModel
    {
        private readonly ITaskData taskData;

        [BindProperty(SupportsGet =true)]
        public MyTask MyTask { get; set; }

        [BindProperty(SupportsGet =true)]
        public TaskStatistics TaskStat { get; set; }




        public WorkOnTaskModel(ITaskData taskData)
        {
            this.taskData = taskData;
        }

        
        public IActionResult OnGet(int taskId)
        {

            MyTask = taskData.GetById(taskId);
            
            if (MyTask == null)
            {
                return RedirectToPage("./NotFound");   
            }

            TaskStat = taskData.AddNewLine(TaskStat, DateTime.Now.ToString(), DateTime.Now.ToString());

            //TaskStat.StartTime = DateTime.Now.ToString();

            return Page();
        }


        
        public IActionResult OnPost(int id)
        {
            TaskStat = taskData.GetStatByTaskId(id);
            TaskStat = taskData.UpdateWorkTime(TaskStat, DateTime.Now);

            return RedirectToPage("./Statistics");

        }


    }
}
