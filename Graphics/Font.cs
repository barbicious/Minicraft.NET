using Minicraft.NET.Math;
using Minicraft.NET.Registry;

namespace Minicraft.NET.Graphics;

public static class Font
{
    private const int CharWidth = 8;
    private const int CharHeight = 8;

    private const int CharRows = 4;
    private const int CharCols = 16;
    
    private static readonly char[] Characters =
    [
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
        'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '/', '<', '>', ':', '+', '1', '2',
        '3', '4', '5', '6', '7', '8', '9', '0', '&', '=', '(', ')', '.', '?', ' '
    ];

    private static readonly int Texture = TextureRegistry.Instance["Font"];
    
    public static void Blit(Renderer renderer, char character, int x, int y, int? foreground, int? background)
    {
        for (int cy = 0; cy < CharRows; cy++)
        {
            for (int cx = 0; cx < CharCols; cx++)
            {
                int index = cy * CharCols + cx;

                if (index >= Characters.Length) break;

                if (character != Characters[cy * CharCols + cx]) continue;

                renderer.Blit(TextureRegistry.Instance[Texture],
                    new Rect<int> { X = cx * CharWidth, Y = cy * CharHeight, W = CharWidth, H = CharHeight }, new Point<int> { X = x, Y = y },
                    [foreground, null, null, background]);
            }
        }
    }

    public static void Blit(Renderer renderer, string str, int x, int y, int? foreground, int? background)
    {
        str = str.ToUpper();
        
        foreach (char c in str)
        {
            Blit(renderer, c, x, y, foreground, background);
            x += CharWidth;
        }
    }
}