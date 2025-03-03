using UnityEngine;

public class RandomIdelAnimations : MonoBehaviour
{
    private Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
        myAnim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
