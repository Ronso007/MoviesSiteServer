using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

public class MoviesService
{
    protected OleDbConnection myConn;
    OleDbParameter objParam;

    public MoviesService()
    {
        myConn = new OleDbConnection(ConnectIMdb.getConnectionString());
    }

    public void InsertMovie(MoviesDetails movie)//מכניס סרט למאגר
    {
        string Name = movie.MovieName;
        string Director = movie.Director;
        string Genre = movie.MovieGenre;
        string Description = movie.Description;
        string Duration = movie.Duration;
        string Image = movie.ImgURL;
        string Trailer = movie.TrailerURL;

        OleDbCommand myCmd = new OleDbCommand("MovieInsInto", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@movieName", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Name;

        objParam = myCmd.Parameters.Add("@director", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Director;

        objParam = myCmd.Parameters.Add("@movieGenre", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Genre;

        objParam = myCmd.Parameters.Add("@description", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Description;

        objParam = myCmd.Parameters.Add("@duration", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Duration;

        objParam = myCmd.Parameters.Add("@Image", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Image;

        objParam = myCmd.Parameters.Add("@trailer", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = Trailer;


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

    public int GetIDbyName(string name)//מקבל שם ומחזיר את האיידי של הסרט
    {
        int id;
        OleDbCommand myCmd = new OleDbCommand("GetMovieByName", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@movieName", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = name;

        try
        {
            myConn.Open();
            OleDbDataReader DataReader = myCmd.ExecuteReader();
            if (DataReader.Read())
            {
                id = (int)DataReader["MovieID"];
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

    public MoviesDetails GetMovieByID(int id) //מקבל איידי ומחזיר סרט
    {
        MoviesDetails movie = new MoviesDetails();

        OleDbCommand myCmd = new OleDbCommand("GetMovieByID", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        objParam = myCmd.Parameters.Add("@movieID", OleDbType.BSTR);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = id;

        try
        {
            myConn.Open();
            OleDbDataReader DataReader = myCmd.ExecuteReader();


            if (DataReader.Read())
            {
                movie.MovieName = (string)DataReader["MovieName"];
                movie.Director = (string)DataReader["Director"];
                movie.MovieGenre = (string)DataReader["MovieGenre"];
                movie.Description = (string)DataReader["Description"];
                movie.Duration = (string)DataReader["Duration"];
                movie.ImgURL = (string)DataReader["image"];
                movie.TrailerURL = (string)DataReader["Trailer"];
                movie.TotalRating = (int)DataReader["TotalRating"];
                movie.NumberOfUsers = (int)DataReader["NumberOfUsers"];
            }
            else
            {
                movie = null;
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
        return movie;

    }

    public DataSet GetAllMovies() //מחזיר את כל הסרטים
    {
        OleDbCommand myCmd = new OleDbCommand("GetAllMovies", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;

        OleDbDataAdapter adapter = new OleDbDataAdapter();
        adapter.SelectCommand = myCmd;

        DataSet MoviesTable = new DataSet();

        try
        {
            adapter.Fill(MoviesTable, "Movies");
            MoviesTable.Tables["Movies"].PrimaryKey = new DataColumn[] { MoviesTable.Tables["Movies"].Columns["MovieID"] };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return MoviesTable;
    }

    public void UpdateMovieRating(int rating, int movieID, int addUser)//מעדכן את הניקוד של הסרט
    {
        OleDbCommand myCmd = new OleDbCommand("UpdateMovieRating", myConn);
        myCmd.CommandType = CommandType.StoredProcedure;



        objParam = myCmd.Parameters.Add("@rating", OleDbType.Integer);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = rating;

        objParam = myCmd.Parameters.Add("@addUser", OleDbType.Integer);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = addUser;


        objParam = myCmd.Parameters.Add("@movieID", OleDbType.Integer);
        objParam.Direction = ParameterDirection.Input;
        objParam.Value = movieID;

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

    public DataSet GetAllMoviesFiltered(int Expression, int Rating)//מחזיר את הסרטים לאחר חיפוש סרט ע"פ דירוג
    {
        string sqlS = "";

        sqlS = "SELECT * FROM Movies WHERE (TotalRating/NumberOfUsers) ";
        if (Expression == 0)
        {
            sqlS += ">";
        }
        else
            sqlS += ">=";
        sqlS += "" + Rating + " ORDER BY (TotalRating/NumberOfUsers) DESC;";


        OleDbDataAdapter adapter = new OleDbDataAdapter(sqlS, myConn);

        DataSet MoviesTable = new DataSet();

        try
        {
            myConn.Open();
            adapter.Fill(MoviesTable, "Movies");
            MoviesTable.Tables["Movies"].PrimaryKey = new DataColumn[] { MoviesTable.Tables["Movies"].Columns["MovieID"] };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            myConn.Close();
        }
        return MoviesTable;

    }

    public DataSet SearchMovie(string SearchExpression) //מחזיר את הסרטים לאחר חיפוש סרט ע"פ שם
    {
        string sqlS = "";

        sqlS = "SELECT * FROM Movies WHERE MovieName LIKE '%" + SearchExpression + "%';";


        OleDbDataAdapter adapter = new OleDbDataAdapter(sqlS, myConn);

        DataSet MoviesTable = new DataSet();

        try
        {
            myConn.Open();
            adapter.Fill(MoviesTable, "Movies");
            MoviesTable.Tables["Movies"].PrimaryKey = new DataColumn[] { MoviesTable.Tables["Movies"].Columns["MovieID"] };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            myConn.Close();
        }
        return MoviesTable;

    }

}
