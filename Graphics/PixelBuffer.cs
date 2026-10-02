namespace Minicraft.NET.Graphics;

public sealed class PixelBuffer
{
    public const int Width = 320;
    public const int Height = 180;

    public const int Channels = 4;

    public const int Pitch = Width * Channels;

    public byte[] Pixels { get; } = new byte[Width * Height * Channels];

    public void SetPixel(int x, int y, uint color)
    {
        SetPixel(x, y, (byte)(color >> 0), (byte)(color >> 8), (byte)(color >> 16), (byte)
            (color >> 24));
    }

    public void SetPixel(int x, int y, byte b, byte g, byte r, byte a)
    {
        int index = (y * Width + x) * Channels;

        Pixels[index] = b;
        Pixels[index + 1] = g;
        Pixels[index + 2] = r;
        Pixels[index + 3] = a;
    }
}