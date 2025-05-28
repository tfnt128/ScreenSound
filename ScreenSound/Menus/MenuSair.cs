using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ArtistDal artistDal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
