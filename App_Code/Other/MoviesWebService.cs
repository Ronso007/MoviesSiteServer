using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for MoviesWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MoviesWebService : System.Web.Services.WebService
{

    public MoviesWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public DataSet GetAllMovies()
    {
        MoviesService mov = new MoviesService();
        return mov.GetAllMovies();
    }

    [WebMethod]
    public void InsertMovie(MoviesDetails movie)
    {
        MoviesService mov = new MoviesService();
        mov.InsertMovie(movie);
    }
    [WebMethod]
    public int GetIDbyName(string name)
    {
        MoviesService mov = new MoviesService();
        return mov.GetIDbyName(name);
    }

    [WebMethod]
    public MoviesDetails GetMovieByID(int id)
    {
        MoviesService mov = new MoviesService();
        return mov.GetMovieByID(id);
    }

    [WebMethod]
    public void UpdateMovieRating(int rating, int movieID, int addUser)
    {
        MoviesService mov = new MoviesService();
        mov.UpdateMovieRating(rating, movieID, addUser);
    }

    [WebMethod]
    public DataSet GetAllMoviesFiltered(int Expression, int Rating)
    {
        MoviesService mov = new MoviesService();
        return mov.GetAllMoviesFiltered(Expression, Rating);
    }
    [WebMethod]
    public void InsertActor(ActorsDetails actor)
    {
        ActorsService act = new ActorsService();
        act.InsertActor(actor);
    }

    [WebMethod]
    public int ActorGetIDbyName(string name)
    {
        ActorsService act = new ActorsService();
        return act.GetIDbyName(name);
    }

    [WebMethod]
    public void InsertActorInMovie(int movieID, int actorID)
    {
        ActorsService act = new ActorsService();
        act.InsertActorInMovie(movieID, actorID);
    }

    [WebMethod]
    public DataSet GetActors()
    {
        ActorsService act = new ActorsService();
        return act.GetActors();
    }

    [WebMethod]
    public string[] ActorsInMovie(int movieID)
    {
        ActorsService act = new ActorsService();
        return act.ActorsInMovie(movieID);
    }
    [WebMethod]
    public DataSet SearchMovie(string SearchExpression)
    {
        MoviesService mov = new MoviesService();
        return mov.SearchMovie(SearchExpression);
    }

}


