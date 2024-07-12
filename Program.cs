using System;
using System.Collections.Generic;

namespace PassengerTrainConfigurator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.Work();
        }
    }

    class Dispatcher
    {
        List<Train> trains = new List<Train>();

        public void Work()
        {
            const string CommandCreateTrain = "1";
            const string CommandExit = "exit";

            bool isRunning = true;

            while (isRunning)
            {
                ShowAllDirections();

                Console.WriteLine($"Введите {CommandCreateTrain}, чтобы создать поезд.\n" +
                    $"Введите {CommandExit}, чтобы выйти.");
                string userInput = Console.ReadLine();

                Console.Clear();

                switch (userInput)
                {
                    case CommandCreateTrain:
                        CreateTrain();
                        break;

                    case CommandExit:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет.");
                        break;
                }

                Console.Clear();
            }

            Console.Clear();
            Console.WriteLine("Вы вышли из программы.");
        }

        private void CreateTrain()
        {
            Direction direction = CreateDirection();
            int passengers = SellT​tickets();

            Train train = new Train(direction, passengers);
            trains.Add(train);

            Console.Clear();

            Console.WriteLine("Создан поезд:");
            train.ShowStats();
            Console.ReadLine();
        }

        private int SellT​tickets()
        {
            int minRandomSellTickets = 100;
            int maxRandomSellTickets = 501;

            int tikects = UserUtils.GenerateRandomNumber(minRandomSellTickets, maxRandomSellTickets);

            return tikects;
        }

        private Direction CreateDirection()
        {
            string commandExit = "1";
            string userInput;

            string pointOfDeparture;
            string arrivalPoint;

            do
            {
                Console.WriteLine("Введите точку отправления поезда:");
                pointOfDeparture = Console.ReadLine();

                Console.WriteLine("Введите точку прибытия поезда:");
                arrivalPoint = Console.ReadLine();

                Console.WriteLine($"Поезд отправляется из {pointOfDeparture} в {arrivalPoint}\n" +
                    $"Если данные введены верно, нажмите {commandExit}");
                userInput = Console.ReadLine();
            }
            while (commandExit != userInput);

            return new Direction(pointOfDeparture, arrivalPoint);
        }

        private void ShowAllDirections()
        {
            foreach (var train in trains)
                train.ShowStats();
        }
    }

    class Train
    {
        private Direction _direction;
        private int _passengers;
        private List<railwayCarriage> _railwayCarriages;

        public Train(Direction direction, int passengers)
        {
            _direction = direction;
            _passengers = passengers;
            _railwayCarriages = CreateList(_passengers);
        }

        private List<railwayCarriage> CreateList(int passengers)
        {
            List<railwayCarriage> railwayCarriages = new List<railwayCarriage>();

            int numberRailwayCarriage = passengers / railwayCarriage.GetCapacity();
            int remainderNumberRailwayCarriage = passengers % railwayCarriage.GetCapacity();

            if (remainderNumberRailwayCarriage > 0)
                numberRailwayCarriage += 1;

            for (int i = 0; i < numberRailwayCarriage; i++)
                railwayCarriages.Add(new railwayCarriage());

            return railwayCarriages;
        }

        public void ShowStats()
        {
            _direction.ShowStats();
            Console.WriteLine($"Билетов продано: {_passengers}.\n" +
                $"{_railwayCarriages.Count} вагонов прицеплено.\n");
        }
    }

    class railwayCarriage
    {
        private static int _capacity = 50;

        public static int GetCapacity()
        {
            return _capacity;
        }
    }

    class Direction
    {
        private string _pointOfDeparture;
        private string _arrivalPoint;

        public Direction(string pointOfDeparture, string arrivalPoint)
        {
            _pointOfDeparture = pointOfDeparture;
            _arrivalPoint = arrivalPoint;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Поезд отправляется из {_pointOfDeparture} в {_arrivalPoint}");
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
