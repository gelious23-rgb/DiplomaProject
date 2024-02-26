using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class TestNetCodeUi : MonoBehaviour
    {
        [SerializeField] private Button _startHostButton;
        [SerializeField] private Button _startClientButton;

        private void Awake()
        {
            _startHostButton.onClick.AddListener(() => {
                Debug.Log("Start Host");
                NetworkManager.Singleton.StartHost();
                Hide();
            });
            _startClientButton.onClick.AddListener(() => {
                Debug.Log("Start Client");
                NetworkManager.Singleton.StartClient();
                Hide();
            });
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
