using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDatabase : MonoBehaviour
{
    private static Skin[] FriendlySkins;
    private static Skin[] HostileSkins;

    [SerializeField] private Skin[] friendlySkins;
    [SerializeField] private Skin[] hostileSkins;

    private void Awake()
    {
        FriendlySkins = friendlySkins;
        HostileSkins = hostileSkins;
    }

    public static Skin GetSkin(string name, Type type)
    {
        switch (type)
        {
            case Type.FRIENDLY:
                for (int i = 0; i < FriendlySkins.Length; i++)
                {
                    if (FriendlySkins[i].name == name)
                        return FriendlySkins[i];
                }
                break;
            case Type.HOSTILE:
                for (int i = 0; i < HostileSkins.Length; i++)
                {
                    if (HostileSkins[i].name == name)
                        return HostileSkins[i];
                }
                break;
            default:
                Debug.Log("ObjectDatabase.GetSkin(name, type) - Invalid type!");
                break;
        }

        Debug.Log("Array could not be reached");
        return null;
    }

    public static Skin GetRandom(Type type)
    {
        if (type == Type.FRIENDLY)
            return FriendlySkins[Random.Range(0, FriendlySkins.Length)];
        else if (type == Type.HOSTILE)
            return HostileSkins[Random.Range(0, HostileSkins.Length)];

        Debug.Log("Array could not be reached");
        return null;
    }
}
