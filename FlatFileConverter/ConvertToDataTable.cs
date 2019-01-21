using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileConverter
{
    internal static class ConvertToDataTable
    {
        internal static DataTable convertToDataTable(string[] DocInfo, string EOL, char delimiter)
        {
            DataTable dtReturn = new DataTable();

            try
            {

                //loop through the x12 array
                foreach (string Line in DocInfo)
                {
                    //split it into seperate array
                    string[] LineSplitarr = Line.Split(delimiter);

                    //adjust dt
                    AdjustDataTable(LineSplitarr.Length, ref dtReturn);

                    //insert split info into datatable
                    InsertInfoIntoDataTable(LineSplitarr, ref dtReturn);

                }
            }
            catch (ConvertException ex)
            {
                //return error
                throw ex;
            }

            return dtReturn;
        }

        private static void InsertInfoIntoDataTable(string[] p_x12LineSplitarr, ref DataTable dtReturn)
        {
            //assign columns to dt by amount of segments in 
            string columnName = "SEGMENT";
            int columnIndex = 0;

            //now loop through and assign value
            foreach (string x12LineSplit in p_x12LineSplitarr)
            {
                DataRow drX12 = dtReturn.NewRow();
                string columnNameIndex = Convert.ToString(columnName + columnIndex).Trim();

                //check to see if column exists
                if (ContainColumn(columnNameIndex, ref dtReturn))
                {
                    drX12[columnNameIndex] = x12LineSplit.ToString();
                }

                //add to dt
                dtReturn.Rows.Add(drX12);

                //increment index number
                columnIndex++;
            }
        }


        private static void AdjustDataTable(int arrLength, ref DataTable dtReturn)
        {
            //assign columns to dt by amount of segments in 
            string columnName = "SEGMENT";
            int columnNameIndex = 0;

            for (int i = 1; i <= arrLength; i++)
            {
                //check to see if column exists, if not, create it
                if (!ContainColumn(Convert.ToString(columnName + columnNameIndex).Trim(), ref dtReturn))
                {
                    dtReturn.Columns.Add(Convert.ToString(columnName + columnNameIndex).Trim(), typeof(string));
                }

                columnNameIndex++;
            }
        }


        private static bool ContainColumn(string columnName, ref DataTable dtReturn)
        {
            bool returnBool = false;

            DataColumnCollection columns = dtReturn.Columns;
            if (columns.Contains(columnName))
            {
                returnBool = true;
            }

            return returnBool;
        }
    }
}
