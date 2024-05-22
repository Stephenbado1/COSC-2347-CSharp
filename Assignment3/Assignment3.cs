/*
    A program using classes and inheritance to declare and instantiate 
    different building types and perform various operations on each type
*/

#nullable disable
using static System.Console;

//main method creating building types and performing actions on them
class Program
{
    public static void Main(string[] args)
    {
        //Build the requested buidings
        SingleFamilyHome familyHome1 = new("123 Spring Lane");
        Warehouse warehouse1 = new("2000 Industrial Blvd");
        TownHome townHome1 = new("212 Lake Road");

        //turn on AC for all buildings
        WriteLine("Attempting to turn on AC for all buildings:");
        WriteLine("-------------------------------------------");
        familyHome1.TurnOnAC();
        warehouse1.TurnOnAC();
        townHome1.TurnOnAC();


        //Respond to fires at each building
        WriteLine("\n\nEvery building has caught on fire!!");
        WriteLine("-----------------------------------");
        familyHome1.FireResponse();
        warehouse1.FireResponse();
        townHome1.FireResponse();

        // Report status of each building object
        WriteLine("\n\nStatus report for each building:");
        WriteLine("--------------------------------");
        familyHome1.Status();
        warehouse1.Status();
        townHome1.Status();
    }
}

//Building is base class with all instance variables and base methods
class Building
{
    public string Name
    { get; set; }

    public string Color
    { get; set; }

    public string Address
    { get; set; }

    public int Toilets
    { get; set; }

    public int Doors
    { get; set; }

    public bool HasSprinklers
    { get; set; }

    public bool HasAC
    { get; set; }

    //Status method returns all variables of Building
    public void Status()
    {
        WriteLine($"Building Type: {Name} \nAddress: {Address} \nColor: {Color} \nToilets: {Toilets} \nDoors: {Doors}" +
                  $" \nSprinklers: {(HasSprinklers ? "Yes" : "No")} \nAC: {(HasAC ? "Yes" : "No")}\n\n");
    }

    //TurnOnAC checks if Building has AC and responds appropriately
    public void TurnOnAC()
    {
        WriteLine(HasAC ? $"{Name} AC has been turned on." : $"{Name} does not have AC.");
    }

    //FireResponse checks if Building has sprinklers and responds appropriately
    public void FireResponse()
    {
        WriteLine(HasSprinklers ? $"{Name} is on fire, sprinklers have been turned on!" : $"{Name} is on fire, does not have sprinklers. Time to call the insurance company!");
    }
}



//Residential is subclass of Building which will extend to TownHome and SingleFamilyHome
class Residential : Building
{
    public Residential()
    {
        HasAC = true;
        HasSprinklers = true;
    }
}

//Commercial is subclass of Building which will extend to Warehouse, Office, and Factory
class Commercial : Building
{
    public Commercial()
    {
        Color = "Grey";
        HasSprinklers = false;
    }
}



//Townhome is subclass of Residential
class TownHome : Residential
{
    public TownHome(string address)
    {
        Name = "Town Home";
        Color = "Red";
        Address = address;
        Toilets = 4;
        Doors = 1;
        WriteLine($"New Town Home built!\n");
    }
}

//SingleFamilyHome is subclass of Residential
class SingleFamilyHome : Residential
{
    public SingleFamilyHome(string address)
    {
        Name = "Single Family Home";
        Color = "Blue";
        Address = address;
        Toilets = 1;
        Doors = 2;
        WriteLine($"New Single Family Home built!\n");
    }
}

//Warehouse is subclass of Commercial
class Warehouse : Commercial
{
    public Warehouse(string address)
    {
        Name = "Warehouse";
        Address = address;
        HasAC = false;
        Toilets = 0;
        Doors = 4;
        WriteLine($"New Warehouse built!\n");
    }
}

//Office is subclass of Commercial
class Office : Commercial
{
    public Office(string address)
    {
        Name = "Office";
        Address = address;
        HasAC = true;
        Toilets = 0;
        Doors = 4;
        WriteLine($"New Office built!\n");
    }
}

//Factory is subclass of Commercial
class Factory : Commercial
{
    public Factory(string address)
    {
        Name = "Factory";
        Address = address;
        HasAC = true;
        Toilets = 2;
        Doors = 6;
        WriteLine($"New Factory built!\n");
    }
}