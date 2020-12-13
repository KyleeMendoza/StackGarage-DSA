﻿using System;
using System.Collections.Generic;

namespace GarageLaptop
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] CarList = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }; 
            //string[] CarList = { "aaa 000", "bbb 111", "ccc 222", "ddd 333", "eee 444", "fff 555", "ggg 666", "hhh 777", "iii 888", "jjj 999" }; //List ng cars

            List<string> Garage = new List<string>(); //List ng slots sa garage
            List<string> History = new List<string>(); //List ng garage history
            //Ito yung // two dimentinal int array {arrival, departure} na nakabase sa positioning ng CarList.
            int[,] CarRecord = new int[10, 2] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, };

            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("------ MattKyleRichard Car Collection ------");
                Console.WriteLine("1.Arrival\n2.Departure\n3.Check Garage\n4.Garage History\n5.Vehicle History");
                Console.Write("List of Registered Cars: ");
                Console.WriteLine(String.Join(", ", CarList));
                Console.WriteLine($"Garage Available Space: {10 - Garage.Count}");
                Console.WriteLine("\nChoice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1) //ARRIVAL
                {
                    Console.WriteLine("\nEnter Model: ");
                    string model = Console.ReadLine();
                    int carIndex = Array.FindIndex(CarList, w => w.Contains(model)); //Index ng Car sa Carlist
                    bool checker = Array.Exists(CarList, element => element == model); //checker kung nageexist ung car sa Carlist
                    if (checker)
                    {
                        bool alreadyExist = Garage.Contains(model); //checker kung nagexxist na ba ung car sa garage
                        if (alreadyExist)
                        {
                            Console.WriteLine("Car already exist in garage! ");
                            //checker++;
                        }
                        else
                        {
                            Garage.Add(model);
                            History.Add($"{model} has arrived.");
                            CarRecord[carIndex, 0]++; // arrival
                            //checker++;
                        }
                    }
                }
                else if (choice == 2) //DEPARTURE
                {
                    Console.WriteLine("\nEnter Model: ");
                    string model = Console.ReadLine();
                    bool checker = Array.Exists(CarList, element => element == model); //checker kung nageexist ung car sa Carlist
                    int carIndex = Garage.IndexOf(model); //Index ng Car sa garage
                    if (checker)
                    {
                        if (carIndex+1 == Garage.Count) //Checker kung blocked ba yung daan o hindi
                        {
                            History.Add($"{model} has left.");
                            Garage.RemoveAll(x => ((string)x) == model); //lambda expression pwede ding Garage.RemoveAt(-index-)
                            Console.WriteLine(model + " has departed from the garage...");
                            int keyIndex = Array.FindIndex(CarList, w => w.Contains(model));
                            CarRecord[keyIndex, 1]++; // departure
                        }
                        else
                        {
                            Garage.RemoveAll(x => ((string)x) == model);
                            Console.WriteLine(model + " has departed from the garage...");
                            int keyIndex = Array.FindIndex(CarList, w => w.Contains(model));
                            CarRecord[keyIndex, 1]++; // departure

                            for (int j = Garage.Count - 1; j >= carIndex; j--) //for loop na aalis ung nasa harap
                            {
                                History.Add($"{Garage[j]} has left.");
                                int departIndex = Array.FindIndex(CarList, w => w.Contains(Garage[j]));
                                CarRecord[departIndex, 1]++; // departure
                            }

                            History.Add($"{model} has left.");
                            for (int k = carIndex; k < Garage.Count; k++) //for loop na unang babalik ung huling lumabas
                            {
                                History.Add($"{Garage[k]} has arrived.");
                                int arriveIndex = Array.FindIndex(CarList, w => w.Contains(Garage[k]));
                                CarRecord[arriveIndex, 0]++; // arrival
                            }
                        }
                    }
                }
                else if (choice == 3)//GARAGE CHECK
                {
                    //Displays = Slot#: Vehicle
                    int slot = 1;
                    foreach (string car in Garage)
                    {
                        Console.WriteLine($"Slot{slot}: {car}");
                        slot++;
                    }
                    if (slot != 10)
                    {
                        for (int i = slot; i < 11; i++)
                        {
                            Console.WriteLine($"Slot{i}: ");
                        }
                    }
                }
                else if (choice == 4)//GARAGE HISTORY
                {
                    foreach (string i in History)
                    {
                        Console.WriteLine(i);
                    }
                }
                else if (choice == 5) //Vehicle History
                {
                    for (int i = 0; i < CarList.Length; i++)
                    {
                        Console.WriteLine($"--{CarList[i]}--");
                        Console.WriteLine($"Arrival: {CarRecord[i, 0]} Departure: {CarRecord[i, 1]}"); //format ng arrival/departure
                    }
                }
                else
                {
                    Console.WriteLine("INVALID ENTRY");
                }
                Console.WriteLine("Continue? Press any key to continue, N or n to exit:");

                var userWantsToContinue = Console.ReadLine();

                keepGoing = userWantsToContinue?.ToUpper() != "N";
                Console.Clear();
            }
        }
    }
}
