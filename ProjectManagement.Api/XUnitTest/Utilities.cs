using ProjectManagement.Entities;
using ProjectManagement.Entities.Enums;
using ProjectManagement.Shared;
using System;

namespace XUnitTestProject
{
    public static class Utilities
    {
        public static void InitializeDbForTests(PMContext<Task> taskDb, PMContext<User> userDb, PMContext<Project> projectDb)
        {
            taskDb.Table.Add(new Task
            {
                ID = 001,
                AssignedToUserID = 001,
                CreatedOn = DateTime.Now,
                ProjectID = 001,
                Status = TaskStatus.New,
                Detail = "Test task"
            });
            taskDb.SaveChanges();

            userDb.Table.Add(new User
            {
                ID = 001,
                FirstName = "tetFirst",
                LastName = "testLast",
                Email = "test@gmail.com",
                Password = "Suji8050"
            });
            userDb.SaveChanges();

            projectDb.Table.Add(new Project
            {
                ID = 001,
                Name = "Test",
                CreatedOn = DateTime.Now,
                Detail = "Test Project"
            });
            projectDb.SaveChanges();
        }
    }
}