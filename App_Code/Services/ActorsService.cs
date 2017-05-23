using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
public class ActorsService
{
    protected OleDbConnection myConn;
    OleDbParameter objParam;

    public ActorsService()
    {
        myConn = new OleDbConnection(ConnectIMdb.getConnectionString());

    }

    public void InsertActor(ActorsDetails actor)
    {
        string name = actor.Name;

        OleDbCommand myCmd = new OleDbCommand("ActorInsInto", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@actorName", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = name;


        try
        {
            myConn.Open();
            myCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            myConn.Close();
        }

    }

    public int GetIDbyName(string name)
    {
        int id;

        OleDbCommand myCmd = new OleDbCommand("GetActorByName", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@actorName", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = name;

        try
        {
            myConn.Open();
            OleDbDataReader DataReader = myCmd.ExecuteReader();

            if (DataReader.Read())
            {
                id = (int)DataReader["ActorID"];
            }
            else
            {
                id = -1;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            myConn.Close();
        }
        return id;
    }

    public void InsertActorInMovie(int movieID, int actorID)
    {

        OleDbCommand myCmd = new OleDbCommand("InsActorsInMovie", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@movieID", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = movieID;

        objParam = myCmd.Parameters.Add("@actorID", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = actorID;

        try
        {
            myConn.Open();
            myCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            myConn.Close();
        }
    }

    public DataSet GetActors()
    {
        OleDbCommand myCmd = new OleDbCommand("GetAllActors", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        OleDbDataAdapter adapter = new OleDbDataAdapter();
        adapter.SelectCommand = myCmd;

        DataSet ActorsTable = new DataSet();

        try
        {
            adapter.Fill(ActorsTable, "Actors");
            ActorsTable.Tables["Actors"].PrimaryKey = new DataColumn[] { ActorsTable.Tables["Actors"].Columns["ActorID"] };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ActorsTable;
    }

    public string[] ActorsInMovie(int movieID)
    {
        DataSet Actors = new DataSet();
        OleDbCommand myCmd = new OleDbCommand("GetActorsInMovie", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@movieID", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = movieID;

        OleDbDataAdapter adapter = new OleDbDataAdapter();
        adapter.SelectCommand = myCmd;


        try
        {
            myConn.Open();

            adapter.Fill(Actors, "Actors");
            Actors.Tables["Actors"].PrimaryKey = new DataColumn[] { Actors.Tables["Actors"].Columns["ActorName"] };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            myConn.Close();
        }
        string[] actorArr = new string[Actors.Tables["Actors"].Rows.Count];
        for (int i = 0; i < Actors.Tables["Actors"].Rows.Count; i++)
        {
            actorArr[i] = Actors.Tables["Actors"].Rows[i][0].ToString();
        }
        return actorArr;
    }
}
