using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    // [SerializeField] private Image staminaBar;
    // [SerializeField] private float decreaseAmount = 20f; //감소량
    // [SerializeField] private float recoverAmount = 0.6f; //회복량
    //
    // private const float StartingStamina = 100f;
    // private readonly WaitForSeconds _recoverDelay = new(0.05f); // 1초에 20번 * 30 프레임 == 600번
    //
    // private float _currentStamina;
    //
    // private void Awake()
    // {
    //     _currentStamina = StartingStamina;
    //     staminaBar.fillAmount = _currentStamina / StartingStamina;
    // }
    //
    // private void Start()
    // {
    //     StartCoroutine(RecoverStamina());
    // }
    //
    // public bool CheckRoll()
    // {
    //     return _currentStamina >= 20f;
    // }
    //
    // public void DeStamina()
    // {
    //     _currentStamina -= decreaseAmount;
    //     var currentFillAmount = _currentStamina / StartingStamina;
    //     staminaBar.fillAmount = currentFillAmount;
    // }
    //
    // private IEnumerator RecoverStamina()
    // {
    //     while (true)
    //     {
    //         _currentStamina = math.min(StartingStamina, _currentStamina + recoverAmount);
    //         staminaBar.fillAmount = _currentStamina / StartingStamina;
    //         yield return _recoverDelay;
    //     }
    //
    //     // ReSharper disable once IteratorNeverReturns
    // }
}