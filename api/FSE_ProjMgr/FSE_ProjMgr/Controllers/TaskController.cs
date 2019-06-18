using FSE_ProjMgr.ActionFilters;
using FSE_ProjMgr.Models;
using FSE_ProjMgr.ProjMgrBC;
using FSE_ProjMgr.ProjMgrDAC;
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
    public class TaskController : ApiController
    {
        TaskBc taskObj = null;
        public TaskController()
        {
            taskObj = new TaskBc();
        }
        [HttpGet]
        [Route("task/tasksbyprojid/{projId}")]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        public JsonResponse GetTaskByProjId(int projId)
        {
            if (projId < 0)
            {
                throw new ArithmeticException("ProjectId cannot be negative!");
            }
            List<Models.Task> Tasks = taskObj.GetTaskByProjId(projId);
            return new JsonResponse()
            {
                Data = Tasks
            };
        }

        [HttpGet]
        [Route("task/parent")]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        public JsonResponse GetParentTasks()
        {
            List<ParentTask> ParentTasks = taskObj.GetParentTasks();

            return new JsonResponse()
            {
                Data = ParentTasks
            };

        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("task/addtask")]
        public JsonResponse InsertTask(Models.Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null!");
            }
            if (task.ParentID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative!");
            }
            if (task.ProjectID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative!");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative!");
            }
            return new JsonResponse()
            {
                Data = taskObj.InsertTask(task)
            };
        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("task/updatetask")]
        public JsonResponse UpdateTaskDetails(Models.Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null!");
            }
            if (task.ParentID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative!");
            }
            if (task.ProjectID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative!");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative!");
            }
            return new JsonResponse()
            {
                Data = taskObj.UpdateTaskDetails(task)
            };

        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("task/deletetask")]
        public JsonResponse DeleteTaskDetails(Models.Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null!");
            }
            if (task.ParentID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative!");
            }
            if (task.ProjectID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative!");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative!");
            }
            return new JsonResponse()
            {
                Data = taskObj.DeleteTaskDetails(task)
            };
        }
    }
}
