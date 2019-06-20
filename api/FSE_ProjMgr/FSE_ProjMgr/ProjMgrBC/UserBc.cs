using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSE_ProjMgr.ProjMgrBC
{
    public class UserBc
    {
        ProjMgrDAC.ProjectManagerEntities1 dbContext = null;
        public UserBc()
        {
            dbContext = new ProjMgrDAC.ProjectManagerEntities1();
        }
        public UserBc(ProjMgrDAC.ProjectManagerEntities1 context)
        {
            dbContext = context;
        }
        public List<Models.User> GetAllUsers()
        {
            using (dbContext)
            {
                return dbContext.Users.Select(x => new Models.User()
                {
                    FirstName = x.First_Name,
                    LastName = x.Last_Name,
                    EmployeeId = x.Employee_ID,
                    UserId = x.User_ID
                }).ToList();
            }
        }

        public int AddUser(Models.User user)
        {
            using (dbContext)
            {
                dbContext.Users.Add(new ProjMgrDAC.User()
                {
                    Last_Name = user.LastName,
                    First_Name = user.FirstName,
                    Employee_ID = user.EmployeeId
                });
                return dbContext.SaveChanges();
            }
        }

        public int UpdateUser(Models.User user)
        {
            using (dbContext)
            {
                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
                if (editDetails != null)
                {
                    editDetails.First_Name = user.FirstName;
                    editDetails.Last_Name = user.LastName;
                    editDetails.Employee_ID = user.EmployeeId;

                }
                return dbContext.SaveChanges();
            }
        }

        public int DeleteUser(Models.User user)
        {
            using (dbContext)
            {
                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
                if (editDetails != null)
                    dbContext.Users.Remove(editDetails);
                return dbContext.SaveChanges();
            }

        }
    }
}