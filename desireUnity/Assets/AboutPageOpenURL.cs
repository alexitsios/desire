using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AboutPageOpenURL : MonoBehaviour
{
    [SerializeField] private string URL;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate 
        {
            Application.OpenURL(URL);
        });
    }
}
