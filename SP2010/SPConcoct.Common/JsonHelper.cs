using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SPConcoct.Common
{
    class JsonHelper
    {

        //#region Json Helper Methods

        //protected void CreateJsonListAndSend(DataTable dt, string keyColumnName, string valueColumnName)
        //{
        //    DataTable dtData = CreateJsonTable(dt, keyColumnName, valueColumnName);

        //    List<string[]> lst = new List<string[]>();
        //    foreach (DataRow dr in dtData.Rows)
        //    {
        //        string[] arr = new string[2];

        //        arr[0] = Convert.ToString(dr["key"]);
        //        arr[1] = Convert.ToString(dr["value"]);
        //        lst.Add(arr);
        //    }

        //    SerializeAndSend(lst);
        //}

        //protected DataTable CreateJsonTable(DataTable dtData, string keyColumnName, string valueColumnName)
        //{
        //    DataTable dtJson = new DataTable();
        //    dtJson.Columns.Add("key");
        //    dtJson.Columns.Add("value");

        //    foreach (DataRow dr in dtData.Rows)
        //    {
        //        string i_VesselType = Convert.ToString(dr[keyColumnName]);
        //        string CandidateCount = Convert.ToString(dr[valueColumnName]);
        //        CreateDR(dtJson, i_VesselType, CandidateCount);
        //    }
        //    return dtJson;

        //}

        //protected void CreateDR(DataTable dtJson, string key, string value)
        //{
        //    DataRow dr = dtJson.NewRow();
        //    dr["key"] = key;
        //    dr["value"] = value;
        //    dtJson.Rows.Add(dr);
        //}
        //protected void SerializeAndSend(object obj)
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    var json = ser.Serialize(obj);

        //    Response.ContentType = "application/json";
        //    Response.Write(json);
        //    Response.End();
        //}
        //#endregion


    }
}
