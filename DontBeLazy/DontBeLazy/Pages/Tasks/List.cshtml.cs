using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontBeLazy.Core;
using DontBeLazy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DontBeLazy.Pages.Tasks
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config; // config vjezba - OBRISATI
        private readonly ITaskData taskData;

        public string Message { get; set; } // config vjezba - OBRISATI
        public IEnumerable<MyTask> MyTask { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }


        public ListModel(IConfiguration config, ITaskData taskData)
        {
            this.config = config; // config vjezba - OBRISATI
            this.taskData = taskData;
        }
        public void OnGet()
        {
            //Message = config["Message"]; // config vjezba - OBRISATI
            MyTask = taskData.GetTasksByName(SearchTerm);
        }
      
    }
}
