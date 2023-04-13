public class Repository
{
    public long GetCurrentMovieId() => _movies.Last().Id + 1;
    public long GetCurrentDirectorId() => _directors.Last().Id + 1;

    private static List<Movie> _movies;

    private static List<Director> _directors;

    public Task<List<Movie>> GetMoviesAsync()
    {
        return Task.FromResult(_movies);
    }

    public Task<List<Director>> GetDirectorsAsync()
    {
        return Task.FromResult(_directors);
    }

    public Task<Movie> GetMovieAsync(string name)
    {
        return Task.FromResult(_movies.FirstOrDefault(m => m.Name == name));
    }

    public Task<Director> GetDirectorAsync(string name)
    {
        return Task.FromResult(_directors.FirstOrDefault(m => m.Name == name));
    }
    
    public async Task AddDirector(Director director)
    {
        _directors.Add(director);
    }
    
    public async Task AddMovie(Movie movie)
    {
        _movies.Add(movie);
    }

    public static void Initialise()
    {
        _directors = new()
        {
            new Director(1, "James Cameron", 55)
        };
        
        _movies = new()
        {
            new Movie(1, "Avatar", "Action", "...", _directors.FirstOrDefault())
        };
    }
}