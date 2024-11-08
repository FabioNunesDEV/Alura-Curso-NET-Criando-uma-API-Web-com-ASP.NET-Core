using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointMusicas (this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(musica);

            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
            {
                dal.Adicionar(musica);
                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) => {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(musica);
                return Results.NoContent();

            });

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) => {

                var musicaAAtualizar = dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id);

                if (musicaAAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAAtualizar.Nome = musicaRequestEdit.nome;
                musicaAAtualizar.AnoLancamento = musicaRequestEdit.anoLancamento;

                dal.Atualizar(musicaAAtualizar);
                return Results.Ok();
            });
        }
    }
}
