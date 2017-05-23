using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Connect
/// </summary>
public class ConnectIMdb
{

    const string file_name = "IMdb.mdb";
    public static string getConnectionString()
    {
        string location = HttpContext.Current.Server.MapPath("~/App_Data/" + file_name);
        string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;data source=" + location;
        return ConnectionString;
    }
	
}