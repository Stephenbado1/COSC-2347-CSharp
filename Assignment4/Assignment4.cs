/*
    Zoo Manager Program to read an input file and store contents as separate events in a Queue.
    Queue is then used to execute events on a list of zoo animal objects.
*/
#nullable disable
using static System.Console;

class Program
{
    //Main method creates Queue and passes to Processing method
    public static void Main(string[] args)
    {
        Queue<string> zooEvents = ReadFile();
        Process(zooEvents);
    }


    //ReadFile method reads a file and stores each line as an element in a Queue of strings
    static Queue<string> ReadFile()
    {
        Queue<string> fileEvents = new();

        //create FileStream to read input file
        FileStream stream = new("/Users/stephenbadeaux/Desktop/C#/Assignment4/AS4 input.txt", FileMode.Open);

        //create streamreader and pass it the filestream you created above
        StreamReader reader = new(stream);

        //read events from file until last line is reached
        while (true)
        {
            //This will read 1 line at a time from the file, breaks at end of file (null)
            string inputString = reader.ReadLine();

            if (inputString == null) break;

            fileEvents.Enqueue(inputString);
        }

        //close reader and return Queue
        reader.Dispose();
        return fileEvents;
    }


    //Process method iterates through events in Queue and handles each accordingly
    static void Process(Queue<string> events)
    {
        List<Animal> animals = new();

        foreach (string zooEvent in events)
        {
            //split event into separate strings for processing
            string[] e = zooEvent.Split(" ");

            //run event name(first index) through switch case for possible events
            switch (e[0])
            {
                //Birth case gets Animal Type and births accordingly
                case "Birth":

                    string animal = e[1];

                    switch (animal)
                    {
                        case "Monkey":
                            {
                                Monkey newAnimal = new();
                                animals.Add(newAnimal);
                                break;
                            }
                        case "Lion":
                            {
                                Lion newAnimal = new();
                                animals.Add(newAnimal);
                                break;
                            }
                        case "Alligator":
                            {
                                Alligator newAnimal = new();
                                animals.Add(newAnimal);
                                break;
                            }
                        case "Predator":
                            {
                                Predator newAnimal = new();
                                animals.Add(newAnimal);
                                break;
                            }
                    }
                    break;

                //Feeding case creates list of all alive animals that can feed and feeds them, special handling for predator feeding
                case "Feeding":

                    List<Animal> feedingAnimals = animals.FindAll(x => x.Alive == true);
                    List<Animal> preyAnimals = feedingAnimals.FindAll(x => x.AnimalName != "Predator");

                    feedingAnimals.ForEach(i => i.Feeding());

                    if (feedingAnimals.OfType<Predator>().Any())
                    {
                        preyAnimals.ForEach(i => i.Death());
                    }
                    break;

                //Plague case creates list of animals that are alive and matching Classification of Plague event and kills them
                case "Plague":

                    List<Animal> plagueVictims = animals.FindAll(x => x.Classification == e[1] && x.Alive == true);
                    plagueVictims.ForEach(i => i.Plague());
                    break;

                //Death case finds first animal of the requested Type and kills it
                case "Death":
                    Animal toDie = animals.Find(x => x.AnimalName == e[1] && x.Alive == true);
                    toDie.Death();
                    break;

                //Fight case finds first animal of each Type and passes them to Fight method of Animal
                case "Fight":
                    Animal fighter1 = animals.Find(x => x.AnimalName == e[1] && x.Alive == true);
                    Animal fighter2 = animals.Find(x => x.AnimalName == e[2] && x.Alive == true);
                    Fight(fighter1, fighter2);
                    break;

                //Sunrise case increments the static day field in Animal
                case "Sunrise":
                    Animal.Sunrise();
                    break;
            }
        }
    }

    //Fight method takes two animals and determines which wins in a fight based on strength
    public static void Fight(Animal animal1, Animal animal2)
    {
        WriteLine($"There was a fight between {animal1.AnimalName} and {animal2.AnimalName}.");

        if (animal1.Strength > animal2.Strength)
        {
            animal2.Death();
        }
        else
        {
            animal1.Death();
        }
    }

    //Animal is base abstract class with all required variables and methods
    public abstract class Animal
    {
        public static DateOnly currentDay = DateOnly.FromDateTime(DateTime.Now);

        public string AnimalName
        { get; set; }

        public string Classification
        { get; set; }

        public string FavoriteFood
        { get; set; }

        public int Strength
        { get; set; }

        public string BirthDate
        { get; set; }

        public string DeathDate
        { get; set; }

        public bool Alive
        { get; set; }

        //Birth method displays what animal was born, modifies BirthDate and Alive bool
        public void Birth()
        {
            WriteLine($"{AnimalName} was born on {currentDay}!");
            BirthDate = currentDay.ToString();
            Alive = true;
        }

        //Feeding method displays what animal is feeding
        public void Feeding()
        {
            WriteLine($"{AnimalName} eats their favorite food {FavoriteFood}.");
        }

        //Plague method displays what animal is afflicted, modifies DeathDate and Alive bool
        public void Plague()
        {
            WriteLine($"{AnimalName} caught the plague and died on {currentDay}.");
            DeathDate = currentDay.ToString();
            Alive = false;
        }

        //Sunrise method increases currentDay and displays
        public static void Sunrise()
        {
            DateOnly newDay = currentDay.AddDays(1);
            currentDay = newDay;
            WriteLine($"New day: {currentDay}");
        }

        //Death method shows what animal has died
        public void Death()
        {
            WriteLine($"{AnimalName} died on {currentDay}.");
            DeathDate = currentDay.ToString();
            Alive = false;
        }
    }

    //Monkey class inherits Animal base class
    public class Monkey : Animal
    {
        public Monkey()
        {
            AnimalName = "Monkey";
            Classification = "Mammal";
            FavoriteFood = "Banana";
            Strength = 100;
            Birth();
        }
    }

    //Lion class inherits Animal base class
    public class Lion : Animal
    {
        public Lion()
        {
            AnimalName = "Lion";
            Classification = "Mammal";
            FavoriteFood = "Deer";
            Strength = 200;
            Birth();
        }
    }

    //Alligator class inherits Animal base class
    public class Alligator : Animal
    {
        public Alligator()
        {
            AnimalName = "Alligator";
            Classification = "Reptile";
            FavoriteFood = "Snakes";
            Strength = 250;
            Birth();
        }
    }

    //Predator class inherits Animal base class
    public class Predator : Animal
    {
        public Predator()
        {
            AnimalName = "Predator";
            Classification = "Extraterrestrial";
            FavoriteFood = "Everything";
            Strength = 10000;
            Birth();
        }
    }
}