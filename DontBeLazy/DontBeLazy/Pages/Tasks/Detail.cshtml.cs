using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DontBeLazy.Core;
using DontBeLazy.Data;

namespace DontBeLazy.Pages.Tasks
{
    public class DetailModel : PageModel
    {
        private readonly ITaskData taskData;

        [TempData]
        public string Message { get; set; }
        public MyTask MyTask { get; set; }

        public DetailModel(ITaskData taskData)
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
            return Page();


        }
    }
}
