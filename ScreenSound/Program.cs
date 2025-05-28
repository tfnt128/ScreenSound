using ScreenSound.Data;
using ScreenSound.Menus;
using ScreenSound.Modelos;

try
{
    var context = new ScreenSoundContext();
    var artistDal = new ArtistDal(context);

    var newArtista = new Artista("Legião Urbana", "Banda smzinho") { Id = 4002 };
    //artistDal.UpdateArtist(newArtista);
    //artistDal.AddArtist(newArtista);
    //artistDal.DeleteArtist(newArtista);
    //artistDal.AddArtist(new Artista("Cocoa", "cuzinho2"));
    //artistDal.AddArtist(new Artista("Babilonia", "smzinho", 1));
    //artistDal.DeleteArtist(new Artista("a", "a", 2013));
    //artistDal.DeleteArtist(new Artista("a", "a", 2014));
    //artistDal.DeleteArtist(new Artista("a", "a", 2015));
    //artistDal.UpdateArtist(new Artista("Babiloncio", "Amigo Azul", 2015));
    //artistDal.UpdateArtist(new Artista("Legião Urbana", "cajsklajkls"));

    foreach (var artista in artistDal.Listar())
    {
        Console.WriteLine(artista);
    }
    //foreach (var artista in artistDal.RecuperarPeloNome("Legião Urbana"))
    //{
    //    Console.WriteLine(artista);
    //}

}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
    return;
}

Artista ira = new Artista("Ira!", "Banda Ira!");
Artista beatles = new("The Beatles", "Banda The Beatles");

Dictionary<string, Artista> artistasRegistrados = new();
artistasRegistrados.Add(ira.Nome, ira);
artistasRegistrados.Add(beatles.Nome, beatles);

Dictionary<int, Menu> opcoes = new();
opcoes.Add(1, new MenuRegistrarArtista());
opcoes.Add(2, new MenuRegistrarMusica());
opcoes.Add(3, new MenuMostrarArtistas());
opcoes.Add(4, new MenuMostrarMusicas());
opcoes.Add(-1, new MenuSair());

void ExibirLogo()
{
    Console.WriteLine(@"

░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
");
    Console.WriteLine("Boas vindas ao Screen Sound 3.0!");
}

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para registrar um artista");
    Console.WriteLine("Digite 2 para registrar a música de um artista");
    Console.WriteLine("Digite 3 para mostrar todos os artistas");
    Console.WriteLine("Digite 4 para exibir todas as músicas de um artista");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
    {
        Menu menuASerExibido = opcoes[opcaoEscolhidaNumerica];
        menuASerExibido.Executar(artistasRegistrados);
        if (opcaoEscolhidaNumerica > 0) ExibirOpcoesDoMenu();
    } 
    else
    {
        Console.WriteLine("Opção inválida");
    }
}

ExibirOpcoesDoMenu();