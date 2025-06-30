using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(DAL<Artista> artistDal)
    {
        base.Executar(artistDal);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaRecuperado = artistDal.Recuperar(a => a.Nome.Equals(nomeDoArtista));
        if (artistaRecuperado is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            Console.Write("Agora digite o ano de lançamento da música: ");
            string releaseYear = Console.ReadLine()!;
            //artistaRecuperado.AdicionarMusica(new Musica(tituloDaMusica) {ReleaseYear = Convert.ToInt32(releaseYear)});
            Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");
            artistDal.Update(artistaRecuperado);
            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
