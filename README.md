# Water Jug Problem Solver

## Description

This project implements a solution to the classic water jug problem. Given two jugs of different capacities and a target amount of water, the program determines whether it is possible to measure exactly the desired amount and provides a sequence of steps to achieve it.

## System Requirements

- .NET SDK 8.0 or higher
- .NET CLI tool

## Setup

### 1. Clone the Repository
```bash
git clone https://github.com/VictorJohan/WaterJugChallengeWS.git

cd WaterJugChallengeWS

Ensure all dependencies are installed by running:
dotnet restore

Compile the project using the following command:
dotnet build

To run the project, use the command:
dotnet run
```
## Usage

The program accepts a JSON input containing the capacities of the jugs and the desired amount of water. Here is an example input:
```bash
{
  "xCapacity": 7,
  "yCapacity": 10,
  "zAmountWanted": 5
}
```
The program processes this input and provides a sequence of steps to measure the exact amount of water if possible.

## Algorithm Explanation

The algorithm uses a Breadth-First Search (BFS) approach to systematically explore all possible combinations of filling, emptying, and transferring between the two jugs. Here’s a detailed breakdown of the process:

## Initialization:

* Start with both jugs empty and enqueue this state for exploration.
* A set is used to keep track of visited states to prevent redundancy.

## State Exploration:

* While there are states in the queue, the algorithm:
  * Dequeues the current state.
  * Checks if the current state meets the desired amount of water.
  * If not, generates all possible states that can be derived from the current state by:
      * Completely filling one of the jugs.
      * Completely emptying one of the jugs.
      * Transferring water between the jugs until one is full or the other is empty.
  * Each new state is marked as visited and enqueued for future exploration.
  
## Termination Conditions:
* If a state is found that meets the desired amount of water, the algorithm terminates and provides the sequence of steps.
* If all possible states are exhausted without finding a solution, the algorithm reports that no solution is possible.
  
## Algorithm Notes
* <b>Optimization:</b> The algorithm is designed to minimize the number of steps needed to reach the solution, although it does not guarantee the shortest solution in terms of steps due to the nature of BFS.
* <b>Limitations:</b> The algorithm's efficiency decreases with increasing jug capacities due to the exponential growth of the search space in combinatorial problems.
