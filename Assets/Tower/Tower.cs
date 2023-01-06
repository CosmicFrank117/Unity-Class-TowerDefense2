using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    GameObject spawnAtRuntime;

    void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }
        
        if(bank.CurrentBalance >= cost)
        {
            spawnAtRuntime = GameObject.FindWithTag("Spawn At Runtime");
            GameObject ballista = Instantiate(tower.gameObject, position, Quaternion.identity);
            ballista.transform.parent = spawnAtRuntime.transform;

            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build() {
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);

                foreach(Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(false);
                }
            }

            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(buildDelay);

                foreach(Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(true);
                }
            }

        }
    }
}