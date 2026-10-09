ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    string choice = Console.ReadLine();
    if (int.TryParse(choice, out int newchoice))
    {
        if (newchoice == 1)
        {
            Console.Write("Namn: ");
            string name = Console.ReadLine();
            Console.Write("Pris: ");
            string priceText = Console.ReadLine();

            if (int.TryParse(priceText, out int price))
            {
                list.Add(new Item(name, price));
            }
            else
            {
                Console.WriteLine("Priset måste vara ett heltal");
            }
        }
        else if (newchoice == 2)
        {
            Console.Write("Nummer: ");
            string number = Console.ReadLine();

            if (!int.TryParse(number, out int newnumber))
            {
                Console.WriteLine("Du måste skriva ett nummer");
            }
            else if (newnumber >= 1 && newnumber <= list.Count)
            {
                list.RemoveAt(newnumber);
            }
            else
            {
                Console.WriteLine("Nummret finns inte i listan");
            }
        }
        else if (newchoice == 3)
        {
            list.Save();
        }
        else if (newchoice == 4)
        {
            Console.Write("Namn att söka efter: ");
            string wanted = Console.ReadLine();
            Item found = list.Find(wanted);

            if (found == null)
            {
                Console.WriteLine("Varan finns inte i listan.");
            }
            else
            {
                Console.WriteLine($"Hittade: {found}");
            }
        }
        else if (newchoice == 5)
        {
            break;
        }
        else
        {
            Console.WriteLine("Du måste välja 1-5");
        }
    }
    else
    {
        Console.WriteLine("Du måste skriva en siffra");
    }
}
