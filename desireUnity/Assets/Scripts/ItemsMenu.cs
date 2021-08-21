using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private Animator animator;
    private GameManager gameManager;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void UseItem(GameObject item)
    {
        gameManager.UsedItem(item.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("isDown") && !gameManager.inConversation)
        {
            animator.SetBool("isDown", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator.GetBool("isDown"))
        {
            animator.SetBool("isDown", false);
        }
    }
}
