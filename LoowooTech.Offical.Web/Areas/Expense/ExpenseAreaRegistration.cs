using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense
{
    public class ExpenseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Expense";//报销
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Expense_default",
                "Expense/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                new [] { "LoowooTech.Offical.Web.Areas.Expense.Controllers" }
            );
        }
    }
}