namespace Minicraft.NET.Graphics;

public sealed class Palette
{
    public const int Channels = 6;

    private readonly uint[] _shades = new uint[Channels * Channels * Channels];

    public Palette()
    {
        int i = 0;

        for (int r = 0; r < Channels; r++)
        {
            for (int g = 0; g < Channels; g++)
            {
                for (int b = 0; b < Channels; b++)
                {
                    float rr = ToColorSpace(r);
                    float gg = ToColorSpace(g);
                    float bb = ToColorSpace(b);

                    float luminance = (rr * 30f + gg * 59f + bb * 11f) / 100f;

                    ApplyLuminance(luminance, ref rr);
                    ApplyLuminance(luminance, ref gg);
                    ApplyLuminance(luminance, ref bb);

                    _shades[i] = (uint)((0xFF << 24)
                                        | ((int)rr << 16)
                                        | ((int)gg << 8)
                                        | (int)bb);

                    i++;
                }
            }
        }
    }

    public uint this[int index] => _shades[index];

    public static int Palettize(int r, int g, int b)
    {
        return MapColor(ToColorSpace(r)) * Channels * Channels +
               MapColor(ToColorSpace(g)) * Channels +
               MapColor(ToColorSpace(b));
    }

    private static void ApplyLuminance(float luminance, ref float color)
    {
        color += luminance;
        color /= 2f;
        color *= 230f / byte.MaxValue;
        color += 10f;
    }

    private static int ToColorSpace(int color)
    {
        return color * byte.MaxValue / (Channels - 1);
    }

    private static byte MapColor(int color)
    {
        if (color < 0)
        {
            return 0;
        }

        return (byte)(color * 100f % 10f + color * 10f % 10f + color % 10f);
    }
}