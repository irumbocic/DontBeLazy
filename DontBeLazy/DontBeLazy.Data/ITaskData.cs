using System;
using System.Collections.Generic;
using System.Text;
using DontBeLazy.Core;

namespace DontBeLazy.Data
{
    public interface ITaskData
    {
        IEnumerable<MyTask> GetTasksByName(string name);
        MyTask GetById(int id);
        MyTask Update(MyTask updatedTask);
        MyTask Add(MyTask newTask);

        IEnumerable<TaskStatistics> GetStatistics();
        TaskStatistics AddNewLine(TaskStatistics newLine, string startTime, string endTime);
        TaskStatistics UpdateWorkTime(TaskStatistics workingTask, DateTime endTimeUpdated);

        TaskStatistics GetStatByTaskId(int id);

        IEnumerable<WorkOnTasksSummary> TasksSummary();




    }

}
