using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSE_ProjMgr.Controllers;
using FSE_ProjMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSE_ProjMgr.Test
{
    [TestClass]
    public class TaskControllerTest
    {
        [TestMethod]
        public void TestRetrieveTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<ProjMgrDAC.Task>();
            var users = new TestDbSet<ProjMgrDAC.User>();
            var parentTasks = new TestDbSet<ProjMgrDAC.ParentTask>();

            parentTasks.Add(new ProjMgrDAC.ParentTask()
            {
                Parent_ID = 778956,
                Parent_Task_Name = "GSI India"

            });
            context.ParentTasks = parentTasks;
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 1
            });
            context.Users = users;
            int projectid = 7789;
            tasks.Add(new ProjMgrDAC.Task()
            {
                Task_ID = 1,
                Task_Name = "Colgate",
                Parent_ID = 457986,
                Project_ID = 1874,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0

            });
            context.Tasks = tasks;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.GetTaskByProjId(projectid) as JsonResponse;

            //Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result.Data, typeof(List<Models.Task>));
        }

        [TestMethod]
        public void TestRetrieveParentTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var parentTasks = new TestDbSet<ProjMgrDAC.ParentTask>();
            parentTasks.Add(new ProjMgrDAC.ParentTask()
            {
                Parent_ID = 77895,
                Parent_Task_Name = "Pepsodant"

            });
            parentTasks.Add(new ProjMgrDAC.ParentTask()
            {
                Parent_ID = 778956,
                Parent_Task_Name = "GSI India"

            });
            context.ParentTasks = parentTasks;

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.GetParentTasks() as JsonResponse;

            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result.Data, typeof(List<Models.TaskParent>));
            //Assert.AreEqual((result.Data as List<TaskParent>).Count, 2);
        }

        [TestMethod]
        public void TestInsertTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "654219",
                First_Name = "Debdut",
                Last_Name = "Ray",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            var task = new Models.Task()
            {

                TaskName = "Colgate",
                ParentID = 786987,
                ProjectID = 34856,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0,
                User = new User()
                {
                    FirstName = "Debdut",
                    LastName = "Ray",
                    EmployeeId = "654219",
                    UserId = 1000
                }
            };

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.InsertTask(task) as JsonResponse;


            Assert.IsNotNull(result);

            Assert.IsNotNull((context.Users.Local[0]).Task_ID);
        }

        [TestMethod]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<ProjMgrDAC.Task>();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = 401638.ToString(),
                First_Name = "Tamal",
                Last_Name = "Pal",
                Project_ID = 457,
                Task_ID = 457,
                User_ID = 457
            });
            tasks.Add(new ProjMgrDAC.Task()
            {
                Task_ID = 1,
                Task_Name = "Colgate",
                Parent_ID = 786987,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            context.Users = users;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                TaskName = "task1",
                ParentID = 786987,
                ProjectID = 34856,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Priority = 30,
                Status = 0,
                User = new User()
                {
                    FirstName = "Prasun",
                    LastName = "Sarkar",
                    EmployeeId = "778956",
                    UserId = 457
                }
            };

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.UpdateTaskDetails(testTask) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Priority, 30);
        }

        [TestMethod]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<ProjMgrDAC.Task>();

            tasks.Add(new ProjMgrDAC.Task()
            {
                Task_ID = 1,
                Task_Name = "Tsk1",
                Parent_ID = 786987,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                TaskName = "task1",
                ParentID = 786987,
                ProjectID = 34856,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            };

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.DeleteTaskDetails(testTask) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Status, 1);
        }

        [TestMethod]
        public void TestRetrieveTaskByProjectId_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<ProjMgrDAC.Task>();
            var users = new TestDbSet<ProjMgrDAC.User>();
            var parentTasks = new TestDbSet<ProjMgrDAC.ParentTask>();
            parentTasks.Add(new ProjMgrDAC.ParentTask()
            {
                Parent_ID = 77895,
                Parent_Task_Name = "Pepsodant"

            });
            context.ParentTasks = parentTasks;
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 77895,
                Project_ID = 7789
            });
            context.Users = users;
            tasks.Add(new ProjMgrDAC.Task()
            {
                Project_ID = 77895,
                Parent_ID = 77895,
                Task_ID = 77895,
                Task_Name = "Subimal",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            tasks.Add(new ProjMgrDAC.Task()
            {
                Project_ID = 457,
                Parent_ID = 457,
                Task_ID = 457,
                Task_Name = "Subimal",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            context.Tasks = tasks;

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.GetTaskByProjId(77895) as JsonResponse;

            //Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result.Data, typeof(List<Models.Task>));
            //Assert.AreEqual((result.Data as List<Models.Task>).Count, 1);
            //Assert.AreEqual((result.Data as List<Models.Task>)[0].TaskName, "Subimal");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestRetrieveTaskByProjectId_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.GetTaskByProjId(-77895) as JsonResponse;
        }





        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.InsertTask(null) as JsonResponse;
        }


        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.ParentID = -786;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.InsertTask(task) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.ProjectID = -786;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.InsertTask(task) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.TaskId = -786;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.InsertTask(task) as JsonResponse;
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.UpdateTaskDetails(null) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.ParentID = -786;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.UpdateTaskDetails(task) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.ProjectID = -221;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.UpdateTaskDetails(task) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.TaskId = -256;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.UpdateTaskDetails(task) as JsonResponse;
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.DeleteTaskDetails(null) as JsonResponse;
        }



        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.ParentID = -786;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.DeleteTaskDetails(task) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.ProjectID = -786;
            var controller = new TaskController(new ProjMgrBC.TaskBc(context));
            var result = controller.DeleteTaskDetails(task) as JsonResponse;
        }        
    }
}
