var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{nome}", (string nome) =>
{
    return Results.Ok($"Bem-vindo ao meu APP {nome}");
});

app.MapGet("/nomecompleto/{nomeCompleto}", (string nomeCompleto) =>
{
    return Results.Ok($"Bem-vindo ao meu APP {nomeCompleto}");
});

app.MapPost("/", (User user) => {
    return Results.Ok(user);
});

app.Run();

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
}
