Dag 1

Jag började med att läsa igenom alla krav, skapade en console app, la till en mapp för menyer samt en app klass för att lämna program.cs.
Efter det gjorde jag ett library.

Sedan laddade jag ned de nödvändiga nuget paketen inklusive Autofac och började implementera det.

Jag började skissa och skapa klasserna som ska innehålla meny för huvudmeny, sten sax påse, miniräknare och shapes.
Därefter la jag in metoden ShowMenu i MainMenu klassen, ShowCalculatorMenu i CalculatorMenu klassen, ShowRockPaperScissorMenu i RockPaperScissor klassen, ShowShapesMenu i ShapesMenu klassen.

Efter att grundarbetet lagts upp började jag skriva i program.cs min tankeprocess och vilka tabeller samt attribut jag skulle behöva.

Jag skapade en mapp CalculatorStrategyService och StrategyContext för att förbereda strategy pattern till först miniräknaren och senare shapes.
Jag skapade en ICalculatorStrategy och IShapeStrategy interface. Första implementeringen blev Calculate metod i ICalculatorStrategy.
Efter det började jag lägga in alla beräknings-klasser i CalculatorStrategyService mappen samt la in CalculatorContext i StrategyContext mappen.
Sedan testade jag alla beräkningar efter jag la in dom i CalculatorMenu klassens meny metod.

Dag 2

Jag kollade mer i detalj på vilka entiteter och attribut jag skulle behöva och skrev upp dessa, sedan implementerades de i min Models mapp i mitt library.

Jag började lägga till mapparna för Shapes Strategy.
Jag implementerade metoderna och strategies för Shapes, började dependency injecta med Autofac och la till fler relevanta interfaces.

Uppdaterade starten av appen och såg till att relevanta klasser blev resolved med Autofac och la till ShapeTypes i IShapeStrategy som kommer att användas för att spara ned vilken form som användas i SSMS.

Dag 3

Jag hade tänkt dagen innan på hur jag skulle implementera CRUD operationerna för calculations och idag var det dags.

Jag uppdaterade min meny i CalculatorMenu, skapade en CalculatorService klass för att hålla i metoderna som skulle anropas i CalculatorMenu klassen.
Jag uppdaterade CalculatorContext för att hantera CRUD som skulle anropas i CalculatorService klassen.

Fixade även IsActive attribut som jag hade glömt till alla mina entiteter!

Dag 4

La till IShapeService, ShapeService och började implementera CRUD för Shapes.

Sedan la jag till en GameService klass som skulle hålla alla metoder för spelet att fungera.
Skapade en GameHistory entitet, la till relevanta attributer.
Började skapa metoderna i GameService, exempelvis PlayGame, ViewPreviousGames, GameRules.

Dag 5

Jag utvecklade felhanteringen för calculations, speciellt upphöjt till delen. Jag hade gjort alla nummer och resultat till decimal och det
gav problem med overflow och för stora nummer. Tillslut hittade jag en lösning, om numret är för högt eller lågt blir resultatet 0, om resultatet är 0 så får användaren
felmeddelande om att det antingen var för stort eller för litet.

Jag funderade ut hur min seeding skulle se ut och skapade en DataSeeding klass som skulle innehålla relevant seeding. Skapade först seeding för de 4 shapes.

Efter det skapade jag seedingen för alla 6 operatorer i calculations.

Dag 6

La till färger på alla menyerna, tyckte att darkcyan passade bra.
Utvecklade calculator create delen, tyckte att det hade varit skönt att kunna lämna beräkningen mitt i och bestämde mig för att inkludera en exit option som kan ta en tillbaka till calculator menyn
om användaren önskar det.

Började kolla runt efter mindre fel i appen och märkte att min seeding var i min huvudloop, det kanske inte är så illa i sig eftersom den inte seedar det som redan finns men det innebär
att den kommer att kontinuerligt försöka seeda. Jag bestämde mig för att flytta ut den ur loopen så att den enbart körs en gång när appen startar och aldrig igen.

Letade efter fel i consolen och hittade en smidig lösning med mina shapes och strategies, möjligheten jag ville var att kunden ska kunna gå ur shape uträkningen mitt i ett tal
löste det till slut genom att sätta tidigare dimensioner till 0 som i sin tur skrevs i en if sats som säkerställde att inget sparas om något relevant värde är 0.

Jag bestämde mig för att gå ur min comfortzone och försökte implementera factory pattern till mina menyer. Det var hackigt att få det att funka med autofac, fick många errors.
Tillslut fick jag ta bort min meny från huvudprojektet och lägga det i mitt library där jag registrerar mina autofacs. Efter mycket felsökning och debugging lyckades jag få factory pattern att fungera!

Implementerade Singleton pattern genom Autofacs inbygga "SingleInstance" för menyerna.

Dag 7

Korrigerade min CalculatorStrategy, hade råkat lägga in upphöjt till när det skulle vara roten ur. Uppdaterade även (R) på calculator & shapes för att visa 2 decimaler.

Uppdaterade Shape och Calculator delete metoderna, hade placeholder som permanently deletade, nu fixat så att det är enligt standard dvs IsActive = 0 om något blir deletat.
Fixade relaterade metoder så att de var anpassade till detta och så att inga oväntade fel kan ske.

Dag 8

Har haft problem med min miniräknare strategy pattern mest kring att roten ur behöver bara ett tal. Kollade runt och hittade tillslut en lösning om att göra number2 nullable. Jag uppdaterade relevanta strategies och interfaces.
Efter flertal error fick jag det att funka. Huvudanledningen till att jag ville göra detta var pga i databasen vid uträkningen av roten ur, stod det exempelvis "number 1 = 25", "number 2 = 0", "result = 5" men jag ville att number 2 skulle 
då inte ha ett värde, dvs NULL. Löst!

Jag gjorde likadant på shapes eftersom SideLength propertyn inte alltid används, exempelvis vid rektangel uträkning. Ändrade koden till att låta sidelength vara null när det behövs.
