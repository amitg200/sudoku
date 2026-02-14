# Sudoku Solver -- C# Implementation

## Overview

This project is a fully object-oriented Sudoku solver implemented in C#.
It follows SOLID design principles and uses a backtracking algorithm
enhanced with:

-   MRV (Minimum Remaining Values) heuristic
-   Forward checking (domain propagation)
-   Domain undo mechanism for efficient backtracking

The system is modular, testable, and extensible.

------------------------------------------------------------------------

## Features

-   Input validation
-   Board validation (rows, columns, sub-grids)
-   Backtracking solver with heuristics
-   Forward-checking domain pruning
-   MRV cell selection strategy
-   Full unit test coverage (xUnit)
-   Performance measurement
-   XML documentation for all core components
-   Clean separation of responsibilities

------------------------------------------------------------------------

## Architecture

The system is divided into logical modules:

### Input Layer

-   IInput
-   InputConsole

Responsible only for reading input.

### Output Layer

-   IOutput
-   OutputConsole

Responsible only for formatting and printing output.

### Core Domain

-   Board
-   SudokuConstants

Represents the Sudoku model and global configuration.

### Parsing & Formatting

-   Parser
-   BoardFormatter

Convert between string representation and board representation.

### Validation

-   InputValidation
-   BoardValidation

Ensure correctness of input and board state.

### Solver

-   ISudokuSolver
-   BacktrackingSolver
-   ICellSelector
-   MrvCellSelector
-   DomainManager

Responsible for solving logic.

------------------------------------------------------------------------

## Algorithm

The solver uses:

### 1. Backtracking

Classic depth-first search with recursion.

### 2. MRV Heuristic

Selects the cell with the smallest domain (fewest possible values). This
reduces branching dramatically.

### 3. Forward Checking

After assigning a value:
    Remove that value from neighbors' domains.
    If a domain becomes empty → backtrack immediately.

### 4. Domain Undo System

A stack of DomainChange objects tracks domain removals. When
backtracking: - Only revert changes made in the current recursion level.

This makes the solver efficient and memory-safe.

------------------------------------------------------------------------

## How to Run the Program

From the root directory: 

    dotnet run --project ./src -c Release

The program will prompt:

    Hello, please type in the sudoku:

Type: - An 81-character Sudoku string - Or `quit` to exit

------------------------------------------------------------------------

## How to Run Tests

    dotnet test -c Release

Tests include:

-   Input validation tests
-   Board validation tests
-   Parser tests
-   Board tests
-   Board formatter tests
-   Solver performance tests

------------------------------------------------------------------------

## Project Structure

    Sudoku/
    │
    ├── src/
    │   ├── Input/
    │   ├── Output/
    │   ├── Parser/
    │   ├── Validation/
    │   ├── Solver/
    │   ├── Formatter/
    │   ├── SudokuConstants.cs
    │   ├── Program.cs
    │   └── Sudoku.csproj
    │
    ├── Sudoku.Tests/
    │   ├── Tests...
    │   └── sudokus.txt
    │
    └── README.md

------------------------------------------------------------------------

## Extensibility

The project was designed to allow:

-   Adding new solving strategies (implement ISudokuSolver)
-   Adding new cell selection heuristics (implement ICellSelector)
-   Replacing console UI with GUI (implement IInput and IOutput)
-   Changing input format via SudokuConstants.InputSymbolMap
-   Support for different board sizes

------------------------------------------------------------------------

## Future Improvements

Possible extensions:

-   Bitmask-based domain representation (performance boost)
-   Parallel solving
-   GUI interface

------------------------------------------------------------------------

## Requirements

-   .NET 8+ / .NET 9 / .NET 10
-   xUnit for testing

------------------------------------------------------------------------