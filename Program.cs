using WEBSERVERGAMESTORE.Dtos;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GET_GAME_ENDPOINT_NAME = "GetGame";

List<GameDto> games = new List<GameDto>
{
    new GameDto(
        1,
        "Street Fighter II",
        "Fighting",
        11.99M,
        new DateOnly(1992, 7, 15)),
    new GameDto(
        2,
        "The Legend of Zelda: Breath of the Wild",
        "Action-Adventure",
        59.99M,
        new DateOnly(2017, 3, 3)),
    new GameDto(
        3,
        "Minecraft",
        "Sandbox",
        26.95M,
        new DateOnly(2011, 11, 18)),
    new GameDto(
        4,
        "Cyberpunk 2077",
        "Action RPG",
        49.99M,
        new DateOnly(2020, 12, 10)),
    new GameDto(
        5,
        "Super Mario Odyssey",
        "Platform",
        39.99M,
        new DateOnly(2017, 10, 27))
};

// Get all games
app.MapGet("/games", () => games);

// Get game by id
app.MapGet("/games/{id}", (int id) => 
{
    GameDto? game = games.Find(game => game.Id == id);
    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GET_GAME_ENDPOINT_NAME);

// POST game
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GET_GAME_ENDPOINT_NAME, new { id = game.Id }, game);
});

// PUT game
app.MapPut("/games/{id}", (int id, UpdatedGame updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent(); // Return 204 No Content for successful update
});

// DELETE game
app.MapDelete("/games/{id}", (int id) =>
{
    var removedCount = games.RemoveAll(game => game.Id == id);

    if (removedCount == 0)
    {
        return Results.NotFound();
    }

    return Results.NoContent(); // Return 204 No Content for successful delete
});

app.Run();