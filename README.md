Dag 1

Jag började med att läsa igenom alla krav, skapade en console app, la till en mapp för menyer samt en app klass för att lämna program.cs.
Efter det gjorde jag ett library.

Sedan laddade jag ned de nödvändiga nuget paketen inklusive autofac och började implementera det.

Jag började skissa och skapa klasserna som ska innehålla meny för huvudmeny, sten sax påse, miniräknare och shapes.
Därefter la jag in metoden ShowMenu i MainMenu klassen, ShowCalculatorMenu i CalculatorMenu klassen, ShowRockPaperScissorMenu i RockPaperScissor klassen, ShowShapesMenu i ShapesMenu klassen.

Efter att grundarbetet lagts upp började jag skriva i program.cs min tankeprocess och vilka tabeller samt attribut jag skulle behöva.

Jag skapade en mapp CalculatorStrategyService och StrategyContext för att förbereda strategy pattern till först miniräknaren och senare shapes.
Jag skapade en ICalculatorStrategy och IShapeStrategy interface. Första implementeringen blev Calculate metod i ICalculatorStrategy.
Efter det började jag lägga in alla beräknings-klasser i CalculatorStrategyService mappen samt la in CalculatorContext i StrategyContext mappen.
Sedan testade jag alla beräkningar efter jag la in dom i CalculatorMenu klassens meny metod.
