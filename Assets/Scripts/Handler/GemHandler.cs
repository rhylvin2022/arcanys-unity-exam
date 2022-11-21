using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHandler : MonoBehaviour
{
    public int Score;
    public float RotationSpeed;
    [HideInInspector]
    public Transform gemSpawnerLocation;

    private bool aboutTobeDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        if(!aboutTobeDestroyed)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.AddScore(score: Score, lastGemSpawnerLocation: gemSpawnerLocation);
                StartCoroutine(HideAndDestory());
            }
        }
    }

    private IEnumerator HideAndDestory()
    {
        aboutTobeDestroyed = true;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

}
