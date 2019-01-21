using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X12translator
{
    public static class X12
    {
        public static DataTable _dtX12 = new DataTable();
        public static string _ReturnMessage;

        public static void TranslateX12(string[] x12DocInfo, string EOL, char delimiter)
        {
            try
            {

                //loop through the x12 array
                foreach (string x12Line in x12DocInfo)
                {
                    //split it into seperate array
                    string[] x12LineSplitarr = x12Line.Split(delimiter);

                    //adjust dt
                    AdjustDataTable(x12LineSplitarr.Length);

                    //insert split info into datatable
                    InsertInfoIntoDataTable(x12LineSplitarr);

                }


                //turn into object list

                ////for example code #1
                //DataTable table = dt;
                //var dynamicTable = table.AsDynamicEnumerable();

                //var firstRowsNameField = dynamicTable.First().Name;

                ////for below example code #2
                //List<dynamic> dynamicDt = dt.ToDynamic();
                //Console.WriteLine(dynamicDt.First().ID);
                //Console.WriteLine(dynamicDt.First().Name);
            }
            catch(CustomException ex)
            {
                //return error
                _ReturnMessage = ex.Message.ToString();
            }
        }


        private static void InsertInfoIntoDataTable(string[] p_x12LineSplitarr)
        {
            //assign columns to dt by amount of segments in 
            string columnName = "SEGMENT";
            int columnIndex = 0;

            //now loop through and assign value
            foreach (string x12LineSplit in p_x12LineSplitarr)
            {
                DataRow drX12 = _dtX12.NewRow();
                string columnNameIndex = Convert.ToString(columnName + columnIndex).Trim();

                //check to see if column exists
                if (ContainColumn(columnNameIndex))
                {
                    drX12[columnNameIndex] = x12LineSplit.ToString();
                }

                //add to dt
                _dtX12.Rows.Add(drX12);

                //increment index number
                columnIndex++;
            }
        }


        private static void AdjustDataTable(int arrLength)
        {
            //assign columns to dt by amount of segments in 
            string columnName = "SEGMENT";
            int columnNameIndex = 0;

            for (int i = 1; i <= arrLength; i++)
            {
                //check to see if column exists, if not, create it
                if (!ContainColumn(Convert.ToString(columnName + columnNameIndex).Trim()))
                {
                    _dtX12.Columns.Add(Convert.ToString(columnName + columnNameIndex).Trim(), typeof(string));
                }

                columnNameIndex++;               
            }
        }


        private static bool ContainColumn(string columnName)
        {
            bool returnBool = false;

            DataColumnCollection columns = _dtX12.Columns;
            if (columns.Contains(columnName))
            {
                returnBool = true;
            }

            return returnBool;
        }
    


        /// <summary>
        /// below translates the datatable into object dynamically
        /// </summary>
        /// 

        #region example code #1

        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }


        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) { _row = row; }

            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }
        }

        #endregion


        #region example code #2

        public static List<dynamic> ToDynamic(this DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return dynamicDt;
        }

        #endregion
    }
}
