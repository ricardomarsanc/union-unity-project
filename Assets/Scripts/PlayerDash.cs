using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    [SerializeField] private PlayerController controller;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            controller.playerCtrl.Move(controller.movePlayer * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
