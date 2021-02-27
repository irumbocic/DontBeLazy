using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontBeLazy.Core;
using DontBeLazy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DontBeLazy.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskData taskData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public MyTask MyTask { get; set; }

        public IEnumerable<SelectListItem> SelectType { get; set; }
        public EditModel(ITaskData taskData, IHtmlHelper htmlHelper)
        {
            this.taskData = taskData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? taskId)
        {

            SelectType = htmlHelper.GetEnumSelectList<TaskType>();


            if (taskId.HasValue)
            {
                MyTask = taskData.GetById(taskId.Value);
            }

            else
            {
                MyTask = new MyTask();
            }

            if (MyTask == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        //OBRISI
        //On POst prima zadatak kojeg editiramo u POST FORMI, jer smo stavili da je MYTASK = BindProperty, tako da smo "uzeli" taj zadatak i dalje s njim
        // mozemo baratati u OnPost metodi, tako cu isto napraviti s "work" gumbom, koji ce mi biti on post :)
        public IActionResult OnPost()
        {
            SelectType = htmlHelper.GetEnumSelectList<TaskType>();

            if (ModelState.IsValid)
            {

                if (MyTask.Id > 0)
                {
                    MyTask = taskData.Update(MyTask);
                    TempData["Message"] = "Task updated!";


                }
                else
                {
                    MyTask = taskData.Add(MyTask);
                    TempData["Message"] = "Task saved!";

                }
                //taskData.Commit();
                return RedirectToPage("./Detail", new { taskId = MyTask.Id });
            }
            return Page();
        }
    }
}
