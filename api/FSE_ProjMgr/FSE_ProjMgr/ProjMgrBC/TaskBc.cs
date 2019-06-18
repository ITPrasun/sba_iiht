using FSE_ProjMgr.Models;
using FSE_ProjMgr.ProjMgrDAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSE_ProjMgr.ProjMgrBC
{
    public class TaskBc
    {
        ProjMgrDAC.ProjectManagerEntities dbContext = null;
        public TaskBc()
        {
            dbContext = new ProjMgrDAC.ProjectManagerEntities();
        }
        public List<Models.Task> GetTaskByProjId(int projId)
        {
            using (dbContext)
            {
                return dbContext.Tasks.Where(z => z.Project_ID == projId).Select(x => new Models.Task()
                {
                    TaskId = x.Task_ID,
                    TaskName = x.Task_Name,
                    ParentTaskName = dbContext.ParentTasks.Where(y => y.ParentID == x.Parent_ID).FirstOrDefault().ParentTaskName,
                    StartDate = x.Start_Date,
                    EndDate = x.End_Date,
                    Priority = x.Priority,
                    Status = x.Status,
                    User = dbContext.Users.Where(y => y.Task_ID == x.Task_ID).Select(z => new Models.User()
                    {
                        UserId = z.User_ID,
                        FirstName = z.First_Name
                    }).FirstOrDefault(),
                }).ToList();
            }

        }

        public List<ParentTask> GetParentTasks()
        {
            using (dbContext)
            {
                return dbContext.ParentTasks.Select(x => new ParentTask()
                {
                    ParentID = x.ParentID,
                    ParentTaskName = x.ParentTaskName
                }).ToList();
            }
        }

        public int InsertTask(Models.Task task)
        {
            using (dbContext)
            {
                if (task.Priority == 0)
                {
                    dbContext.ParentTasks.Add(new ProjMgrDAC.ParentTask()
                    {
                        ParentTaskName = task.TaskName

                    });
                }
                else
                {
                    ProjMgrDAC.Task taskDetail = new ProjMgrDAC.Task()
                    {
                        Task_Name = task.TaskName,
                        Project_ID = task.ProjectID,
                        Start_Date = task.StartDate,
                        End_Date = task.EndDate,
                        Parent_ID = task.ParentID,
                        Priority = task.Priority,
                        Status = task.Status
                    };
                    dbContext.Tasks.Add(taskDetail);
                    dbContext.SaveChanges();

                    var editDetails = (from editUser in dbContext.Users
                                       where editUser.User_ID.ToString().Contains(task.User.UserId.ToString())
                                       select editUser).ToList();
                    if (editDetails != null && editDetails.Count > 0)
                    {
                        editDetails.First().Task_ID = taskDetail.Task_ID;
                    }
                }
                return dbContext.SaveChanges();
            }
        }

        public int UpdateTaskDetails(Models.Task task)
        {
            using (dbContext)
            {
                var editDetails = (from editTask in dbContext.Tasks
                                   where editTask.Task_ID.ToString().Contains(task.TaskId.ToString())
                                   select editTask).First();
                if (editDetails != null)
                {
                    editDetails.Task_Name = task.TaskName;
                    editDetails.Start_Date = task.StartDate;
                    editDetails.End_Date = task.EndDate;
                    editDetails.Status = task.Status;
                    editDetails.Priority = task.Priority;

                }
                var editDetailsUser = (from editUser in dbContext.Users
                                       where editUser.User_ID.ToString().Contains(task.User.UserId.ToString())
                                       select editUser).First();
                if (editDetailsUser != null)
                {
                    editDetails.Task_ID = task.TaskId;
                }
                return dbContext.SaveChanges();
            }
        }

        public int DeleteTaskDetails(Models.Task task)
        {
            using (dbContext)
            {
                var deleteTask = (from editTask in dbContext.Tasks
                                  where editTask.Task_ID.ToString().Contains(task.TaskId.ToString())
                                  select editTask).First();
                if (deleteTask != null)
                {
                    deleteTask.Status = 1;
                }
                return dbContext.SaveChanges();
            }
        }
    }
}