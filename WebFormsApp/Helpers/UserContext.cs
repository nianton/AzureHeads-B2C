using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsApp.Helpers
{
    public class UserContext
    {
        private const string SessionKey = "__UserContext";

        public UserContext(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
        public ISet<string> PassportRoles { get; set; }
        public int PassportId { get; set; }

        // TODO: Rest of User session information goes here.


        public static UserContext FromSession()
        {
            return HttpContext.Current.Session[SessionKey] as UserContext;
        }

        public static void AbandonSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static void InitSession(UserContext userCtx)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session[SessionKey] = userCtx;
        }
    }
}