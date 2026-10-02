using Minicraft.NET.Math;
using SDL3;

namespace Minicraft.NET.Graphics;

public sealed class Renderer
{
    private readonly nint _texture;
    private readonly nint _renderer;
    private readonly PixelBuffer _pixelBuffer = new();
    private readonly Palette _palette = new();

    public Renderer(nint window)
    {
        _renderer = SDL.CreateRenderer(window, null);
        if (_renderer == 0)
        {
            throw new InvalidOperationException(SDL.GetError());
        }

        _texture = SDL.CreateTexture(_renderer, SDL.PixelFormat.ARGB8888, SDL.TextureAccess.Streaming, PixelBuffer.Width, PixelBuffer.Height);
        if (_texture == 0)
        {
            throw new InvalidOperationException(SDL.GetError());
        }
        
        SDL.SetTextureScaleMode(_texture, SDL.ScaleMode.Nearest);
    }

    public void Blit(Texture texture, Rect<int> src, Point<int> dst, int?[] colors)
    {
        for (int y = 0; y < src.H; y++)
        {
            int pixelY = y + dst.Y;

            if (pixelY is < 0 or >= PixelBuffer.Height)
            {
                continue;
            }
            
            for (int x = 0; x < src.W; x++)
            {
                int pixelX = x + dst.X;
                
                if (pixelX is < 0 or >= PixelBuffer.Width)
                {
                    continue;
                }
                
                byte pixel = texture[x + src.X, y + src.Y];

                if (pixel == Texture.TransparentPixel || colors[pixel] == null)
                {
                    continue;
                }
                
                _pixelBuffer.SetPixel(pixelX, pixelY, _palette[colors[pixel].Value]);
            }
        }
    }

    public void Splat()
    {
        SDL.UpdateTexture(
            _texture,
            new SDL.Rect { X = 0, Y = 0, W = PixelBuffer.Width, H = PixelBuffer.Height },
            _pixelBuffer.Pixels,
            PixelBuffer.Pitch
        );

        SDL.RenderTexture(_renderer, _texture, new SDL.FRect { X = 0, Y = 0, W = PixelBuffer.Width, H = PixelBuffer.Height },
            new SDL.FRect { X = 0, Y = 0, W = 1280, H = 720 });

        SDL.RenderPresent(_renderer);
    }
}