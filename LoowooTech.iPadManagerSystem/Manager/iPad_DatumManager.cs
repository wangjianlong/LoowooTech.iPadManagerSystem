﻿using LoowooTech.iPadManagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class iPad_DatumManager:ManagerBase
    {
        /// <summary>
        /// 作用：添加平板数据记录
        /// 作者：汪建龙
        /// 编写时间：2016年12月11日22:07:14
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public int Add(iPadDatum datum)
        {
            using (var db = GetDbContext())
            {
                db.iPad_Datums.Add(datum);
                db.SaveChanges();
                return datum.ID;
            }
        }
        /// <summary>
        /// 作用：获取平板数据所有可用记录
        /// 作者：汪建龙
        /// 编写时间：2016年12月11日22:07:40
        /// </summary>
        /// <returns></returns>
        public List<iPadDatum> Get()
        {
            using (var db = GetDbContext())
            {
                return db.iPad_Datums.Where(e => e.Repeal == false).OrderByDescending(e => e.ID).ToList();
            }
        }
        /// <summary>
        /// 作用：平板数据作废
        /// 作者：汪建龙
        /// 编写时间：2016年12月14日11:46:04
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Repeal(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.iPad_Datums.Find(id);
                if (entry == null)
                {
                    return false;
                }
                entry.Repeal = true;
                db.SaveChanges();
                return true;
            }
        }
    }
}