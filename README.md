# Felen i programmet

Uppgiften har sex stycken fel, men jag har delat upp några i flera delar:

Kraschar: Load(fel 1), meny/pris/nummer (fel 2, 4, 6), nummer som inte finns (fel 3 och 5),

Fel resultat: totalsumman (fel 8)

Döljda fel: tom catch (fel 10)

Fel 9 hör ihop med load felet.

## 1. Första felet:

#### Load funktionen försökte läsa den fjärde raden i items.txt men den behandlar den tomma raden som: pris;namn därför crashar den.

## Lösningen:

#### Load hoppar över rader som inte kan läsas istället för att det kommer krascha. Det checkas att raden har två olika delar och att priset är ett tal

```csharp
string[] parts = line.Split(';' 2);

if (parts.Length == 2 && int.TryParse(parts[0], out int price))
{
    items.Add(new Item(parts[1], price));
}
```


## 2 & 3. Andra och tredje felet :

#### FEL 2: Programmet crashar när man skriver in en bokstav när man egentligen ska skriva in ett nummer i else if (choice == 2) i Program.cs filen.



### Lade till 3 olika felhanteringar :

### FEL 2 FIX: 
```csharp
string number = Console.ReadLine();
if(!int.TryParse(number, out int _))
{
    Console.WriteLine("Du måste skriva ett nummer");
}
```
#### Här använder jag "!int.TryParse" för att kolla om det inte gå att parsa användarens input till en siffra med andra ord att användaren skriver en bokstav eller något annat tecken därefter skriver consolen:


```csharp
Console.WriteLine("Du måste skriva ett nummer");
```

### Andra & Tredje Felhanteringen :



#### Här kollas det om användarens input är en siffra är det en siffra så gör det till en variabel som heter newnumber med datatypen int:
```csharp
else if (int.TryParse(number, out int newnumber))
```

#### FEL 3: Programmet craschar om man skriver in ett nummer som inte finns i listan.

#### FIX: Här kollar programmet om nummret som användaren skrivit in finns i listan. Listan visas som 1 2 3 så kontrollen måste börja på 1 och sluta på list.Count:
```csharp
else if (newnumber >= 1 && newnumber <= list.Count)
{
    list.RemoveAt(newnumber);
}
```


#### Detta körs ifall man skriver ett nummer som inte finns i listan
```csharp
else
{
    Console.WriteLine("Nummret finns inte i listan");
}
```

## Fel Fyra & Fem :

#### Jag gjorde of den förra "int choice = int.tryparse(console.readline())"

#### Till:
```csharp
string choice = Console.ReadLine();
if(int.TryParse(choice, out int newchoice))
```
#### Fel 4, programmet crashar när man skriver en bokstav i menyn.

#### FIX: För att checka så att det användaren skrev in verkligen var en int och om inte körs:

```csharp
else
{
    Console.WriteLine("Du måste skriva en siffra");
}       
```

#### Fel 5, Skriver användaren ett nummer som inte finns så crashar programmet

#### FIX: Lägger en else indeterat på samma nivå som newchoice nivån.

```csharp
else
{
    Console.WriteLine("Du måste välja 1-5");
}
```

## Fel 6:

#### Programmet kraschar när manh skriver en bokstav som pris när man lägger till varor.

#### FIX: bytte ut int.Parse till int.Tryparse så att prgorammet skriver ett meddelande i stället för att krascha:

```csharp
string priceText = Console.ReadLine();

if (int.Tryparse(priceText out int price))
{
    list.Add(new Item(name, price));
}
else
{
    Console.WriteLine("Priset måste vara ett heltal");
}
```


### Fel 7:

#### Programmet craschar om txt filen inte finns 

#### FIX: load kollar först om filen finns och om den inte finns startar programmet med en tom lista.

```csharp
if (!File.Exists(path))
{
    Console.WriteLine("Hittade ingen sparad lista,startar med en tom lista.");
    return;
}
```

## Fel 8:

#### Total summan blir fel. Loopen i Total() började på 1 och hoppade därför över första varan.

#### FIX : Gjorde så att loopen börjar på 0:

```csharp
for (int i = 0; < items.Count; i++)
```

## FEL 9: 

#### sökningen av varor hittade inte varor som syns i listan. Filen har Windows radbrytningar (\r\n) och Split ('\n') lämande kvar ett osynligt \r efter namnet. "Mjölk" blev "Mjölk\r" och då hittades den inte.

#### Fix: jag la till File.ReadAllLines och File.WriteAllLines som, sköter radbrytningarna själv

```csharp
lines = File.ReadAllLines(path);
```

```csharp
File.WriteAllLines(path, lines);
```

## FEL 10:

#### Save hade tom catch. om det misslyckades med sparningne såg man inget fel och programmet skrev ändå "listan är sparad."

#### FIX : "Listan är sparad" skrivs ut bara när det lyckas. Catchen fångar specifika fel och skriver ut vad som gick fel

```csharp
try
{
    File.WriteAllLines(path, lines);
    Console.WriteLine("Listan är sparad.");
}
catch (IOException ex)
{
    Console.WriteLine($"Kunde inte spara listan: {ex.Message}");
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine($"Saknar behörighet att spara listan : {ex.Message}");
}
```

# Del 2: Item och Budget tak

## Item skyddar sig själv

#### Konstruktorn  i item kaster ett undantag om värdet är ogiltigt så att det aldrig kan skapes en trasing vara.

Tomt namn blir 'ArgumentException'

Negativt pris blir 'ArgumentOutOfRangeException'

#### Program.cs fångar undantagen och skriver meddelande. Load hoppar över ogiltiga rader i filen.
