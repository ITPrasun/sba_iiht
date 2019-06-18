using FSE_ProjMgr.ActionFilters;
using FSE_ProjMgr.Models;
using FSE_ProjMgr.ProjMgrBC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FSE_ProjMgr.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        UserBc userbc = null;
        public UsersController()
        {
            userbc = new UserBc();
        }

        [HttpGet]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("user/users")]
        public JsonResponse GetAllUsers()
        {
            List<User> Users = userbc.GetAllUsers();
            return new JsonResponse()
            {
                Data = Users
            };
        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("user/adduser")]
        public JsonResponse AddUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("User id is null!");
                int employeeId = Convert.ToInt32(user.EmployeeId);
                int projectId = Convert.ToInt32(user.ProjectId);
                if (employeeId < 0)
                    throw new ArithmeticException("Employee id cannot be negative!");
                if (projectId < 0)
                    throw new ArithmeticException("Project id cannot be negative!");

                return new JsonResponse()
                {
                    Data = userbc.AddUser(user)
                };
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid format of employee Id or project Id!", ex);
            }
        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("user/updateuser")]
        public JsonResponse UpdateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("User id is null!");
                int employeeId = Convert.ToInt32(user.EmployeeId);
                int projectId = Convert.ToInt32(user.ProjectId);
                if (employeeId < 0)
                    throw new ArithmeticException("Employee id cannot be negative!");
                if (projectId < 0)
                    throw new ArithmeticException("Project id cannot be negative!");

                return new JsonResponse()
                {
                    Data = userbc.UpdateUser(user)
                };
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid format of employee Id or project Id!", ex);
            }
        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("user/deleteuser")]
        public JsonResponse DeleteUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("User id is null!");
                int employeeId = Convert.ToInt32(user.EmployeeId);
                int projectId = Convert.ToInt32(user.ProjectId);
                if (employeeId < 0)
                    throw new ArithmeticException("Employee id cannot be negative!");
                if (projectId < 0)
                    throw new ArithmeticException("Project id cannot be negative!");

                return new JsonResponse()
                {
                    Data = userbc.DeleteUser(user)
                };
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid format of employee Id or project Id!", ex);
            }
        }
    }
}
