using Minicraft.NET.Graphics;
using Minicraft.NET.Math;
using Minicraft.NET.Registry;
using SDL3;

namespace Minicraft.NET;

public static class Program
{
    public static void Main(string[] args)
    {
        SDL.Init(SDL.InitFlags.Video);

        nint window = SDL.CreateWindow("Minicraft.NET", 1280, 720, 0);
        if (window == 0)
        {
            throw new InvalidOperationException(SDL.GetError());
        }

        var renderer = new Renderer(window);
        
        Font.Blit(renderer, "!!!!!!!!!!!!!!!!!!!!! Hello World! !!!", 0, 0, null, Palette.Palettize(1, 5, 1));
        
        bool running = true;

        while (running)
        {
            while (SDL.PollEvent(out SDL.Event sdlEvent))
            {
                switch (sdlEvent.Type)
                {
                    case (uint)SDL.EventType.Quit:
                        running = false;
                        break;
                }
            }

            renderer.Splat();
        }

        SDL.DestroyWindow(window);

        SDL.Quit();
    }
}