using Minicraft.NET.Graphics;

namespace Minicraft.NET.Registry;

public class TextureRegistry : Registry<Texture>
{
    public static TextureRegistry Instance { get; } = new(); 
    
    private TextureRegistry()
    {
        foreach (string file in Directory.EnumerateFiles(@"Assets\Textures"))
        {
            var texture = new Texture(file);
            
            Add(Path.GetFileNameWithoutExtension(file), texture);
        }
    }
}