using System;
using System.Collections.Generic;
using DontBeLazy.Core;
using System.Linq;

namespace DontBeLazy.Data
{
    public class InMemoryTaskData : ITaskData // ovdje cu kreirati i podatke o zadacima i podatke za drugu tablicu koju cu koristiti za statistiku
    {
        List<MyTask> tasks;
        List<TaskStatistics> taskStatistics;

        public InMemoryTaskData()
        {
            tasks = new List<MyTask>()
            {
                new MyTask { Id = 1, Name = "Javit se u Mono", DueAt = "05.02.2021.", TaskType = TaskType.Work },
                new MyTask { Id = 2, Name = "Zavrsiti projekt - Don't be lazy", DueAt = "05.02.2021.", TaskType = TaskType.Work },
            };

            taskStatistics = new List<TaskStatistics>()
            {
                new TaskStatistics { Id = 1, StartTime = DateTime.Now.ToString(), EndTime = (DateTime.Now.AddMinutes(30)).ToString(), WorkTime = 12, TaskId = 1},
                new TaskStatistics { Id = 2, StartTime = DateTime.Now.ToString(), EndTime = (DateTime.Now.AddMinutes(30)).ToString(), WorkTime = 13, TaskId = 2}

            };
        }

        public MyTask GetById(int id)
        {
            return tasks.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<MyTask> GetTasksByName(string name = null)
        {
            return from t in tasks
                   where string.IsNullOrEmpty(name) || t.Name.StartsWith(name)
                   orderby t.Name
                   select t;
        }
        public IEnumerable<TaskStatistics> GetStatistics()
        {
            return from s in taskStatistics
                   orderby s.Id
                   select s;
        }

        public MyTask Add(MyTask newTask)
        {
            newTask.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(newTask);
            return newTask;
        }

        public TaskStatistics AddNewLine(TaskStatistics newLine, string startTime, string endTime)
        {
            newLine.Id = taskStatistics.Max(s => s.Id) + 1;
            newLine.StartTime = startTime;
            newLine.EndTime = endTime;
            newLine.WorkTime = (DateTime.Parse(newLine.EndTime) - DateTime.Parse(newLine.StartTime)).TotalMinutes;
            taskStatistics.Add(newLine);
            return newLine;
        }

        public TaskStatistics GetStatByTaskId(int id)
        {
            return taskStatistics.SingleOrDefault(s => s.Id == id);
        }


        public MyTask Update(MyTask updatedTask)
        {
            var myTask = tasks.SingleOrDefault(t => t.Id == updatedTask.Id);
            if (myTask != null)
            {
                myTask.Name = updatedTask.Name;
                myTask.DueAt = updatedTask.DueAt;
                myTask.TaskType = updatedTask.TaskType;

            }
            return myTask;
        }
        public TaskStatistics UpdateWorkTime(TaskStatistics workingTask, DateTime endTimeUpdated)
        {
            var updateWT = taskStatistics.SingleOrDefault(s => s.Id == workingTask.Id);
            if (updateWT != null)
            {
                updateWT.StartTime = workingTask.StartTime;
                updateWT.EndTime = endTimeUpdated.ToString();
                updateWT.WorkTime = (DateTime.Parse(updateWT.EndTime) - DateTime.Parse(updateWT.StartTime)).TotalMinutes;

                updateWT.TaskId = workingTask.TaskId;
            }
            return updateWT;
        }

        public IEnumerable<WorkOnTasksSummary> TasksSummary()
        {
            return taskStatistics.GroupBy(s => s.TaskId)
                                 .Select(i => new WorkOnTasksSummary()
                                 {
                                     TaskId = i.Key,
                                     Summary = i.Sum(item => item.WorkTime)
                                 }).ToList();
        }
    }

}
