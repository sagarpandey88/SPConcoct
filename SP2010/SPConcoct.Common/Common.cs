using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPConcoct.Common
{
    public class Common
    {
        /// <summary>
        /// Removes the FBA string from the login name
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private static string RemoveFBAStringLoginName(string loginName)
        {
            string FBAKey = System.Configuration.ConfigurationManager.AppSettings["MembershipProvider"];

            if (loginName.Contains(FBAKey))
            {
                loginName = loginName.Replace(FBAKey, "");
            }

            return loginName;
        }

        /// <summary>
        /// Seperates the comma seperated value and returns it as a list of string
        /// </summary>
        /// <param name="commaSeperatedValues">comma seperated string</param>
        /// <returns>List of seperated string</returns>
        public static List<string> GetCommaSeperatedItems(string commaSeperatedValues)
        {
            List<string> seperatedValue = new List<string>();
            if (!string.IsNullOrEmpty(commaSeperatedValues))
            {
                commaSeperatedValues = commaSeperatedValues.TrimEnd(',');
            }

            if (commaSeperatedValues.Contains(","))
            {
                seperatedValue = commaSeperatedValues.Split(',').ToList();
            }
            else
            {
                //if the string is not null and doesnot contain comma
                if (!string.IsNullOrEmpty(commaSeperatedValues))
                {
                    seperatedValue.Add(commaSeperatedValues);
                }
            }

            return seperatedValue;

        }


        /// <summary>
        /// Gets the Start day of the week as date time object
        /// </summary>
        /// <param name="dt">date of which the start day of the week has to be determined</param>
        /// <param name="startOfWeek">Value of the start day of the week. ex Sunday or Monday ...</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }


    }
}
