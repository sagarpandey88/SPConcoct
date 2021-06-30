using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;
using System.Collections;
using System.Data;

namespace SPConcoct.SAPConnect
{
    public class SAPExtension
    {
        /* to do
         * Create a method for transaction
        * Make extension for all the data types
         */
        public const string InputParamterCharacter = "CHAR";
        public const string InputParamterTable = "Table";
        public const string InputParamterStructure = "Structure";
        private const string TransactionBAPIName = "BAPI_TRANSACTION_COMMIT";
        public List<ReturnValue> ExecuteSAPReader(string functionName, List<InputParamters> inputParamters)
        {
            List<ReturnValue> outputParameters = new List<ReturnValue>();
            RfcDestination oRfcDestination = GetConnection();
            IRfcFunction IRfLock = oRfcDestination.Repository.CreateFunction(functionName);
            SetInputParamters(IRfLock, inputParamters);
            IRfLock.Invoke(oRfcDestination);
            outputParameters = SetOutputParamters(IRfLock);
            return outputParameters;
        }

        public List<ReturnValue> ExecuteSAPWithTransaction(string functionName, List<InputParamters> inputParamters)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();

            List<ReturnValue> outputParameters = new List<ReturnValue>();
            RfcDestination oRfcDestination = GetConnection();
            IRfcFunction IRfLock = oRfcDestination.Repository.CreateFunction(functionName);
            IRfcFunction IRfcCommit = oRfcDestination.Repository.CreateFunction(TransactionBAPIName);
            SetInputParamters(IRfLock, inputParamters);
            IRfLock.Invoke(oRfcDestination);
            IRfcCommit.Invoke(oRfcDestination);
            outputParameters = SetOutputParamters(IRfLock);
            return outputParameters;
        }

        public IRfcFunction ExecuteSAPReaderIRFC(string functionName, List<InputParamters> inputParamters)
        {
            List<ReturnValue> outputParameters = new List<ReturnValue>();
            RfcDestination oRfcDestination = GetConnection();
            IRfcFunction IRfLock = oRfcDestination.Repository.CreateFunction(functionName);
            SetInputParamters(IRfLock, inputParamters);
            IRfLock.Invoke(oRfcDestination);

            //outputParameters = SetOutputParamters(IRfLock);
            return IRfLock;
        }


        #region  Input
        protected void SetInputParamters(IRfcFunction IRfLock, List<InputParamters> inputParamters)
        {
            foreach (InputParamters paramter in inputParamters)
            {
                switch (paramter.ParameterType)
                {
                    case InputParamterCharacter:
                        IRfLock.SetValue(paramter.ParameterName, paramter.ParameterValue);
                        break;
                    case InputParamterTable:
                        SetTableTypeInput(IRfLock, paramter);
                        break;
                    case InputParamterStructure:
                        SetStructureTypeInput(IRfLock, paramter);
                        break;
                }
            }

        }

        protected void SetTableTypeInput(IRfcFunction IRfLock, InputParamters paramter)
        {
            DataTable dt = (DataTable)paramter.ParameterValue;
            IRfcTable sapTable = (IRfcTable)IRfLock[paramter.ParameterName];
            foreach (DataRow dr in dt.Rows)
            {
                sapTable.Append();
                foreach (DataColumn dc in dt.Columns)
                {
                    sapTable.SetValue(dc.ColumnName, Convert.ToString(dr[dc.ColumnName]));
                }
            }

        }

        protected void SetStructureTypeInput(IRfcFunction IRfLock, InputParamters paramter)
        {
            DataTable dt = (DataTable)paramter.ParameterValue;
            IRfcStructure sapStructure = (IRfcStructure)IRfLock[paramter.ParameterName];
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    sapStructure.SetValue(dc.ColumnName, Convert.ToString(dr[dc.ColumnName]));
                }
            }

        }
        #endregion

        #region  Output

        protected List<ReturnValue> SetOutputParamters(IRfcFunction IRfLock)
        {
            List<ReturnValue> outputParameters = new List<ReturnValue>();
            foreach (IRfcParameter sapOutputParamter in IRfLock)
            {
                if (sapOutputParamter.Metadata.Direction == RfcDirection.CHANGING || sapOutputParamter.Metadata.Direction == RfcDirection.EXPORT
                                                                                    || sapOutputParamter.Metadata.Direction == RfcDirection.TABLES)
                {
                    ReturnValue outputParamter = new ReturnValue();
                    if (sapOutputParamter.Metadata.DataType == RfcDataType.TABLE || sapOutputParamter.Metadata.DataType == RfcDataType.STRUCTURE)
                    {
                        #region   Tables
                        DataTable dt = new DataTable();
                        if (sapOutputParamter.Metadata.DataType == RfcDataType.TABLE)
                        {
                            dt = IRfcTableToDataTable(sapOutputParamter.GetTable());
                        }
                        else if (sapOutputParamter.Metadata.DataType == RfcDataType.STRUCTURE)
                        {
                            dt = IRfcStructureToDataTable(sapOutputParamter.GetStructure());
                        }
                        outputParamter.ParameterName = sapOutputParamter.Metadata.Name;
                        outputParamter.ParameterType = "TABLE";
                        outputParamter.ParameterTableValue = dt;
                        #endregion
                    }
                    else
                    {
                        outputParamter.ParameterName = sapOutputParamter.Metadata.Name;
                        outputParamter.ParameterCharValue = Convert.ToString(sapOutputParamter.GetValue());
                    }
                    outputParameters.Add(outputParamter);
                }
            }

            return outputParameters;
        }

        protected DataTable IRfcTableToDataTable(IRfcTable sapTable)
        {
            DataTable dt = new DataTable();
            dt.TableName = sapTable.Metadata.Name;
            if (sapTable.Count > 0)
            {
                foreach (IRfcField fld in sapTable[0])
                {
                    dt.Columns.Add(fld.Metadata.Name);
                }

                foreach (IRfcStructure row in sapTable)
                {
                    DataRow dr = dt.NewRow();
                    foreach (IRfcField fld in sapTable[0])
                    {
                        dr[fld.Metadata.Name] = Convert.ToString(row.GetValue(fld.Metadata.Name));
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        protected DataTable IRfcStructureToDataTable(IRfcStructure sapStructure)
        {
            DataTable dt = new DataTable();
            if (sapStructure.Count > 0)
            {
                foreach (IRfcField fld in sapStructure)
                {
                    dt.Columns.Add(fld.Metadata.Name);
                }

                DataRow dr = dt.NewRow();
                foreach (IRfcField fld in sapStructure)
                {
                    dr[fld.Metadata.Name] = Convert.ToString(sapStructure.GetValue(fld.Metadata.Name));
                }
                dt.Rows.Add(dr);

            }
            return dt;
        }
        #endregion

        #region  Utility
        public RfcDestination GetConnection()
        {
            string companyCode = "XYZ";
            RfcRepository RfcRepository;
            RfcDestination oRfcDestination;
            oRfcDestination = RfcDestinationManager.TryGetDestination(companyCode);

            SAPConfiguration oSapConfiguration = new SAPConfiguration();

            //1: Create and register SAP configuration
            if (oRfcDestination == null)
            {
                try
                {
                    RfcDestinationManager.RegisterDestinationConfiguration(oSapConfiguration);
                }
                catch (Exception ex)
                { }
                oRfcDestination = RfcDestinationManager.GetDestination(companyCode);
            }

            //3: Get repository
            RfcRepository = oRfcDestination.Repository;

            return oRfcDestination;
        }

        public static DataTable GetTableByName(string tableName, List<ReturnValue> rtValueList)
        {
            DataTable dt = new DataTable();

            foreach (ReturnValue rtv in rtValueList)
            {
                if (rtv.ParameterName == tableName)
                {
                    dt = rtv.ParameterTableValue;
                }
            }

            return dt;

        }

        #endregion


    }

    #region Properties

    public class InputParamters
    {
        public string ParameterType { get; set; }
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }

        public InputParamters(string paramType, string paramName, object paramValue)
        {
            ParameterType = paramType;
            ParameterName = paramName;
            ParameterValue = paramValue;
        }
    }

    public class ReturnValue
    {
        public string ParameterType { get; set; }
        public string ParameterName { get; set; }
        public DataTable ParameterTableValue { get; set; }
        public string ParameterCharValue { get; set; }
    }

    public class TableOrStructureType
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public object ColumnValue { get; set; }
    }

    #endregion
}
