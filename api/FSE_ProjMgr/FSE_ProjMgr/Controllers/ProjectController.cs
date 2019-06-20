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
    public class ProjectController : ApiController
    {
        ProjBc projObjBC = null;

        public ProjectController()
        {
            projObjBC = new ProjBc();
        }
        public ProjectController(ProjBc projBC)
        {
            projObjBC = projBC;
        }
        [HttpGet]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("project/getproj")]
        public JsonResponse GetProjects()
        {
            List<Project> Projects = projObjBC.GetProjects();

            return new JsonResponse()
            {
                Data = Projects
            };

        }

        [HttpPost]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        [Route("project/addproj")]
        public JsonResponse AddProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("Project is null!");
            }
            if (project.ProjectId < 0)
            {
                throw new ArithmeticException("Project ID cannot be negative!");
            }
            if (project.User == null)
            {
                throw new ArgumentNullException("User related to the project cannot be null!");
            }
            if (project.User.ProjectId < 0)
            {
                throw new ArithmeticException("User object project Id cannot be negative!");
            }
            if (project.NoOfCompletedTasks > project.NoOfTasks)
            {
                throw new ArgumentException("Completed tasks cannot be greater than total tasks!");
            }
            return new JsonResponse()
            {
                Data = projObjBC.AddProject(project)
            };

        }


        [HttpPost]
        [Route("project/updateproj")]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        public JsonResponse UpdateProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("Project is null!");
            }
            if (project.ProjectId < 0)
            {
                throw new ArithmeticException("Project ID cannot be negative!");
            }
            if (project.User == null)
            {
                throw new ArgumentNullException("User related to the project cannot be null!");
            }
            if (project.User.ProjectId < 0)
            {
                throw new ArithmeticException("User object project Id cannot be negative!");
            }
            if (project.NoOfCompletedTasks > project.NoOfTasks)
            {
                throw new ArgumentException("Completed tasks cannot be greater than total tasks");
            }
            return new JsonResponse()
            {
                Data = projObjBC.UpdateProject(project)
            };
        }

        [HttpPost]
        [Route("project/deleteproj")]
        [ExceptionLogFilter]
        [TransactionLogFilter]
        public JsonResponse DeleteProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("Project is null!");
            }
            if (project.ProjectId < 0)
            {
                throw new ArithmeticException("Project ID cannot be negative!");
            }
            if (project.User == null)
            {
                throw new ArgumentNullException("User related to the project cannot be null!");
            }
            if (project.User.ProjectId < 0)
            {
                throw new ArithmeticException("User object project Id cannot be negative!");
            }
            if (project.NoOfCompletedTasks > project.NoOfTasks)
            {
                throw new ArgumentException("Completed tasks cannot be greater than total tasks!");
            }
            return new JsonResponse()
            {
                Data = projObjBC.DeleteProject(project)
            };
        }
    }
}
