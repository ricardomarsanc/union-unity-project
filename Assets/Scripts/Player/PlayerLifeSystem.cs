using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] private List<GameObject> respawnPoints;
    [SerializeField]private CinemachineFreeLook vcam;
    private GameObject respawnPoint;
    private IEnumerator coroutine;

    [SerializeField] Image blackBackground;
    [SerializeField] private Color opaqueColor = Color.black;
    [SerializeField] private Color transparentColor = Color.clear;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        canvas.enabled = false;
        // If for some reason there are no respawn points defined, create a default one in the (0, 0, 0) position of the scene
        if (respawnPoints.Count < 1)
        {
            GameObject defaultRespawnPoint = new GameObject();
            defaultRespawnPoint.name = "Default_Respawn_Point";
            defaultRespawnPoint.transform.position = Vector3.zero;
            respawnPoints.Add(defaultRespawnPoint);
        }
    }

    GameObject GetNearestRespawnPoint()
    {
        float distance = -1f;
        GameObject respawnPoint = null;
        foreach (GameObject point in respawnPoints)
        {
            float auxDistance = Vector3.Distance(point.transform.position, player.transform.position);
            if (auxDistance < distance || distance == -1)
            {
                distance = auxDistance;
                respawnPoint = point;
            }
        }

        // Double check in case the respawnPoints list is empty or there was a problem with the conditions
        if (respawnPoint == null)
            return new GameObject();

        return respawnPoint;
    }

    IEnumerator Respawn(GameObject rp)
    {
        yield return new WaitForSeconds(1);

        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.transform.position, Quaternion.identity);
        vcam.Follow = newPlayer.transform;
        vcam.LookAt = newPlayer.transform;
        player = newPlayer;
    }

    public void Die()
    {

        Fade(2f);

        // Play Die Animation
        // Play fade-out animation
        respawnPoint = GetNearestRespawnPoint();
        Destroy(player);
        coroutine = Respawn(respawnPoint);
        StartCoroutine(coroutine);
    }

    public void Fade(float duration)
    {
        duration *= 0.5f;

        if (duration <= 0)
        {
            duration = Time.deltaTime / 2;
        }

        StartCoroutine(IFade(duration));
    }

    private IEnumerator IFade(float duration)
    {
        canvas.enabled = true;

        blackBackground.color = transparentColor;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            blackBackground.color = Color.Lerp(transparentColor, opaqueColor, t);

            yield return null;
        }

        blackBackground.color = opaqueColor;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            blackBackground.color = Color.Lerp(opaqueColor, transparentColor, t);

            yield return null;
        }

        blackBackground.color = transparentColor;

        canvas.enabled = false;
    }

}
