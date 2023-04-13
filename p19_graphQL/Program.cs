using p19_graphQL.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<Repository>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL();

Repository.Initialise();

app.Run();