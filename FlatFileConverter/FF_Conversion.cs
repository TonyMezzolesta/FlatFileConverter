using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileConverter
{
    public class FF_Conversion
    {

        public static DataTable ConvertFlatFile(string[] DocInfo, string EOL, char delimiter)
        {
            DataTable dtReturn = new DataTable();

            try
            {
                ConvertToDataTable.convertToDataTable(DocInfo, EOL, delimiter);
            }
            catch (ConvertException ex)
            {
                //return error
                throw ex;
            }

            return dtReturn;
        }
    }
}
