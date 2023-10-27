using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameFinished : MonoBehaviour
{
GameObject cartel;

 void Start(){
    cartel = GameObject.Find("CenterEyeAnchor").transform.GetChild(0).gameObject;

 }

private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("salut");
            cartel.SetActive(cartel);
            StartCoroutine(ResetGame());
        }
    }
    IEnumerator ResetGame(){
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);
    }
}
