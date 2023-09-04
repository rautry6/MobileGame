using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoSpawning : MonoBehaviour
{
    [SerializeField] private GameObject pegPrefab;
    [SerializeField] private Transform startingPeg;
    private Vector3 startingPosition;
    [SerializeField] private int numberOfRows = 5;
    [SerializeField] private int pegsPerRow = 6;
    [SerializeField] private float distanceBetweenRows = 5f;
    [SerializeField] private float distanceBetweenPegs = 8f;
    [SerializeField] private float horizontalOffsetBetweenRows = 9f;

    [SerializeField] private GameObject slime;
    private Vector3 centeringOffset;

    // Start is called before the first frame update
    void Start()
    {

        CalculateCenterOffset();
        startingPosition = startingPeg.position;

        SpawnPegs();
    }

    /// <summary>
    /// Centers the board with the slime's start position
    /// </summary>
    void CalculateCenterOffset()
    {
        float sizeOfPeg = startingPeg.localScale.x;
        float lengthOfFirstRow = (sizeOfPeg * pegsPerRow) + (distanceBetweenPegs * pegsPerRow);

        float centerLength = lengthOfFirstRow / 2;

        //I dunno why multiplying by 0.7 centers it but it does
        centeringOffset = new Vector3 (slime.transform.position.x + centerLength * 0.7f, startingPeg.transform.position.y, startingPeg.transform.position.z);
        startingPeg.transform.position = centeringOffset;

    }

    /// <summary>
    /// Creates the plinko board
    /// </summary>
    void SpawnPegs()
    {
        for(int i = 0; i < numberOfRows; i++)
        {
            int currentNumPegs = 0;

            //First row starts off with one peg as the starting location
            if (i == 0)
            {
                //currentNumPegs++;   
            }

            //Spawn each peg and update current pos
            for (int j = currentNumPegs; j < pegsPerRow; j++)
            {
                GameObject nextPeg = Instantiate(pegPrefab, new Vector3(startingPosition.x, startingPosition.y, startingPosition.z), startingPeg.transform.rotation);

                nextPeg.transform.position -= Vector3.right * j * distanceBetweenPegs;

                nextPeg.transform.parent = transform; //Just keep it all under pegs object to keep scene clear
            }

            //Moves the starting position down and by the horizontal offset for the next row
            startingPosition = new Vector3(startingPosition.x - horizontalOffsetBetweenRows, startingPosition.y - distanceBetweenRows, startingPosition.z);

            //Flips horizontal offset so rows alternate
            horizontalOffsetBetweenRows = -horizontalOffsetBetweenRows;
        }
    }
}
