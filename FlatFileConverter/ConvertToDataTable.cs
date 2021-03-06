﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileConverter
{
    internal static class ConvertToDataTable
    {
        internal static DataTable convertToDataTable(string contents, char EOL, char segmentDiv, bool firstLineIsHeader)
        {
            DataTable dt = new DataTable();

            try
            {
                //break the file into seperate lines
                string[] contentSplit = contents.Split(EOL);

                //create datatable
                if (firstLineIsHeader)
                {
                    dt = CreateFlatFileDataTable(dt, contentSplit[0], segmentDiv);
                }
                else
                {
                    dt = CreateDataTable(dt, contentSplit, segmentDiv);
                }

                //add line number for when the first line of the file is column names
                int lineNo = 0;

                //loop through each line and fill in the segments
                foreach (string drContent in contentSplit)
                {
                    //increment line number
                    lineNo++;

                    if ((lineNo > 1 && firstLineIsHeader) | (!firstLineIsHeader))
                    {
                        int index = 0;

                        //get columns amount by splitting the line
                        string[] segmentSplit = drContent.Split(segmentDiv);

                        DataRow dr = dt.NewRow();

                        //loop through each segment and assign to datarow
                        foreach (string segment in segmentSplit)
                        {
                            //add segment to datacolumn in datarow
                            dr[index] = segment.Trim();
                            index++;
                        }

                        //add completed datarow to datatable
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        private static DataTable CreateDataTable(DataTable dt, string[] contentSplit, char segmentDiv)
        {
            try
            {
                int contentColumnCount = 0;

                //loop through each line to determine the max amount of columns to create
                foreach (string drContent in contentSplit)
                {
                    //get columns amount by splitting the line
                    string[] segmentSplit = drContent.Split(segmentDiv);

                    //check to see if there are more columns, if so, update the total column amount
                    if (segmentSplit.Count() > contentColumnCount)
                    {
                        contentColumnCount = segmentSplit.Count();
                    }
                }

                //create columns
                for (int i = 1; i <= contentColumnCount; i++)
                {
                    //create column and name the segment "SEGMENT01" (padded left by 2 with zeros)
                    DataColumn dcSegment = new DataColumn("SEGMENT" + Convert.ToString(i).PadLeft(2, '0'), typeof(string));

                    dt.Columns.Add(dcSegment);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        private static DataTable CreateFlatFileDataTable(DataTable dt, string contentSplit, char segmentDiv)
        {
            try
            {
                //this is the first row (header), split to get column names
                string[] segmentSplit = contentSplit.Split(segmentDiv);

                //loop through each segment to get column names for creation
                foreach (string drsegment in segmentSplit)
                {
                    //create columns
                    DataColumn dcSegment = new DataColumn(drsegment, typeof(string));

                    dt.Columns.Add(dcSegment);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
    }
}
