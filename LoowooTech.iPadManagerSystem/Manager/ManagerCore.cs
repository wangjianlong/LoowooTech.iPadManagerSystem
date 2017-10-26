using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class ManagerCore
    {
        public static readonly ManagerCore Instance = new ManagerCore();


        private ManagerCore()
        {
            foreach (var p in this.GetType().GetProperties())
            {
                if (p.PropertyType == this.GetType())
                {
                    continue;
                }
                var val = p.GetValue(this);
                if (val == null)
                {
                    p.SetValue(this, Activator.CreateInstance(p.PropertyType));
                }
            }
        }

        public UserManager UserManager { get; private set; }
        public iPadManager iPadManager { get; private set; }
        public iPad_AccountMnaager iPad_AccountManager { get; private set; }
        public iPad_DatumManager iPad_DatumManager { get; private set; }
        public iPad_ContactManager iPad_ContactManager { get; private set; }

        public iPad_RegisterManager iPad_registerManager { get; private set; }

        public Register_iPadManager Register_iPadManager { get; private set; }

        public iPad_ContractManager iPad_ContractManager { get; private set; }
        public iPad_InvoiceManager iPad_InvoiceManager { get; private set; }
        public ColorManager ColorManager { get; private set; }
    }
}