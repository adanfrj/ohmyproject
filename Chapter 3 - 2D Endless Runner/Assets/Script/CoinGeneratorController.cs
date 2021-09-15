using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneratorController : MonoBehaviour
{
    [Header("Templates")]
    public List<CoinController> coinTemplates;
    public float coinTemplatesWidth;

    [Header("Generator Area")]
    public Camera gameCamera;
    public float areaStartOffset;
    public float areaEndOffset;

    private const float debugLineHeight = 10.0f;

    private float GetHorizontalPositionStart()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(0f, 0f)).x + areaStartOffset;
    }

    private float GetHorizontalPositionEnd()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(1f, 0f)).x + areaEndOffset;
    }

    //debug
    private void OnDrawGizmos()
    {
        Vector3 areaStartPosition = transform.position;
        Vector3 areaEndPosition = transform.position;

        areaStartPosition.x = GetHorizontalPositionStart();
        areaEndPosition.x = GetHorizontalPositionEnd();

        Debug.DrawLine(areaStartPosition + Vector3.up * debugLineHeight / 2, areaStartPosition + Vector3.down * debugLineHeight / 2, Color.red);
        Debug.DrawLine(areaEndPosition + Vector3.up * debugLineHeight / 2, areaEndPosition + Vector3.down * debugLineHeight / 2, Color.red);
    }

    private List<GameObject> spawnedCoin;
    private float lastGeneratedPositionX;
    private float lastRemovedPositionX;
    

    private void Start()
    {
        spawnedCoin = new List<GameObject>();
        lastGeneratedPositionX = GetHorizontalPositionStart();
        lastRemovedPositionX = lastGeneratedPositionX - coinTemplatesWidth;

        while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
             //generate
             GenerateCoin(lastGeneratedPositionX);
             lastGeneratedPositionX += coinTemplatesWidth;
        }
    }

    private void Update()
    {
        while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
        GenerateCoin(lastGeneratedPositionX);
        lastGeneratedPositionX += coinTemplatesWidth;
        }

        while (lastRemovedPositionX + coinTemplatesWidth< GetHorizontalPositionStart())
        {
        RemoveCoin(lastRemovedPositionX);
        lastRemovedPositionX += coinTemplatesWidth;
        }
    }

    private void GenerateCoin(float posX, CoinController forcecoin = null)
    {
        GameObject newCoin = Instantiate(coinTemplates[Random.Range(0, coinTemplates.Count)].gameObject, transform);
        newCoin.transform.position = new Vector2(posX,0f);
        spawnedCoin.Add(newCoin);
    }

    private void RemoveCoin(float posX)
    {
        GameObject coinToRemove = null;

        //find terrain at pos x
        foreach (GameObject item in spawnedCoin)
        {
            if (item.transform.position.x == posX)
            {
                coinToRemove = item;
                break;
            }
        }

        //after found
        if (coinToRemove !=null )
        {
            spawnedCoin.Remove(coinToRemove);
            Destroy(coinToRemove);
        }
    }
}
