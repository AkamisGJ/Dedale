using Obi;
using UnityEngine;

public class ActivateRope : MonoBehaviour
{
    [SerializeField] private ObiParticleAttachment _particleAttachmentController = null;

    public void DetachPerfusion()
    {
        _particleAttachmentController.enabled = false;
    }
}
