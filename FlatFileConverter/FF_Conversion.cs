using Newtonsoft.Json.Linq;
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
        //convert to datatable
        public static DataTable ConvertFlatFileToDataTable(string DocInfo, char EOL, char delimiter, bool firstLineIsHeader)
        {
            DataTable dtReturn = new DataTable();

            try
            {
                dtReturn = ConvertToDataTable.convertToDataTable(DocInfo, EOL, delimiter, firstLineIsHeader);
            }
            catch (ConvertException ex)
            {
                //return error
                throw ex;
            }

            return dtReturn;
        }

        //convert to json string
        public static string ConvertFlatFileToJson(string DocInfo, char EOL, char delimiter, bool firstLineIsHeader)
        {
            string strReturn = string.Empty;

            try
            {
                strReturn = ConvertToJson.convertToJson(DocInfo, EOL, delimiter, firstLineIsHeader);
            }
            catch (ConvertException ex)
            {
                //return error
                throw ex;
            }

            return strReturn;
        }

        //convert to json string
        public static dynamic ConvertFlatFileToObject(string DocInfo, char EOL, char delimiter, bool firstLineIsHeader)
        {
            dynamic returnObj = new JObject();

            try
            {
                returnObj = ConvertToObject.convertToObject(DocInfo, EOL, delimiter, firstLineIsHeader);
            }
            catch (ConvertException ex)
            {
                //return error
                throw ex;
            }

            return returnObj;
        }
    }
}
