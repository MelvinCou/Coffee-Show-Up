using System;
using UnityEngine;

public class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Debug.Log("Start mapping");

        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        Debug.Log(wrapper);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
