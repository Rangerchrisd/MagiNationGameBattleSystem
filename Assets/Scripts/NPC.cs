using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour, IPointerDownHandler
{
    Vector2 worldPoint;
    public GameObject dialoguePrefab;
    public Protagonist playerScript;
    public float distanceTalk;
    // Update is called once per frame
    private void Start()
    {
        playerScript = FindObjectOfType<Protagonist>();
    }
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("c");
        talking();
    }

    public void talking() {
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(playerScript.gameObject.transform.position, this.transform.position) < distanceTalk)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Instantiate(dialoguePrefab, Vector3.zero, Quaternion.identity);

                Debug.Log("a");
            }
        }
    }
}
