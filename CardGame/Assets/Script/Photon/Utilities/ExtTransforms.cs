using UnityEngine;
public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
    {
        foreach (Transform child in t)
        {
            if(destroyImmediately)
                MonoBehaviour.Destroy(child.gameObject);
            else
                MonoBehaviour.DestroyImmediate(child.gameObject);
        }
    }
}