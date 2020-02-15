using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GetSpecifiedItemsWebpart.Mapping;

namespace GetSpecifiedItemsWebpart.Helper
{
    public static class Helper
    {
        /// <summary>
        /// helper method to get current time
        /// </summary>
        /// <returns></returns>
        public static double GetCurrentTimestamp() => Convert.ToDouble(DateTime.Now.ToString("yyyyMMddHHmmssffff"));

        /// <summary>
        /// provider to get fields to view
        /// </summary>
        /// <returns></returns>
        public static string GetViewFields() =>
            new StringBuilder()
            .Append($"<FieldRef Name='ID' />")
            .Append($"<FieldRef Name='{COLUMN_EMPLOYEE_ID}' />")
            .Append($"<FieldRef Name='{COLUMN_SOME_OTHER_COLUMN}' />")
            .Append($"<FieldRef Name='{COLUMN_ANOTHER_COLUMN}' />")
            .ToString();

        /// <summary>
        /// helper method to create CAML query string
        /// </summary>
        /// <param name="whereConditionsList">list of items that should be included in where</param>
        /// <param name="type">type which we connect conditions AND/OR</param>
        /// <returns>query string</returns>
        public static string GetQueryString(List<string> whereConditionsList)
        {
            var queryStr = new StringBuilder();

            queryStr.Append("<Where>");

            if (whereConditionsList.Any())
            {
                queryStr.Append("<In>");
                queryStr.Append($"<FieldRef Name='{COLUMN_EMPLOYEE_ID}'/>");
                queryStr.Append("<Values>");
                whereConditionsList.ForEach(conditionValue =>
                {
                    if (conditionValue != "")
                    {
                        queryStr.Append($"<Value Type='Number'>{conditionValue}</Value>");
                    }
                });
                queryStr.Append("</Values>");
                queryStr.Append("</In>");
            }
            queryStr.Append("</Where>");
            queryStr.Append("<OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>");

            return queryStr.ToString();
        }
    }
}
