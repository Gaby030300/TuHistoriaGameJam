using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Camara System")]
    [SerializeField] private CinemachineVirtualCamera zoomCamera;
    [SerializeField] private CinemachineVirtualCamera firstCamera;

    private void Awake()
    {
        zoomCamera.gameObject.SetActive(false);
        firstCamera.gameObject.SetActive(true);  
    }

    private void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            zoomCamera.gameObject.SetActive(true);
            firstCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            zoomCamera.gameObject.SetActive(false);
            firstCamera.gameObject.SetActive(true);
        }
    }
}
