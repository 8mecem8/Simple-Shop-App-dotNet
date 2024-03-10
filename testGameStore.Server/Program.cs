using testGameStore.Server.Models;

List<Game> games = new()
{
    new Game()
    {
        Id = 1,
        Name = "Street Fighter",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991, 2, 1)
    },
    new Game()
    {
        Id = 2,
        Name = "Final Fantasy XIV",
        Genre = "Role-playing",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010, 9, 30)
    },
    new Game()
    {
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateTime(2022, 9, 27)
    },
};



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



/*----Routes/Endpoints Groups----*/

var games_group = app.MapGroup("/games")
                        .WithParameterValidation();

/*----Routes/Endpoints Groups----*/




/* ---------------------------------------------------------------- Routes/Endpoints ---------------------------------------------------------------- */

/**
* @route:       /
* @desc:        Main Page for server
* @parameters:  N/A
* @access:      Public
*/
app.MapGet("/", () => 
{
    var htmlContent = @$"<!doctype html>
    <html>
        <head><title>ASP.net Server</title></head>
        <body>
            <h1>Welcome to Our Server</h1>
            <p style='font-size: 10px'>The time on the server is {DateTime.Now:O}</p>
        </body>
    </html>";
    
    return Results.Content(htmlContent, "text/html");
});


/**
* @route:       /games
* @desc:        Get  the all games list 
* @parameters:  N/A
* @access:      Public
*/
games_group.MapGet("/", () => games);


/**
* @route:       /games/{id}
* @desc:        Get one spesific game item 
* @parameters:  int id
* @access:      Public
*/
games_group.MapGet("/{id}", (int id) =>
{ 
   Game? filteredGame = games.Find(arg => arg.Id == id);

   if(filteredGame is null)
   {
        return Results.NotFound();
   }
    
    return Results.Ok(filteredGame);
}).WithName("getGameById");


/**
* @route:       /games
* @desc:        Post a new game to the games List
* @parameters:  game
* @access:      Public
*/
games_group.MapPost("/", (Game game) => 
{
    game.Id = games.Count + 1;
    games.Add(game);

    return Results.CreatedAtRoute("getGameById",new {id = game.Id},game);
});


/**
* @route:       /games
* @desc:        Put the new data to selected item in the games list
* @parameters:  id, updatedGame
* @access:      Public
*/
games_group.MapPut("/{id}",(int id, Game updatedGame)=>
{
    Game? currentGame = games.Find((arg)=>{ return arg.Id == id;});

    if(currentGame is null)
    {
        updatedGame.Id = id;
        games.Add(updatedGame);
        return Results.CreatedAtRoute("getGameById", new{ id = updatedGame.Id}, updatedGame);
    }

        currentGame.Name = updatedGame.Name;
        currentGame.Genre = updatedGame.Genre;
        currentGame.Price = updatedGame.Price;
        currentGame.ReleaseDate = updatedGame.ReleaseDate;
        return Results.NoContent();
});



games_group.MapDelete("/{id}",(int id)=>
{
    Game? selectedGame = games.Find((arg)=>{ return arg.Id == id;});

    if(selectedGame is null)
    {
        return Results.NotFound();
    }

    games.Remove(selectedGame);

    return Results.Text($"Delete operation of the item NAME:{selectedGame.Name} Done");
    
});

/* ---------------------------------------------------------------- Routes/Endpoints ---------------------------------------------------------------- */
app.Run();
