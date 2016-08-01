using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Plus54PortfolioRedesign2014.Common
{
    public static class Utility
    {
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }        /// <summary>
        /// Randomizes a number
        /// </summary>
        /// <param name="minNumber">Minimum number</param>
        /// <param name="maxNumber">Maximum number</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int RandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            return random.Next(minNumber, maxNumber);
        }

        /// <summary>
        /// Randomizes a string
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lower">Lower value</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RandomString(int size, bool lower)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch = '\0';
            int i = 0;

            //VBMath.Randomize();

            for (i = 0; i <= size; i++)
            {
                ch = Convert.ToChar(random.Next(65, 91));
                builder.Append(ch);
            }

            if (lower)
            {
                return builder.ToString().ToLower(CultureInfo.CurrentCulture);
            }
            else
            {
                return builder.ToString();
            }
        }

        public static string ToDescription(Enum en) //ext method
        {
            if (en != null)
            {
                Type type = en.GetType();
                MemberInfo[] memInfo = type.GetMember(en.ToString());
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                        return ((DescriptionAttribute)attrs[0]).Description;
                }
                return en.ToString();
            }

            return String.Empty;
        }

        public static List<T> ToListFromEnum<T>()
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString(CultureInfo.InvariantCulture)));
            }

            return enumValList;
        }

    }
}
