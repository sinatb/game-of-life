using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gridObj;
    private Grid mainGrid;
    private bool _isStarted;
    private bool _isActive;
    private float _waitTime = 2.0f;
    public Camera mainCam;
    private int turn = 0;
    public TextMeshProUGUI StartText;
    // Start is called before the first frame update
    void Start()
    {
        mainGrid = gridObj.GetComponent<Grid>();
        mainGrid.CreateGrid();
        StartCoroutine(interval());
    }

    IEnumerator interval()
    {
        yield return new WaitForSeconds(_waitTime);
        turn++;
        _isActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isStarted)
        {
            if (_isActive)
            {
                mainGrid.UpdateGrid();
                _isActive = false;
                StartCoroutine(interval());
            }
        }
        else
        {
            SetCellState();
        }
    }

    public void SetStart()
    {
        _isStarted = !_isStarted;
        StartText.text = _isStarted ? "Stop" : "Start";
    }
    private void SetCellState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero);
            if (hit)
            {
                GameObject hitObj = hit.transform.gameObject;
                if (hitObj.tag == "Cell")
                {
                    Cell cellComp = hitObj.GetComponent<Cell>();
                    if (cellComp.GetIsAlive())
                        cellComp.ResetIsAlive();
                    else
                        cellComp.SetIsAlive();
                }
            }
        }
    }
}
