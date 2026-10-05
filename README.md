# Felen i programmet

## 1. Första felet:

#### Load funktionen försökte läsa den fjärde raden i items.txt men den behandlar den tomma raden som: pris;namn därför crashar den.

## Lösningen:

#### Ta bort den tomma raden i items.txt.

## 2 & 3. Andra och tredje felet :

#### 2. Andra felet Programmet crashar när man skriver in en bokstav när man egentligen ska skriva in ett nummer i else if (choice == 2) i Program.cs filen.

#### 3. Tredje felet Programmet craschar om man skriver in ett nummer som inte finns i listan.

### Lade till 3 olika felhanteringar :

### Första Felhanteringen :
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

#### Här kollar programmet om nummret som användaren skrivit in finns i listan:



```csharp
if (newnumber >= 0 && newnumber < list.Count)
list.RemoveAt(newnumber);
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