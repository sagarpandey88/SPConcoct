using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Text;

namespace SPConcoct.SAPConnect
{
    public class CommonMethods
    {
        static string LogFileLocation = ConfigurationManager.AppSettings["LogFileLocation"];
        static string LogFileName = ConfigurationManager.AppSettings["LogFileName"];
        static string LogFile = LogFileLocation + LogFileName + DateTime.Now.Date.ToString("ddMMMyyyy") + ".txt";
        public const string SAPDateFormat = "yyyy-MM-dd";
        public const string SAPMinDate = "0000-00-00";
        public const string SAPMaxDate = "9999-12-31";
         const string EJSSAPDateFormat = "yyyyMMdd";

        public static void WriteToTraceLog(string Message, string action)
        {
            try
            {
                if (!File.Exists(LogFile))
                {
                    File.Create(LogFile).Close();

                }

                StreamWriter sw = File.AppendText(LogFile);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendFormat("[{0}] Event Occurred:\n", DateTime.Now.ToString());
                sb.AppendFormat(": {0}", action);
                sb.AppendFormat(": {0}", Message);
                sw.WriteLine(sb.ToString());
                sw.Close();

            }
            catch (Exception ex)
            {

            }
        }


        public void AddResultStatus(string resultStatus)
        {
            ReturnValue returnResult = new ReturnValue();
            returnResult.ParameterName = "ProcessExceuctionResult";
            returnResult.ParameterCharValue = resultStatus;
            returnResult.ParameterType = "Result Status";
        }

        public static DateTime ParseSAPDate(object objDate)
        {
            string date = Convert.ToString(objDate);
            DateTime dtDate = new DateTime();
            if (date != SAPMinDate)
                dtDate = DateTime.ParseExact(date, SAPDateFormat, System.Globalization.CultureInfo.InvariantCulture);

            return dtDate;

        }

        public static int DifferenceYears(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        public static string ToSAPDateFormat(DateTime dt)
        {
            string returnDate = "";
            try
            {
                returnDate = Convert.ToString(dt.ToString(SAPDateFormat));
            }
            catch (Exception ex)
            {

            }

            return returnDate;

        }

        /// <summary>
        /// This method to be used only when passing birthdate data to HR_MAINTAINMASTER_DATA
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSEJSAPDateFormat(DateTime dt)
        {
            string returnDate = "";
            try
            {
                returnDate = Convert.ToString(dt.ToString(EJSSAPDateFormat));
            }
            catch (Exception ex)
            {

            }

            return returnDate;

        }


    }
}