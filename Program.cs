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
        private List<Train> _trains = new List<Train>();

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

            Train train = new Train(direction, passengers, CreateList(passengers));
            _trains.Add(train);

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

            string departurePoint;
            string arrivalPoint;

            do
            {
                ReadCorrectPoint(out departurePoint, out arrivalPoint);

                Console.WriteLine($"Поезд отправляется из {departurePoint} в {arrivalPoint}\n" +
                    $"Если данные введены верно, нажмите {commandExit}");
                userInput = Console.ReadLine();
            }
            while (commandExit != userInput);

            return new Direction(departurePoint, arrivalPoint);
        }

        private void ReadCorrectPoint(out string departurePoint, out string arrivalPoint)
        {
            do
            {
                Console.WriteLine("Введите точку отправления поезда:");
                departurePoint = Console.ReadLine();

                Console.WriteLine("Введите точку прибытия поезда:");
                arrivalPoint = Console.ReadLine();
            }
            while (departurePoint == arrivalPoint);
        }

        private void ShowAllDirections()
        {
            foreach (var train in _trains)
                train.ShowStats();
        }
        public List<RailwayCarriage> CreateList(int passengers)
        {
            List<RailwayCarriage> railwayCarriages = new List<RailwayCarriage>();

            RailwayCarriage railwayCarriage = new RailwayCarriage();

            int numberRailwayCarriage = passengers / railwayCarriage.GetCapacity();
            int remainderNumberRailwayCarriage = passengers % railwayCarriage.GetCapacity();

            if (remainderNumberRailwayCarriage > 0)
                numberRailwayCarriage += 1;

            for (int i = 0; i < numberRailwayCarriage; i++)
                railwayCarriages.Add(railwayCarriage);

            return railwayCarriages;
        }
    }

    class Train
    {
        private Direction _direction;
        private int _passengers;
        private List<RailwayCarriage> _railwayCarriages;

        public Train(Direction direction, int passengers, List<RailwayCarriage> railwayCarriages)
        {
            _direction = direction;
            _passengers = passengers;
            _railwayCarriages = railwayCarriages;
        }

        public void ShowStats()
        {
            _direction.ShowStats();
            Console.WriteLine($"Билетов продано: {_passengers}.\n" +
                $"{_railwayCarriages.Count} вагонов прицеплено.\n");
        }
    }

    class RailwayCarriage
    {
        private int _capacity = 50;

        public int GetCapacity()
        {
            return _capacity;
        }
    }

    class Direction
    {
        private string _departurePoint;
        private string _arrivalPoint;

        public Direction(string departurePoint, string arrivalPoint)
        {
            _departurePoint = departurePoint;
            _arrivalPoint = arrivalPoint;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Поезд отправляется из {_departurePoint} в {_arrivalPoint}");
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
