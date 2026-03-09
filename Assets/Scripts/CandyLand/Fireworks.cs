using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private string _fireworksSong;
    public void InitializeFireworks()
    {
        AudioManager.Instance.PlaySound(_fireworksSong);
    }
}
