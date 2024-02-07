

using testGameStore.client.Models;




namespace testGameStore.client;

public static class GameClient
{
     private static readonly List<Game>  games = new()
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

    public static Game[] GetGames()
    {
        return games.ToArray();
    }

    public static void AddGame(Game game)
    {
        game.Id = games.Count + 1;
        games.Add(game);
    }

    public static Game GetGame(int id)
    {
        return games.Find(arg => arg.Id == id) ?? throw new Exception("Could not find Game");
    }

    public static void EditGame(Game singleGame)
    {
        Game gametoEdit = GetGame(singleGame.Id);

        gametoEdit.Name = singleGame.Name;
        gametoEdit.Price = singleGame.Price;
        gametoEdit.Genre = singleGame.Genre;
        gametoEdit.ReleaseDate = singleGame.ReleaseDate;
    }

    public static void DeleteGame(int id)
    {

        Game gametoDelete = GetGame(id);

        games.Remove(gametoDelete);

    }
}