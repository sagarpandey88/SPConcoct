using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Microsoft.SharePoint.Utilities;

namespace SPConcoct.SPCommon
{
    /// <summary>
    /// This Class is hepler class to build dynamic caml query
    /// </summary>
    /*
     * TODO
     * SPUtility.CreateISO8601DateTimeFromSystemDateTime() --Done
     * OffsetDate in Date Time Query
     */
    public class CamlQueryHelper
    {
        /// <summary>
        /// Creates Dynamic caml query based on the query item list provided
        /// </summary>
        /// <param name="queryItemsList"></param>
        /// <returns></returns>
        public string CreateQuery(List<QueryItem> queryItemsList)
        {
            StringBuilder queryMain = new StringBuilder();
            queryMain.Append("<Where>");

            queryMain.Append(CreateSubQuery(queryItemsList));

            queryMain.Append("</Where>");

            return queryMain.ToString();

        }

        /// <summary>
        /// Sub function of create query to create the dynamic caml query
        /// </summary>
        /// <param name="queryItemsList"></param>
        /// <returns></returns>
        protected string CreateSubQuery(List<QueryItem> queryItemsList)
        {
            StringBuilder subQuery = new StringBuilder();
            if (queryItemsList.Count == 1)
            {
                //subQuery.Append("<" + queryItemsList[0].ComparerType + "><FieldRef Name='" + queryItemsList[0].ColumnName + "' /><Value Type='Text'>" + queryItemsList[0].ColumnValue + "</Value></" + queryItemsList[0].ComparerType + ">");
                subQuery.Append(GetQueryBasedOnColumnDataType(queryItemsList[0]));
            }
            else if (queryItemsList.Count > 1)
            {
                subQuery.Append(AppendAnds(queryItemsList.Count));
                foreach (QueryItem itm in queryItemsList)
                {

                    //  subQuery.Append("<" + itm.ComparerType + "><FieldRef Name='" + itm.ColumnName + "' /><Value Type='Text'>" + itm.ColumnValue + "</Value></" + itm.ComparerType + ">");
                    subQuery.Append(GetQueryBasedOnColumnDataType(itm));

                    if (queryItemsList.IndexOf(itm) > 0)
                    {
                        subQuery.Append("</And>");
                    }
                }

            }

            return subQuery.ToString();
        }

        /// <summary>
        /// Creates text based query based on the data type of the column
        /// </summary>
        /// <param name="itm"></param>
        /// <returns></returns>
        protected string GetQueryBasedOnColumnDataType(QueryItem itm)
        {
            string queryText = "";
            switch (itm.ColumnDataType)
            {
                //Regular Query
                case CamlColumnDataType.Text:
                case CamlColumnDataType.None:
                    queryText = "<" + itm.ComparerType + "><FieldRef Name='" + itm.ColumnName + "' /><Value Type='Text'>" + itm.ColumnValue + "</Value></" + itm.ComparerType + ">";
                    break;
                //IncludeTimeValue and Type
                case CamlColumnDataType.DateTime:
                    queryText = "<" + itm.ComparerType + "><FieldRef Name='" + itm.ColumnName + "' /><Value  IncludeTimeValue='FALSE' Type='DateTime'>" + GetSPDateFormated(itm.ColumnValue) + "</Value></" + itm.ComparerType + ">";
                    break;
                //Lookup ID = true 
                case CamlColumnDataType.Lookup:
                case CamlColumnDataType.User:
                    //queryText = "<" + itm.ComparerType + "><FieldRef Name='" + itm.ColumnName + "'  LookupId='TRUE' /><Value Type='Text'>" + itm.ColumnValue + "</Value></" + itm.ComparerType + ">";
                    if (itm.ComparerType == QueryItem.CamlComparerIn)
                    {
                        queryText = "<" + itm.ComparerType + "><FieldRef Name='" + itm.ColumnName + "'  LookupId='TRUE' /><Values>" + GetCommaSeperatedValuesForInClause(itm.ColumnValue) + "</Values></" + itm.ComparerType + ">";
                    }
                    else
                    {
                        queryText = "<" + itm.ComparerType + "><FieldRef Name='" + itm.ColumnName + "'  LookupId='TRUE' /><Value Type='Text'>" + itm.ColumnValue + "</Value></" + itm.ComparerType + ">";
                    }
                    break;
            }

            return queryText;
        }

        /// <summary>
        /// Appends "AND" Condition based on the number of query item count
        /// </summary>
        /// <param name="andCount"></param>
        /// <returns></returns>
        protected string AppendAnds(int andCount)
        {
            string ands = "";
            for (int i = 0; i < andCount - 1; i++)
            {
                ands += "<And>";
            }
            return ands;

        }

        /// <summary>
        /// Gets the SP formatted date
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        protected string GetSPDateFormated(string dateValue)
        {
            DateTime dt = Convert.ToDateTime(dateValue);
            return SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt);
        }

        /// <summary>
        /// Helper method to add a query item 
        /// </summary>
        /// <param name="querableItems"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <param name="comparerType"></param>
        /// <param name="columnDataType"></param>
        public void AddParameterInQuery(List<QueryItem> querableItems, string columnName, string columnValue, string comparerType, CamlColumnDataType columnDataType)
        {
            QueryItem queryParam = new QueryItem();
            queryParam.ColumnName = columnName;
            queryParam.ColumnValue = columnValue;
            queryParam.ComparerType = comparerType;
            queryParam.ColumnDataType = columnDataType;
            querableItems.Add(queryParam);

        }

        /// <summary>
        /// Get the text based query from comma seperated values
        /// </summary>
        /// <param name="commaValues"></param>
        /// <returns></returns>
        protected string GetCommaSeperatedValuesForInClause(string commaValues)
        {
            List<string> valuesList = new List<string>();
            if (commaValues.Contains(','))
            {
                valuesList = commaValues.Split(',').ToList();
            }
            else
            {
                valuesList.Add(commaValues);
            }

            StringBuilder formattedValues = new StringBuilder();

            foreach (string strValue in valuesList)
            {
                formattedValues.Append("<Value Type='Text'>");
                formattedValues.Append(strValue);
                formattedValues.Append("</Value>");

            }


            return formattedValues.ToString();

        }

    }

    #region Properties and Enums

    public class QueryItem
    {
        public const string CamlComparerLeq = "Leq";
        public const string CamlComparerGeq = "Geq";
        public const string CamlComparerEq = "Eq";
        public const string CamlComparerContains = "Contains";
        public const string CamlComparerBeginsWith = "BeginsWith";
        public const string CamlComparerIn = "In";

        public QueryItem()
        {

        }
        public QueryItem(string columnName, string columnValue, string comparerType, CamlColumnDataType columnDateType)
        {
            ColumnName = columnName;
            ColumnValue = columnValue;
            ComparerType = comparerType;
            ColumnDataType = columnDateType;
        }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string ComparerType { get; set; }
        public CamlColumnDataType ColumnDataType { get; set; }
    }

    public enum CamlColumnDataType
    {
        None, DateTime, Text, Lookup, User
    }
    #endregion
}