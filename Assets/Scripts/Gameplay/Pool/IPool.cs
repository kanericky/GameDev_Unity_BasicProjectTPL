namespace Gameplay.Pool
{
    /// <summary>
    /// A collection that pools objects of given type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPool<T>
    {
        // Pre-setup the pool with the given size
        void PreSetup(int poolSize);
        
        // Handle the pop request to get the item with the given type
        T Request();
        
        // Push the item back to the pool
        void Return(T member);
    }
}
