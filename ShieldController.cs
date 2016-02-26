using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
    public float timeTillShutDown = 0;
    public float percentTillWarning = 90f;
    public float percentFlashTakes = 2f;

    MeshRenderer shieldMesh;
    float timeTillWarning;
    float flashTime;

    void Start()
    {
        shieldMesh = gameObject.GetComponent<MeshRenderer>();
        timeTillWarning = timeTillShutDown * percentTillWarning/100f;
        flashTime = timeTillShutDown * percentFlashTakes/100f;
        StartCoroutine(ActivateShield());
    }

    IEnumerator ActivateShield()
    {
        yield return new WaitForSeconds(timeTillWarning);
        
        for (int i = 0; i <= (100 - percentTillWarning) / (percentFlashTakes/2); i++)
        {
            if (shieldMesh.enabled == true)
                shieldMesh.enabled = false;
            else
                shieldMesh.enabled = true;
            yield return new WaitForSeconds(flashTime);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
