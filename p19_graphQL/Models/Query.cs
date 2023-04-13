public class Query
{
    public Task<List<Movie>> GetMovies([Service]Repository repository) => repository.GetMoviesAsync();

    public Task<List<Director>> GetDirectors([Service]Repository repository) => repository.GetDirectorsAsync();

    public Task<Movie> GetMovieByName(string name, [Service]Repository repository) => repository.GetMovieAsync(name);
    
    public Task<Director> GetDirectorByName(string name, [Service]Repository repository) => repository.GetDirectorAsync(name);
}