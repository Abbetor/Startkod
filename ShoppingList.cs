// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;
    private int budget;

    public int Count
    {
        get { return items.Count; }
    }

    public ShoppingList(string path, int budget)
    {
        this.path = path;
        this.budget = budget;
    }


    // Adds the item if it fits within the budget. Returns false if it does not.
    public bool Add(Item item)
    {
        if (Total() + item.Price > budget)
        {
            return false;
        }

        items.Add(item);
        return true;
    }


    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

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
            Console.WriteLine($"Saknar behörighet att spara listan: {ex.Message}");
        }
    }

    // Reads the file back into the list.
    public void Load()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Hittade ingen sparad lista, startar med en tom lista.");
            return;
        }

        string[] lines;

        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Kunde inte läsa listan: {ex.Message}");
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Saknar behörighet att läsa listan: {ex.Message}");
            return;
        }

        foreach (string line in lines)
        {
            string[] parts = line.Split(';', 2);

            if (parts.Length == 2 && int.TryParse(parts[0], out int price))
            {
                try
                {
                    if (!Add(new Item(parts[1], price)))
                    {
                        Console.WriteLine($"Hoppar över {parts[1]}, den får inte plats i budgeten.");
                    }

                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Hoppar över ogiltig rad: {line}");
                }
            }

        }
    }
}
