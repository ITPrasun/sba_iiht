using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSE_ProjMgr.ProjMgrBC
{
    public class ProjBc
    {
        ProjMgrDAC.ProjectManagerEntities1 dbContext = null;
        public ProjBc()
        {
            dbContext = new ProjMgrDAC.ProjectManagerEntities1();
        }
        public List<Models.Project> GetProjects()
        {
            using (dbContext)
            {
                return dbContext.Projects.Select(x => new Models.Project()
                {
                    ProjectId = x.Project_ID,
                    ProjectName = x.Project_Name,
                    ProjectEndDate = x.End_Date,
                    ProjectStartDate = x.Start_Date,
                    Priority = x.Priority,
                    User = dbContext.Users.Where(y => y.Project_ID == x.Project_ID).Select(z => new Models.User()
                    {
                        UserId = z.User_ID
                    }).FirstOrDefault(),
                    NoOfTasks = dbContext.Tasks.Where(y => y.Project_ID == x.Project_ID).Count(),
                    NoOfCompletedTasks = dbContext.Tasks.Where(y => y.Project_ID == x.Project_ID && y.Status == 1).Count(),
                }).ToList();
            }
        }

        public int AddProject(Models.Project project)
        {
            using (dbContext)
            {
                ProjMgrDAC.Project proj = new ProjMgrDAC.Project()
                {
                    Project_Name = project.ProjectName,
                    Start_Date = project.ProjectStartDate,
                    End_Date = project.ProjectEndDate,
                    Priority = project.Priority
                };
                dbContext.Projects.Add(proj);
                dbContext.SaveChanges();
                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID.ToString().Contains(project.User.UserId.ToString())
                                   select editUser).First();
                if (editDetails != null)
                {
                    editDetails.Project_ID = proj.Project_ID;
                }
                return dbContext.SaveChanges();
            }
        }

        public int UpdateProject(Models.Project project)
        {
            using (dbContext)
            {
                var editProjDetails = (from editProject in dbContext.Projects
                                       where editProject.Project_ID.ToString().Contains(project.ProjectId.ToString())
                                       select editProject).First();
                if (editProjDetails != null)
                {
                    editProjDetails.Project_Name = project.ProjectName;
                    editProjDetails.Start_Date = project.ProjectStartDate;
                    editProjDetails.End_Date = project.ProjectEndDate;
                    editProjDetails.Priority = project.Priority;
                }


                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID.ToString().Contains(project.User.UserId.ToString())
                                   select editUser).First();
                if (editDetails != null)
                {
                    editDetails.Project_ID = project.ProjectId;
                }
                return dbContext.SaveChanges();
            }

        }
        public int DeleteProject(Models.Project project)
        {
            using (dbContext)
            {

                var editDetails = (from proj in dbContext.Projects
                                   where proj.Project_ID == project.ProjectId
                                   select proj).First();
                if (editDetails != null)
                {
                    dbContext.Projects.Remove(editDetails);
                }
                return dbContext.SaveChanges();
            }

        }
    }
}