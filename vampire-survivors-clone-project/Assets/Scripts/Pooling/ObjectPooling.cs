using System.Collections.Generic;

public sealed class ObjectPooling<T> where T : IPooling
{
    public delegate T OnInstance();

    private List<T> m_objectPools;

    /// <summary>
    /// Get an instance of the object pool.
    /// </summary>
    public IReadOnlyList<T> ObjectPools => m_objectPools;

    /// <summary>
    /// Get the number of instances of the object pool.
    /// </summary>
    public int Count => m_objectPools.Count;

    private OnInstance m_onInstance;

    /// <summary>
    /// Constructor that need to pass a delegate to create an instance of the object pool.
    /// </summary>
    /// <param name="onInstance"></param>
    public ObjectPooling(OnInstance onInstance)
    {
        m_objectPools = new List<T>();
        m_onInstance = onInstance;
    }

    /// <summary>
    /// Constructor that need to pass a delegate to create an instance of the object pool and the amount of instances.
    /// </summary>
    /// <param name="onInstance"></param>
    /// <param name="amount"></param>
    public ObjectPooling(OnInstance onInstance, int amount)
    {
        m_objectPools = new List<T>();
        m_onInstance = onInstance;
        if (m_onInstance != null)
        {
            for (int i = 0; i < amount; i++)
            {
                T obj = onInstance.Invoke();
                obj.ActiveInPool = false;
                m_objectPools.Add(obj);
            }
        }
    }

    /// <summary>
    /// Get an instance of the object pool.
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        for (int i = 0; i < m_objectPools.Count; i++)
        {
            if (!m_objectPools[i].ActiveInPool)
            {
                m_objectPools[i].ActiveInPool = true;
                return m_objectPools[i];
            }
        }
        if (m_onInstance != null)
        {
            T obj = m_onInstance.Invoke();
            obj.ActiveInPool = true;
            m_objectPools.Add(obj);
            return obj;
        }
        else
            return default(T);
    }

    /// <summary>
    /// Dispose the object pool and clear the object pooling list
    /// If you want to clear or dispose the object pool, you can call this method.
    /// </summary>
    public void Dispose()
    {
        for (int i = 0; i < m_objectPools.Count; i++)
        {
            m_objectPools[i].ActiveInPool = false;
        }
        m_objectPools.Clear();
    }
}