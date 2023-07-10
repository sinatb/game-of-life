using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool _isAlive;
    private bool _nextIsAlive;
    private SpriteRenderer _spriteColor;

    private List<Cell> _neighbours;
    // Start is called before the first frame update
    void Awake()
    {
        _spriteColor = gameObject.GetComponent<SpriteRenderer>();
        _neighbours = new List<Cell>(9);
    }
    public void SetIsAlive()
    {
        _isAlive = true;
    }
    public void ResetIsAlive()
    {
        _isAlive = false;
    }
    public bool GetIsAlive()
    {
        return _isAlive;
    }
    private int numNeighbours()
    {
        int ret = 0;
        foreach (var cell in _neighbours)
        {
            if (cell.GetIsAlive())
                ret++;
        }
        return ret;
    }
    public void ChangeState()
    {
        _isAlive = _nextIsAlive;
    }
    public void UpdateState()
    {
        int newNum = numNeighbours();
        _nextIsAlive = false;
        if (newNum == 3)
            _nextIsAlive = true;
        if (_isAlive && (newNum == 3 || newNum == 2))
            _nextIsAlive = true;
    }
    public void AddNeighbour(Cell neighbour)
    {
        _neighbours.Add(neighbour);
    }
    // Update is called once per frame
    void Update()
    {
        if (_isAlive)
            _spriteColor.color = Color.yellow;
        else
            _spriteColor.color = Color.white;
    }
}
