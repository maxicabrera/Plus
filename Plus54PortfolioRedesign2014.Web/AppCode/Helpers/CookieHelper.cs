using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plus54PortfolioRedesign2014.Common;

namespace Plus54PortfolioRedesign2014.Web.AppCode.Helpers
{
    public class CookieHelper
    {
        public static HttpCookie CreateCookie<T>(string cookieName, ref T obj)
        {
            HttpCookie cookie = null;
            string serialized = Serializer.SerializeToXml<T>(ref obj);

            if (((obj != null) & !string.IsNullOrEmpty(cookieName)))
            {
                string encriptedString = Encryptor.DoEncryptBasic(serialized);
                cookie = new HttpCookie(cookieName, encriptedString);
            }
            return cookie;
        }

        public static T GetCookie<T>(HttpCookie cookie)
        {
            T serialized = default(T);

            if ((!string.IsNullOrEmpty(cookie.Value)))
            {
                string desencriptedString = Encryptor.DoDecryptBasic(cookie.Value);
                serialized = Serializer.DeserializeFromXml<T>(ref desencriptedString);
            }
            return serialized;
        }

        public static void DeleteCookie(string cookieName)
        {
            HttpContext.Current.Response.Cookies.Remove(cookieName);

            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #region UserCookie

        private const string DEFAULT_USER_COOKIE = "DEFAULT_USER_COOKIE";

        /// <summary>
        /// Gets or Sets the default User Id and saves the value in cookie encrypted.
        /// </summary>
        public static int DefaultIdUser
        {
            get
            {
                //HttpCookie cookie = HttpContext.Current.Request.Cookies[DEFAULT_USER_COOKIE + HttpContext.Current.Session.SessionID];
                HttpCookie cookie = HttpContext.Current.Request.Cookies[DEFAULT_USER_COOKIE];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    string value = GetCookie<string>(cookie);

                    var auxCookie = Encryptor.DoDecryptBasic(value);

                    return Convert.ToInt16(auxCookie);
                }

                return 0;
            }
            set
            {
                //string cookieName = DEFAULT_USER_COOKIE + HttpContext.Current.Session.SessionID;
                string cookieName = DEFAULT_USER_COOKIE;
                HttpContext.Current.Response.Cookies.Remove(cookieName);

                var auxCookie = Encryptor.DoEncryptBasic(value.ToString());

                HttpCookie cookie = CreateCookie<string>(cookieName, ref auxCookie);
                cookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void DeleteUserCookie()
        {
            DeleteCookie(DEFAULT_USER_COOKIE);
        }

        #endregion
    }
}