namespace Minicraft.NET.Registry;

public class Registry<T>
{
    private readonly Dictionary<string, int> _names = new();
    private readonly List<T> _values = [];

    protected void Add(string name, T value)
    {
        _names[name] = _values.Count;
        _values.Add(value);
    }
    
    public int this[string name] => _names[name];
    
    public T this[int index] => _values[index];
}