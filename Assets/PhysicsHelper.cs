using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhysicsHelper : MonoBehaviour
{
    static PhysicsHelper s_Instance;
    static PhysicsHelper Instance
    {
        get
        {
            if (s_Instance != null)
                return s_Instance;

            s_Instance = FindObjectOfType<PhysicsHelper>();

            if (s_Instance != null)
                return s_Instance;

            Create();

            return s_Instance;
        }
        set { s_Instance = value; }
    }

    static void Create()
    {
        GameObject physicsHelperGameObject = new GameObject("PhysicsHelper");
        s_Instance = physicsHelperGameObject.AddComponent<PhysicsHelper>();
    }

    Dictionary<Collider2D, MovingPlatform> m_MovingPlatformCache = new Dictionary<Collider2D, MovingPlatform>();
    Dictionary<Collider2D, PlatformEffector2D> m_PlatformEffectorCache = new Dictionary<Collider2D, PlatformEffector2D>();

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        PopulateColliderDictionary(m_MovingPlatformCache);
        PopulateColliderDictionary(m_PlatformEffectorCache);
    }

    protected void PopulateColliderDictionary<TComponent>(Dictionary<Collider2D, TComponent> dict)
        where TComponent : Component
    {
        TComponent[] components = FindObjectsOfType<TComponent>();

        for (int i = 0; i < components.Length; i++)
        {
            Collider2D[] componentColliders = components[i].GetComponents<Collider2D>();

            for (int j = 0; j < componentColliders.Length; j++)
            {
                dict.Add(componentColliders[j], components[i]);
            }
        }
    }

    public static bool ColliderHasMovingPlatform(Collider2D collider)
    {
        return Instance.m_MovingPlatformCache.ContainsKey(collider);
    }

    public static bool ColliderHasPlatformEffector(Collider2D collider)
    {
        return Instance.m_PlatformEffectorCache.ContainsKey(collider);
    }

    public static bool TryGetMovingPlatform(Collider2D collider, out MovingPlatform movingPlatform)
    {
        return Instance.m_MovingPlatformCache.TryGetValue(collider, out movingPlatform);
    }

    public static bool TryGetPlatformEffector(Collider2D collider, out PlatformEffector2D platformEffector)
    {
        return Instance.m_PlatformEffectorCache.TryGetValue(collider, out platformEffector);
    }

}