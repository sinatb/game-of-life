using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public float xLim = 10.0f;
    public float yLim = 10.0f;
    public float mouseSpeed = 5.0f;
    public GameObject gridObj;
    private Grid mainGrid;
    private bool _isStarted;
    private bool _isActive;
    private float _waitTime = 2.0f;
    public Camera mainCam;
    private int turn = 0;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI StartText;
    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        mainGrid = gridObj.GetComponent<Grid>();
        mainGrid.CreateGrid();
        StartCoroutine(interval());
    }
    IEnumerator interval()
    {
        yield return new WaitForSeconds(_waitTime);
        turn++;
        if (_isStarted)
            turnText.text = "Turn : " + turn.ToString();
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
        MouseManager();
    }
    public void SetStart()
    {
        _isStarted = !_isStarted;
        StartText.text = _isStarted ? "Stop" : "Start";
    }
    public void increaseDelay()
    {
        _waitTime *= 2;
    }
    public void decreaseDelay()
    {
        _waitTime /= 2;
    }
    public void resetGrid()
    {
        if (!_isStarted)
        {
            turn = 0;
            turnText.text = "Turn : 0";
            mainGrid.ResetGrid();
        }
    }
    private void SetCellState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
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
    private void MouseManager()
    {
        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
        }
        mainCam.orthographicSize = Mathf.MoveTowards (mainCam.orthographicSize, targetOrtho, 
            smoothSpeed * Time.deltaTime);
        
        if (Input.mousePosition.x < xLim)
            mainCam.transform.Translate(Vector3.left*(Time.deltaTime * mouseSpeed));
        if (Input.mousePosition.x > Screen.width - xLim)
            mainCam.transform.Translate(Vector3.right*(Time.deltaTime * mouseSpeed));
        if (Input.mousePosition.y < yLim)
            mainCam.transform.Translate(Vector3.down*(Time.deltaTime * mouseSpeed));
        if (Input.mousePosition.y > Screen.height + yLim)
            mainCam.transform.Translate(Vector3.up*(Time.deltaTime * mouseSpeed));
    }
}
