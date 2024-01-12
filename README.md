Project 1
Kort beskrivning av projektet, inklusive dess syfte och huvudfunktioner.

Innehåll
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
Beskriv hur Shapes-delen av projektet fungerar. Inkludera en lista över klasser och gränssnitt som är relaterade till Shapes.

Shapes Klasser
ShapeMenuFactory
ShapesMenu
Shape Entity
ParallelogramStrategy
RectangleStrategy
RhombusStrategy
TriangleStrategy
ShapeService
ShapeContext
Shapes Interface
IShapeContext
IMenuFactory
IShape
IShapeStrategy
IShapeDimensionsProvider

5. Calculator
Beskriv hur Calculator-delen av projektet fungerar. Inkludera en lista över klasser och gränssnitt som är relaterade till Calculator.

Calculator Klasser
CalculatorMenuFactory
CalculatorMenu
Calculator Entity
AdditionStrategy
DivisionStrategy
ModulusStrategy
MultiplicationStrategy
SquareRootStrategy
SubtractionStrategy
CalculatorService
CalculatorContext
Calculator Interface
ICalculatorContext
IMenuFactory
ICalculator
ICalculatorService
ICalculatorStrategy

6. Rock Paper Scissors Game
Om Rock Paper Scissors Game är en del av projektet, inkludera information om dess struktur och komponenter här.

7. Bygga och köra projektet
Ge instruktioner om hur man bygger och kör projektet lokalt på användarens maskin.

8. Licens
Inkludera licensinformation för ditt projekt.

9. Bidrag
Om du välkomnar bidrag från andra utvecklare, inkludera information om hur man kan bidra till ditt projekt.

10. Kontakta oss
Ge kontaktinformation för projektets underhållare eller bidragsgivare om någon behöver hjälp eller vill diskutera projektet.

Anpassa och lägg till ytterligare sektioner efter behov. Det viktigaste är att din README ger en tydlig och omfattande översikt över ditt projekt och dess olika komponenter.
