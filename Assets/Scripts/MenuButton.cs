using UnityEngine.EventSystems;
using UnityEngine;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] int thisIndex;
    [SerializeField] AnimationFunction animationFunction;


    private void Update()
    {
        if(menuButtonController.index == thisIndex)
        {
            animator.SetBool("Hover", true);
        
         
        }
        else
        {
            animator.SetBool("Hover", false);
            animator.SetBool("Pressed", false);
        }
    
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        menuButtonController.index = thisIndex;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hover", false);
        animator.SetBool("Pressed", false);
    }


}
