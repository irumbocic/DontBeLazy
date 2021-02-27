using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DontBeLazy.Core
{
    public class MyTask
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string DueAt { get; set; }
        public TaskType TaskType { get; set; }
    }
}
