namespace WEBSERVERGAMESTORE.Dtos;

    public record UpdatedGame(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate);
