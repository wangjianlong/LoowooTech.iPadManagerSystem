﻿using LoowooTech.iPadManagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class iPad_AccountMnaager:ManagerBase
    {
        public List<iPadAccount> Get()
        {
            using (var db = GetDbContext())
            {
                return db.iPad_Accounts.ToList();
            }
        }

        public int Save(iPadAccount account, bool edit)
        {
            using (var db = GetDbContext())
            {
                if (edit)
                {
                    var entry = db.iPad_Accounts.Find(account.ID);
                    if (entry != null)
                    {
                        db.Entry(entry).CurrentValues.SetValues(account);

                    }
                }
                else
                {
                    var entry = db.iPad_Accounts.FirstOrDefault(e => e.Account == account.Account || e.EMail == account.EMail);
                    if (entry == null)
                    {
                        db.iPad_Accounts.Add(account);
                    }

                }
                db.SaveChanges();
                return account.ID;
            }
        }

        public iPadAccount Get(int id)
        {
            if (id == 0) return null;
            using (var db = GetDbContext())
            {
                return db.iPad_Accounts.Find(id);
            }
        }
    }
}