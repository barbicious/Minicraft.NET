using StbImageSharp;

namespace Minicraft.NET.Graphics;

public sealed class Texture
{
    public const byte TransparentPixel = 255;
    
    private byte[] _pixels;
    
    public int Width { get; }
    public int Height { get; }
    
    public Texture(string path)
    {
        using (var stream = new FileStream(path, FileMode.Open))
        {
            var image = ImageResult.FromStream(stream);
            
            Width = image.Width;
            Height = image.Height;

            _pixels = new byte[Width * Height];

            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = image.Data[i * PixelBuffer.Channels];
            }
        }

        for (int i = 0; i < _pixels.Length; i++)
        {
            if (_pixels[i] == 0)
            {
                _pixels[i] = TransparentPixel;
                continue;
            }

            _pixels[i] /= 64;
        }
    }
    
    public byte this[int x, int y] => _pixels[y * Width + x];
}