using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSE_ProjMgr.Controllers;
using FSE_ProjMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSE_ProjMgr.ProjMgrBC;
using System.Web.Http;
using System.Web.Http.Results;

namespace FSE_ProjMgr.Test
{

    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public void TestGetProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<ProjMgrDAC.Project>();
            projects.Add(new FSE_ProjMgr.ProjMgrDAC.Project()
            {
                Project_ID = 7789,
                Project_Name = "Book View Project",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            projects.Add(new FSE_ProjMgr.ProjMgrDAC.Project()
            {
                Project_ID = 77895,
                Project_Name = "Book View Project",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            context.Projects = projects;

            var controller = new ProjectController(new ProjBc());
            var result = controller.GetProjects() as JsonResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<Project>));
            //Assert.AreEqual((result.Data as List<Project>).Count, 2);
        }

        [TestMethod]
        public void TestInsertProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<FSE_ProjMgr.ProjMgrDAC.User>();
            users.Add(new FSE_ProjMgr.ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            var testProject = new FSE_ProjMgr.Models.Project()
            {
                ProjectId = 77895,
                ProjectName = "Book View Project",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(5),
                Priority = 3,
                NoOfCompletedTasks = 3,
                NoOfTasks = 5,
                User = new User()
                {
                    FirstName = "Prasun",
                    LastName = "Sarkar",
                    EmployeeId = "778956",
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.AddProject(testProject) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.IsNotNull((context.Users.Local[0]).Project_ID);
        }

        [TestMethod]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<ProjMgrDAC.Project>();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = 401638.ToString(),
                First_Name = "Subimal",
                Last_Name = "Kumar",
                Project_ID = 457,
                Task_ID = 457,
                User_ID = 457
            });
            projects.Add(new ProjMgrDAC.Project()
            {
                Project_Name = "3FOX",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 2,
                Project_ID = 457
            });
            context.Projects = projects;
            context.Users = users;
            var testProject = new Models.Project()
            {
                ProjectId = 457,
                Priority = 3,
                NoOfCompletedTasks = 2,
                NoOfTasks = 5,
                ProjectName = "ProjectTest",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(10),
                User = new User()
                {
                    EmployeeId = 401638.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = 457,
                    UserId = 457
                }
            };

            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.UpdateProject(testProject) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Projects.Local[0]).Project_Name.ToUpper(), "PROJECTTEST");
        }

        [TestMethod]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<ProjMgrDAC.Project>();
            projects.Add(new ProjMgrDAC.Project()
            {
                Project_ID = 457,
                Project_Name = "Subimal",
                Priority = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            projects.Add(new ProjMgrDAC.Project()
            {
                Project_ID = 786,
                Project_Name = "Kumar",
                Priority = 2,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(10)
            });
            context.Projects = projects;
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));

            var testProject = new Models.Project()
            {
                ProjectId = 457,
                Priority = 3,
                NoOfCompletedTasks = 2,
                NoOfTasks = 5,
                ProjectName = "ProjectTest",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(10),
                User = new User()
                {
                    EmployeeId = 401638.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = 457,
                    UserId = 457
                }
            };

            var result = controller.DeleteProject(testProject) as JsonResponse;
            Assert.IsNotNull(result);
            Assert.AreEqual(context.Projects.Local.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.AddProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -786,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = -786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.AddProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = null
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.AddProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 786,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = -786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.AddProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInsertProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 786,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = 786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.AddProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.UpdateProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -786,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Tathagata",
                    LastName = "Sen",
                    ProjectId = -786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.UpdateProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = null
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.UpdateProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 786,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = -786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.UpdateProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 786,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = 786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.UpdateProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.DeleteProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -786,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = -786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.DeleteProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = null
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.DeleteProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 786,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = -786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.DeleteProject(testProject) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                User_ID = 457,
                Task_ID = 457
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 786,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "Subimal",
                User = new User()
                {
                    EmployeeId = 457.ToString(),
                    FirstName = "Madhu",
                    LastName = "Desai",
                    ProjectId = 786,
                    UserId = 457
                }
            };
            var controller = new ProjectController(new ProjMgrBC.ProjBc(context));
            var result = controller.DeleteProject(testProject) as JsonResponse;
        }
    }
}
