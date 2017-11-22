using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project
{
    public class ProjectAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Project";//项目
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Project_default",
                "Project/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "LoowooTech.Offical.Web.Areas.Project.Controllers" }
            );
        }
    }
}