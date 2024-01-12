Project 1
Kort beskrivning av projektet, inklusive dess syfte och huvudfunktioner.

Content
1. Description
2. Structure
3. Patterns/Principles/Methods
4. Shapes
5. Calculator
6. Rock Paper Scissors Game
7. Bygga och köra projektet
8. Licens
9. Beskrivning

1. Description

There are 3 main parts of this app (Shape calculation, Calculation & Rock Paper Scissor).

This project is made to help calculate different shapes (example Rhombus, Parallelogram) and to calculate different numbers (example Square Root, Triangle). 
The project also feature a mini game called rock paper scissors to play whenever, especially useful when you're bored of doing calculations!

2. Struktur
Beskriv hur projektet är strukturerat och vilka komponenter det består av.

3. Patterns/Principles

The patterns used in my project:
Factory pattern
Strategy pattern,
Dependency Injection pattern,
Singleton pattern.

Principles i have used in my project:
SOC (Separation of Concern):
I achieved this by making sure to divide my classes and interfaces into different maps and more classes. This makes it easier to for example debug, scale and develop.

Single Responsibility Principle (SRP):
Since i have divided my classes and interfaces it also makes my project follow SRP by only being responsible for one thing. 
An example of this is my Strategy classes, they are only responsible of calculating either a shape or a numbered calculation.

Open/Closed Principle (OCP):
My classes have been designed to not require any adjustments but they can be developed to include new features without needing to change code that's already written.

Interface Segregation Principle (ISP):
Most of my classes have a interface and the interfaces only have methods that the classes require, the methods that arent required for the interface were made private and thus my project
is following ISP.

Dependency Inversion Principle (DIP):
Almost all my classes depend on interfaces that are registered by Autofac, thus my project is not very dependent on different parts of my code.

4. Shapes
Description of how the shapes class/method/interfaces works.

Shapes classes, interfaces and methods.

ShapeMenuFactory
- CreateMenu
Uses IMenu and IMenuFactory to create Menu for Shapes.
ShapesMenu
- ShowMenu
Shows all necessary parts of Shapes menu.
Shape Entity
- Relevant attributes for the database.
ParallelogramStrategy
- SetDimensions
Sets the dimensions for the shape.
- GetDimensionCount
Returns the count of dimensions.
- CalculateArea
Returns a calculation of the area.
- CalculatePerimeter
Returns a calculation of the perimeter.
RectangleStrategy
- Same as Parallelogram.
RhombusStrategy
- Same as Parallelogram.
TriangleStrategy
- Same as Parallelogram.
ShapeService
- GetAvailableShapeTypes
Returns a list of possible shapes (Parallelogram, Rectangle, Rhombus or Triangle).
- GetShapeStrategy
Switch case that returns the required shape.
- CreateShape
Uses methods "GetAvailableShapeTypes", "SetShapeCalculator", "GetShapeStrategy", "CalculateAndDisplayResults" to create a calculation of the shape user has input to the program.
- ReadShape
Displays all the shapes with their info aswell as if they are active or soft deleted.
- UpdateShape
Uses methods "ReadShape", "SaveChangesToDatabase" and updates the ShapeID user has input to the program.
- DeleteShape
Uses methods "ReadShape" and finds the ShapeID to soft delete.
- ReActivateShape
Uses methods "ReadShape" and finds the ShapeID to re activate.  
- SaveChangesToDatabase
Uses .SaveChanges to save the input from user.
ShapeContext
- SetShapeCalculator
Takes the IShapeStrategy and sets it to the relevant strategy.
- CalculateAndDisplayResults
Uses methods "GetDimensionsInput", "SetShapeProperties", "CalculateArea", "CalculatePerimeter", "SaveResultsToDatabase" to calculate and show the results.
- SaveResultsToDatabase
Uses methods "CalculateArea" and "CalculatePerimeter" to add and save results to database.
- SetShapeProperties
Uses method "SetDimensions" to set the dimensions of the shape.
- GetDimensionsInput
Used to ask user for their input and save it to the relevant dimension (base, height, sidelength)
- GetDimensionName
Switch case, returns (base, height, sidelength or default dimension)

Shapes Interface
IShapeContext
- SetShapeCalculator
- CalculateAndDisplayResults
IMenuFactory
- CreateMenu
IShape
- Relevant attributes for the database
IShapeStrategy
- CalculateArea
- CalculatePerimeter
- SetDimensions
IShapeDimensionsProvider
- GetDimensionCount

5. Calculator
Description of how the calculator class/method/interfaces works.

Calculator classes, interfaces and methods

CalculatorMenuFactory
- CreateMenu
Uses IMenu and IMenuFactory to create Menu for Calculator.
CalculatorMenu
- ShowMenu
Shows all necessary parts of Shapes menu.
Calculator Entity
- Relevant attributes for the database
AdditionStrategy
- Calculate
Returns the required calculation.
DivisionStrategy
- Calculate
Returns the required calculation.
ModulusStrategy
- Calculate
Returns the required calculation.
MultiplicationStrategy
- Calculate
Returns the required calculation.
SquareRootStrategy
- Calculate
Returns the required calculation.
SubtractionStrategy
- Calculate
Returns the required calculation.
CalculatorService
- PerformCreateCalculation
Gives user the option of what operator to use for the calculation.
Uses methods "SetStrategyFromOperationChoice", "GetUserInput", "ExecuteOperation", "SaveCalculationToDatabase" to create the desired calculation.
- IsNumberOutOfRange
Returns a check if the input number is less than 1,000,000
- SetStrategyFromOperationChoice
Switch case containing all operator strategies.
Uses method "SetStrategy".
- ReadCalculation
Displays all relevant calculation info aswell as if its active or soft deleted.
- UpdateCalculation
Uses method "ReadCalculation" and updates a calculation by inputting CalculatorID.
- DeleteCalculation
Uses method "ReadCalculation" and soft deletes a calculation by inputting CalculatorID.
- ReActivateCalculation
Uses method "ReadCalculation" and re activates a calculation by inputting CalculatorID.
- UpdateCalculationInDatabase
Uses methods "IsNumberOutOfRange", "ExecuteOperation", "SaveCalculationToDatabase".
This method checks that it is possible to update the calculation.
- IsResultOutOfRange
Returns a check if the result is less than 1,000,000.
CalculatorContext
- GetUserInput
Returns method "GetBoundedDoubleInput"
- GetBoundedDoubleInput
This method helps with exit option aswell as making sure the numbers are valid.
- ExecuteOperation
Uses method "Calculate" to perform the desired calculation.
- SetStrategy
Sets the strategy to desired operator.
- CreateCalculation
Uses methods "ExecuteOperation" and "SaveCalculationToDatabase" to create the calculation.
- ReadCalculation
Returns a list of all calculations
- UpdateCalculation
Gets calculationID and saves the changes to database.
- SaveCalculationToDatabase
Uses method "SaveCalculationDetails" to save relevant info.
- SaveCalculationDetails
Saves all information the user has input.
- GetCalculationById
Finds a calculation by CalculatorID

Calculator Interface
ICalculatorContext
- ExecuteOperation
- GetUserInput
- SetStrategy
- CreateCalculation
- ReadCalculation
- UpdateCalculation
- DeleteCalculation
- SaveCalculationToDatabase
IMenu
- ShowMenu
- GetMenuType
IMenuFactory
- CreateMenu
ICalculator
- Relevant attributes for the database
ICalculatorService
- PerformCreateCalculation
- ReadCalculation
- UpdateCalculation
- DeleteCalculation
- UpdateCalculationInDatabase
- ReActivateCalculation
ICalculatorStrategy
- Calculate

6. Rock Paper Scissors Game
Description of how the rock paper scissor class/method/interfaces works.

Rock Paper Scissors classes, interfaces and methods

GameMenuFactory
- CreateMenu
Uses IMenu and IMenuFactory to create Menu for the game.
GameMenu
- ShowMenu
Shows all necessary parts of game menu.
Game
- Relevant attributes for the database
GameHistory
- Relevant attributes for the database
GameStatistics
- Relevant attributes for the database
GameService
- PlayGame
Uses methods "GenerateComputerMove", "DetermineResult", DisplayGameResult", "UpdateGameStatistics, "SaveGameHistory".
This method is responsible for playing the game, it shows all relevant parts and gathers the logic to play rock paper scissors.
- UpdateGameStatistics
Checks the status of the game that has been played and updates the statistics (TotalWin, TotalLoss, TotalDraws)
- GenerateComputerMove
Uses the random class to call the enum "Move" between Rock, Paper and Scissor.
- DetermineResult
Gathers the user and computers enum moves to determine who won the round.
- SaveGameHistory
After game finished, saves all relevant information about the game.
- DisplayGameResult
Shows the user what the results of the finished game was.
- ViewPreviousGames
Shows the user all previous games ever played, aswell as statistics such as total wins, total loss and total draws.
- GameRules
Displays the game rules.

Rock Paper Scissors Interface
IMenu
- ShowMenu
- GetMenuType
IMenuFactory
- CreateMenu
IGame
- Relevant attributes for the database
IGameHistory
- Relevant attributes for the database
IGameStatistics
- Relevant attributes for the database
IGameService
- PlayGame
- ViewPreviousGames
- GameRules
