namespace ScreenSound.API.Requests;

public record MusicaRequestEdit(int Id, string nome, int artistaId, int anoLancamento)
    : MusicaRequest(nome, artistaId, anoLancamento);