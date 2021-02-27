using DontBeLazy.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DontBeLazy.Data
{
    public class TaskSDbContext : DbContext
    {
        public TaskSDbContext(DbContextOptions<TaskSDbContext> options) 
            : base (options)
        {
        }

        public DbSet<MyTask> MyTask { get; set; }
        public DbSet<TaskStatistics> TaskStatistics { get; set; }
    }
}
