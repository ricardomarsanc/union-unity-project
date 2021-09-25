using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    private Vector3 finalMove;

    public Animator animator;

    [SerializeField] private PlayerController controller;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isDashing", true);
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            finalMove = controller.movePlayer;
            finalMove.y = 0;

            controller.playerCtrl.Move(finalMove * dashSpeed * Time.deltaTime);

            yield return null;
            animator.SetBool("isDashing", false);
        }
        
    }
}
