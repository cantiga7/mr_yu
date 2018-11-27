using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Data;

using System.Data.SqlClient;
//using System.Windows.Forms;


 
public static class clsSQLClientFunctions
{
    public static string SAPConnection;

    public static string GlobalConnectionString(string _ServerName,
                                                string _DatabaseName,
                                                string _UserName,
                                                string _Password
                                                )
    {
        return @"Data Source=" + _ServerName + ";Initial Catalog=" + _DatabaseName + ";Persist Security Info=True;User ID=" + _UserName + ";Password=" + _Password;
    }
    public static string LocalConnectionString(string _ServerName,
                                             string _lclDatabaseName,
                                             string _UserName,
                                             string _Password
                                             )
    {
        return @"Data Source=" + _ServerName + ";Initial Catalog=" + _lclDatabaseName + ";Persist Security Info=True;User ID=" + _UserName + ";Password=" + _Password;
    }
        
    /// <summary>
    /// Checking of Connection Status
    /// </summary>
    /// <param name="valConnectionString">Connection String</param>
    /// <returns></returns>
    public static Boolean CheckConnection(string valConnectionString)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(valConnectionString))
            {
                connection.Open();
                return true;
            }
        }

        catch
        {
            //MessageBox.Show(e.Message, "GlobalConnectionString Error");
            return false;
        }

    }
    /// Data Display
    /// </summary>
    /// <param name="valConnectionString">Connection String</param>
    /// <param name="valSQLSyntax">SQL Query</param>
    /// <returns></returns>
    public static DataTable DataList(string valConnectionString, string valSQLSyntax)
    {
        DataTable _dt = new DataTable();
        _dt.Clear();
        try
        {
            using (SqlConnection connection = new SqlConnection(valConnectionString))
            {

                SqlCommand _Command = new SqlCommand(valSQLSyntax, connection);
                _Command.CommandTimeout = 240;
                SqlDataAdapter _da = new SqlDataAdapter(_Command);

                connection.Open();
                _da.Fill(_dt);
                connection.Close();

                return _dt;
            }
        }

        catch (Exception e)
        {
            MessageBox.Show(e.Message + " : " + valSQLSyntax, "DataList Error");
            return _dt;
        }

    }
   
    /// <summary>
    /// Global Command Execute for Non Queries Returns
    /// </summary>
    /// <param name="valConnectionString">Connection String</param>
    /// <param name="valSQLSyntax">SQL Query</param>
    /// <returns>Boolean</returns>
    /// <remarks>This will handle the execution for Non Queries Syntax Like Update, Delete, Insert, Etc.</remarks>
    public static Boolean GlobalExecuteCommand(string valConnectionString, string valSQLSyntax)
    {
        Boolean _Bool = false;
        try
        {
            using (SqlConnection connection = new SqlConnection(valConnectionString))
            {
                SqlCommand _Command;
                _Command = connection.CreateCommand();
                connection.Open();
                _Command.CommandText = valSQLSyntax;
                _Command.ExecuteNonQuery();
                _Bool = true;
                connection.Close();
            }
        }

        catch (Exception e)
        {
            MessageBox.Show(e.Message, "GlobalExecuteCommand Error");
        }
        return _Bool;

    }


    /// <summary>
    /// To identify the returning value of teh record set
    /// </summary>
    /// <param name="_DataTable">Data Table</param>
    /// <param name="_FieldName">Returning Field</param>
    /// <param name="_Datatype">0 - String , 1 - Numeric</param>
    /// <returns></returns>

    public static string GetData(DataTable _DataTable, string _FieldName, String _Datatype)
    {
        string _ReturnData;
        try
        {
            _ReturnData = _DataTable.Rows[0][_FieldName].ToString();
        }

        catch (Exception e)
        {
            //MessageBox.Show(e.Message, "Error In Connection");

            switch(_Datatype.ToString())
            {
                case "0":
                    _ReturnData = "";
                    break;
                case "1":
                    _ReturnData = "0";
                    break;
                default:
                    _ReturnData = e.Message;
                    break;
            }
        }
        return _ReturnData;
    }
    
}
