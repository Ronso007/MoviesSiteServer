using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MoviesDetails
/// </summary>
public class MoviesDetails
{
    private string movieName;
    private string director;
    private string movieGenre;
    private string description;
    private string duration;
    private string imgURL;
    private string trailerURL;
    private int totalRating;
    private int numberOfUsers;

    public string MovieName
    {
        get
        {
            return movieName;
        }

        set
        {
            movieName = value;
        }
    }

    public string Director
    {
        get
        {
            return director;
        }

        set
        {
            director = value;
        }
    }

    public string MovieGenre
    {
        get
        {
            return movieGenre;
        }

        set
        {
            movieGenre = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public string ImgURL
    {
        get
        {
            return imgURL;
        }

        set
        {
            imgURL = value;
        }
    }

    public string TrailerURL
    {
        get
        {
            return trailerURL;
        }

        set
        {
            trailerURL = value;
        }
    }

    public int TotalRating
    {
        get
        {
            return totalRating;
        }
        set
        {
            totalRating = value;
        }
    }

    public int NumberOfUsers
    {
        get
        {
            return numberOfUsers;
        }
        set
        {
            numberOfUsers = value;
        }
    }
}