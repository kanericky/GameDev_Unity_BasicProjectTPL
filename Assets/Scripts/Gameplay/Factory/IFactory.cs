namespace Gameplay.Factory
{
    /// <summary>
    /// Factory for the given type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<T>
    {
        T Create();
    }
}
