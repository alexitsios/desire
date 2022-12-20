using UnityEngine;

public class CreditsHandler : MonoBehaviour
{
    [SerializeField]
    [Range(0.001f, 2f)]
    private float speed = 1f;

    
    void Update()
    {
        var movement = speed * Time.deltaTime * 140;
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y + movement, 
                                         transform.position.z);
    }
}
