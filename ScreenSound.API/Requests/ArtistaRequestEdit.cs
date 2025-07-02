namespace ScreenSound.API.Requests
{
    public record ArtistaRequestEdit : ArtistaRequest
    {
        public int Id { get; init; }

        public ArtistaRequestEdit(int id, string nome, string bio)
            : base(nome, bio)
        {
            Id = id;
        }
    }
}
