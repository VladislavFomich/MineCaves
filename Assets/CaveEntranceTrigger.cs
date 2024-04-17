using UnityEngine;

public class CaveEntranceTrigger : MonoBehaviour
{
    [SerializeField] private EntranceType entranceType = EntranceType.Background;

    public enum EntranceType
    {
        Background,
        ClipPlane
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHandler>();
        if (player != null)
        {
            if(entranceType == EntranceType.Background)
            {
                CameraManager.Instance.EntranceCaveBackground();
            }
            else if(entranceType == EntranceType.ClipPlane)
            {
                CameraManager.Instance.EntranceCaveClipPlane();
            }
        }
    }
}
