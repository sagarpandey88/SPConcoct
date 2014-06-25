using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPConcoct.Common
{
    public class Common
    {

        private static string RemoveFBAStringLoginName(string loginName)
        {
            string FBAKey = System.Configuration.ConfigurationManager.AppSettings["MembershipProvider"];

            if (loginName.Contains(FBAKey))
            {
                loginName = loginName.Replace(FBAKey, "");
            }

            return loginName;
        }


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



    }
}
