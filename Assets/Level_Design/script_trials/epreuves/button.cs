using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField] private GameObject _door1 = null;
    [SerializeField] private GameObject _door2 = null;
    private bool _activate1 = false;
    

    private void OnTriggerEnter(Collider other)
    {
        _activate1 = !_activate1;
        _door1.SetActive(_activate1);
        _door2.SetActive(!_activate1);
    }
}
