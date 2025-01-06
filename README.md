# Tilt_Game_Algoritm
# Tilt Game Solver

## Overview
This project is a **C# implementation** of a solver for the "Tilt Game." The goal is to determine whether a given board configuration has a solution, and if so, find the **minimum number of steps** required along with the **sequence of moves**.

## Algorithm
We use the **Breadth-First Search (BFS)** algorithm to explore the possible moves and find the shortest path to the solution.

### Why BFS?
- Guarantees the shortest path.
- Efficient in exploring all possible board states systematically.
- Works well for this type of puzzle where each move changes the board state deterministically.

## Features
✅ Detects if the board configuration has a solution.
✅ Finds the minimum number of moves required to solve it.
✅ Outputs the sequence of moves leading to the solution.

## Implementation Details
- Implemented in **C#**.
- Uses **BFS with hashing** (to avoid visiting duplicate board states).
- Hashing utilizes **prime numbers** for better uniqueness.

## How It Works
1. The initial board configuration is provided as input.
2. The BFS algorithm explores all possible moves:
   - Moves in **four directions** (Up, Down, Left, Right).
   - Ensures valid board states are maintained.
3. If a solution is found:
   - Outputs the **minimum number of steps**.
   - Displays the **sequence of moves**.
4. If no solution exists, it reports "No Solution."

## Usage
### **Running the Solver**
```sh
# Compile the C# project
csc TiltGameSolver.cs

# Run the executable
TiltGameSolver.exe
```

### **Example Input**
```
Board:
#####
#O  #
#   #
#  X#
#####
```

### **Example Output**
```
Solution Found!
Minimum Steps: 3
Moves: Right → Down → Left
```

## Future Enhancements
🔹 Optimize hashing strategy for faster state lookup.  
🔹 Implement a GUI for better visualization.  
🔹 Support different board sizes and obstacles.  


## License
This project is open-source and licensed under the **MIT License**.


