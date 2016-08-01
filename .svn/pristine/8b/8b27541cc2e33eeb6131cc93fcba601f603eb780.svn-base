using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;


namespace Plus54PortfolioRedesign2014.Web
{

    public static class SessionHelper
    {
        public static UserSession CurrentUser
        {
            get
            {
                return (UserSession)CurrentSession["CurrentUser"];
            }
            set
            {
                CurrentSession["CurrentUser"] = value;
            }
        }

        private static HttpSessionState CurrentSession
        {
            [DebuggerStepThrough]
            get
            {
                if (HttpContext.Current.Session == null)
                    throw new NullReferenceException("Session is not available in the current context.");
                else
                    return HttpContext.Current.Session;
            }
        }

        public static void Logout()
        {
            CurrentSession.Abandon();
        }

        public static bool IsSignedIn
        {
            get
            {
                if (CurrentUser != null)
                    return true;

                return false;
            }
        }
    }
}