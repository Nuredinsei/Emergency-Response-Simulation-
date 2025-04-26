using System;
using System.Collections.Generic;

namespace EmergencyResponseSimulation
{
    // Abstract Class
    abstract class EmergencyUnit
    {
        public string Name { get; set; }
        public int Speed { get; set; }

        public EmergencyUnit(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public abstract bool CanHandle(string incidentType);
        public abstract void RespondToIncident(Incident incident);
    }

    // Police class
    class Police : EmergencyUnit
    {
        public Police(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Crime";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is responding to a crime at {incident.Location}.");
        }
    }

    // Firefighter class
    class Firefighter : EmergencyUnit
    {
        public Firefighter(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Fire";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is extinguishing a fire at {incident.Location}.");
        }
    }

    // Ambulance class
    class Ambulance : EmergencyUnit
    {
        public Ambulance(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Medical";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is treating patients at {incident.Location}.");
        }
    }

    // Incident Class
    class Incident
    {
        public string Type { get; set; }
        public string Location { get; set; }
        public string Difficulty { get; set; } // Still showing difficulty for display

        public Incident(string type, string location, string difficulty)
        {
            Type = type;
            Location = location;
            Difficulty = difficulty;
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int score = 0;
            List<EmergencyUnit> units = new List<EmergencyUnit>
            {
                new Police("Police Unit 1", 80),
                new Firefighter("Firefighter Unit 1", 70),
                new Ambulance("Ambulance Unit 1", 90)
            };

            string[] incidentTypes = { "Fire", "Crime", "Medical" };
            string[] locations = { "City Hall", "Downtown", "Suburbs", "Park", "Mall" };
            string[] difficultyLevels = { "Easy", "Medium", "Difficult" };

            for (int round = 1; round <= 5; round++)
            {
                Console.WriteLine($"\n--- Turn {round} ---");

                string randomType = incidentTypes[random.Next(incidentTypes.Length)];
                string randomLocation = locations[random.Next(locations.Length)];
                string randomDifficulty = difficultyLevels[random.Next(difficultyLevels.Length)];

                Incident incident = new Incident(randomType, randomLocation, randomDifficulty);

                Console.WriteLine($"Incident: {incident.Type} at {incident.Location} [Difficulty: {incident.Difficulty}]");

                bool handled = false;
                foreach (var unit in units)
                {
                    if (unit.CanHandle(incident.Type))
                    {
                        unit.RespondToIncident(incident);

                        Console.WriteLine($"+10 points");
                        score += 10;
                        handled = true;
                        break;
                    }
                }

                if (!handled)
                {
                    Console.WriteLine("No unit available to handle this incident.");
                    Console.WriteLine("-5 points");
                    score -= 5;
                }

                Console.WriteLine($"Current Score: {score}");
            }

            Console.WriteLine($"\nFinal Score: {score}");
            Console.WriteLine("Simulation Ended. Thank you for playing!");
        }
    }
}
