namespace ScreenSound.API.Requests
{
    public record MusicaRequestEdit : MusicaRequest
    {
        public int Id { get; init; }

        public MusicaRequestEdit(int id, string nome, int artistaId, int releaseYear)
            : base(nome, artistaId, releaseYear)
        {
            Id = id;
        }
    }
}