using DontBeLazy.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DontBeLazy.Data
{
    public class SQLTaskData : ITaskData
        {
        private readonly TaskSDbContext context;

        public SQLTaskData(TaskSDbContext context)
        {
            this.context = context;
        }

        public MyTask Add(MyTask newTask)
        {
            context.MyTask.Add(newTask);
            context.SaveChanges();
            return newTask;
        }


        public TaskStatistics AddNewLine(TaskStatistics newLine, string startTime, string endTime)
        {
            newLine.StartTime = startTime;
            newLine.EndTime = endTime;
            newLine.WorkTime = (DateTime.Parse(newLine.EndTime) - DateTime.Parse(newLine.StartTime)).TotalMinutes;
            context.TaskStatistics.Add(newLine);
            context.SaveChanges();
            return newLine;
        }

        public MyTask GetById(int id)
        {
            return context.MyTask.Find(id);
        }

        public TaskStatistics GetStatByTaskId(int id)
        {
            return context.TaskStatistics.Find(id);
        }

        public IEnumerable<TaskStatistics> GetStatistics()
        {
            return from s in context.TaskStatistics
                   orderby s.Id
                   select s;
        }

        public IEnumerable<MyTask> GetTasksByName(string name)
        {
            return from t in context.MyTask
                   where string.IsNullOrEmpty(name) || t.Name.StartsWith(name)
                   orderby t.Name
                   select t;
        }

        public IEnumerable<WorkOnTasksSummary> TasksSummary()
        {
            return context.TaskStatistics.GroupBy(s => s.TaskId)
                                 .Select(i => new WorkOnTasksSummary()
                                 {
                                     TaskId = i.Key,
                                     Summary = i.Sum(item => item.WorkTime)
                                 }).ToList();
        }

        public MyTask Update(MyTask updatedTask)
        {
            var myTask = context.MyTask.Attach(updatedTask);
            myTask.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTask;
        }

        public TaskStatistics UpdateWorkTime(TaskStatistics workingTask, DateTime endTimeUpdated)
        {
            var updateWT = context.TaskStatistics.SingleOrDefault(s => s.Id == workingTask.Id);
            if (updateWT != null)
            {
                updateWT.StartTime = workingTask.StartTime;
                updateWT.EndTime = endTimeUpdated.ToString();
                updateWT.WorkTime = (DateTime.Parse(updateWT.EndTime) - DateTime.Parse(updateWT.StartTime)).TotalMinutes;

                updateWT.TaskId = workingTask.TaskId;
            }
            context.SaveChanges();
            return updateWT;
        }
    }
}
