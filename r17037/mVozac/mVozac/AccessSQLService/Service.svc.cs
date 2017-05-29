/****************************** Module Header ******************************\
 * Module Name:  Service.cs
 * Project:      CSWindowsStoreAppAccessSQLServer
 * Copyright (c) Microsoft Corporation.
 * 
 * This is the Service class which implements the IService interface. 
 * 
 * This source is subject to the Microsoft Public License.
 * See http://www.microsoft.com/en-us/openness/licenses.aspx#MPL
 * All other rights reserved.
 * 
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System.Data;
using System.Data.SqlClient;

namespace AccessSQLService
{
    public class Service : IService
    {
        /// <summary>
        /// Query data in TestTable
        /// </summary>
        /// <returns></returns>

        SqlConnection sqlCon = new SqlConnection("Data Source=31.147.204.119\\PISERVER,1433;Initial Catalog=17037_DB;User ID=17037_User;Password=***********");
        public DataSet querySql(out bool queryParam)
        {
            try
            {
                sqlCon.Open();
                string strSql = "select Title, Text from TestTable";
                DataSet ds = new DataSet();
                SqlDataAdapter sqlDa = new SqlDataAdapter(strSql, sqlCon);
                sqlDa.Fill(ds);
                queryParam = true;
                return ds;
            }
            catch
            {
                queryParam = false;
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
