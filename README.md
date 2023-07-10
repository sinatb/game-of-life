# conway's game of life

The following project is a simple implementation of [Conway's game of life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) in unity Engine.

## Made with

![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

## Explanation

In game of life each cell is either dead or alive.There are 4 rules for updating the cells in this game.

1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
2. Any live cell with two or three live neighbours lives on to the next generation.
3. Any live cell with more than three live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

In the code after each turn, the new state of each cell is determined in `Cell` and then shown in `GameManager`.Computation on a gruoup of Cells is done in the `Grid` class.

## Next Steps

The next step for the project is to implement a drag and drop system for adding some of the well-known patterns.
