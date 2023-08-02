using UnityEngine;

[CreateAssetMenu(menuName ="Data/EventChannels/VoidEventChannel",fileName = "VoidEventChannel_")]
public class EventChannel : ScriptableObject
{
    event System.Action Delegate;

    public void Broadcast()
    {
        Delegate?.Invoke();
    }

    public void AddListener(System.Action action)
    {
        Delegate += action;
    }

    public void RemoveListener(System.Action action)
    {
        Delegate -= action;
    }
}
