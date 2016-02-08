using UnityEngine;

/// <summary>
/// Base Manager. All Managers should inherit from this class.
/// </summary>
public abstract class Director<T> : Singleton<T>, IDirector where T : MonoBehaviour
{
}
