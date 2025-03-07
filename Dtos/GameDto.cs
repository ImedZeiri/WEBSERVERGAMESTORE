namespace WEBSERVERGAMESTORE.Dtos;

    public record GameDto(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate);
