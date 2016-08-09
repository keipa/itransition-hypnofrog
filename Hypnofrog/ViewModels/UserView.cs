﻿using Hypnofrog.DBModels;
using Hypnofrog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypnofrog.ViewModels
{
    public class UserView
    {
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string UserId { get; set; }
        public double Rating { get; set; }
        public bool IsAdmin { get; set; }

        public UserView() { }

        public UserView(Models.ApplicationUser user, string avatarpath, double rating, bool isadmin)
        {
            this.UserName = user.UserName;
            this.UserId = user.Id;
            this.UserAvatar = avatarpath;
            this.Rating = rating;
            this.IsAdmin = isadmin;
        }

        public static IEnumerable<UserView> GetUserViews(IEnumerable<Models.ApplicationUser> users)
        {
            List<UserView> result = new List<UserView>();
            using (var db = new Context())
            {
                using (var udb = new ApplicationDbContext())
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(udb));
                    foreach (var user in users)
                        try
                        {
                            result.Add(new UserView(user, db.Avatars.Where(x => x.UserId == user.UserName).FirstOrDefault().Path, GetProfilerRate(db, user.UserName), UserManager.IsInRole(user.Id, "Admin")));
                        }
                        catch
                        {
                            result.Add(new UserView(user, db.Avatars.Where(x => x.UserId == user.UserName).FirstOrDefault().Path, GetProfilerRate(db, user.UserName), false));
                        }
                }
            }
            return result;
        }

        private static double GetProfilerRate(Context db, string username)
        {
            List<Site> sites = new List<Site>();
            double average = 0.0;
            sites = db.Sites.Where(x => x.UserId == username).ToList();
            foreach (var item in sites) average += item.Rate;
            if (sites.Count() == 0) return 0.0;
            average = average / sites.Count();
            return average;
        }
    }
}