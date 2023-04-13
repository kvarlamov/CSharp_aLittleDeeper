namespace p19_graphQL.Models;

public class Mutation
{
    public async Task<DirectorPayload> AddDirector(DirectorInput input, [Service] Repository repository)
    {
        var id = repository.GetCurrentDirectorId();            
        var director = new Director(id, input.name, input.Age);
        await repository.AddDirector(director);
        return new DirectorPayload(director);
    }

    public async Task<MoviePayload> AddMovie(MovieInput input, [Service] Repository repository)
    {
        var id = repository.GetCurrentMovieId();
        var director = await repository.GetDirectorAsync(input.Director) ?? 
                       throw new Exception("Director not found");
        var movie = new Movie(id, input.Name, input.Genre, input.Description, director);
        await repository.AddMovie(movie);
        return new MoviePayload(movie);
    }    
}

public record MoviePayload(Movie? record, string? error = null);
public record MovieInput(string Name, string Genre, string Description, string Director);
public record DirectorPayload(Director record);
public record DirectorInput(string name, int Age);