using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSE_ProjMgr.Controllers;
using System.Collections.Generic;
using System.Web;
using FSE_ProjMgr.Models;
using System.Data.Entity;

namespace FSE_ProjMgr.Test
{
    class MockProjectManagerEntities : ProjMgrDAC.ProjectManagerEntities1
    {
        private DbSet<ProjMgrDAC.User> _users = null;
        private DbSet<ProjMgrDAC.Project> _projects = null;
        private DbSet<ProjMgrDAC.Task> _tasks = null;
        public override DbSet<ProjMgrDAC.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<ProjMgrDAC.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<ProjMgrDAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.GetAllUsers() as JsonResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<User>));
            Assert.AreEqual((result.Data as List<User>).Count, 2);
        }

        [TestMethod]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new Models.User();
            user.FirstName = "Pranatosh Kr.";
            user.LastName = "Ganguly";
            user.EmployeeId = "223344";
            user.UserId = 287;
            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.AddUser(user) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();

            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Khush";
            user.LastName = "Kundu";
            user.EmployeeId = "287";
            user.UserId = 445588;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.UpdateUser(user) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "KHUSH");
        }

        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Manas";
            user.LastName = "Kundu";
            user.EmployeeId = "416370";
            user.UserId = 445588;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.DeleteUser(user) as JsonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.DeleteUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "Subimal";

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.DeleteUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-456";

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.DeleteUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "416370";
            user.FirstName = "Manas";
            user.LastName = "Kundu";
            user.ProjectId = -1;
            user.UserId = 415379;


            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.DeleteUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.DeleteUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.UpdateUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "Subimal";

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.UpdateUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.UpdateUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.UpdateUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.UpdateUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.AddUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "Subimal";

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.AddUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.AddUser(user) as JsonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjMgrDAC.User>();
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "415379",
                First_Name = "Prasun",
                Last_Name = "Sarkar",
                Project_ID = 287,
                Task_ID = 287,
                User_ID = 401638
            });
            users.Add(new ProjMgrDAC.User()
            {
                Employee_ID = "416370",
                First_Name = "Manas",
                Last_Name = "Kundu",
                Project_ID = 4579,
                Task_ID = 4579,
                User_ID = 445588
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UsersController(new ProjMgrBC.UserBc(context));
            var result = controller.AddUser(user) as JsonResponse;
        }

    }
}

