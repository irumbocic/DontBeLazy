using System;
using System.Collections.Generic;
using System.Text;
using DontBeLazy;
using DontBeLazy.Core;

namespace DontBeLazy.Core
{
    public class TaskStatistics
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double WorkTime { get; set; }

        public int TaskId { get; set; } 

    }
}

