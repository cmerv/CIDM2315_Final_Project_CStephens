namespace CIDM2315_Final_Project_CStephens;
using System;
using System.Linq;
using System.Collections.Generic;
class Hotel_Main
{
    static void Main(string[] args)
    {  
        // define and set rooms
        Room room101 = new Room(101, 2);
        Room room102 = new Room(102, 2);
        Room room103 = new Room(103, 2);
        Room room104 = new Room(104, 2);
        Room room105 = new Room(105, 2);
        Room room106 = new Room(106, 2);
        Room room107 = new Room(107, 3);
        Room room108 = new Room(108, 3);
        Room room109 = new Room(109, 3);
        Room room110 = new Room(110, 4);

        Console.Write("---CIDM 2315 Final Project: Christopher Stephens---\n---Welcome to Buff Hotel---\n");
        ReadLogin();

        static void ReadLogin()
        {
            Console.WriteLine("\n---Buff Hotel Login---");
            bool loginComplete = false;
            do {
            Console.WriteLine("---> Please input username:");
            string? username = Convert.ToString(Console.ReadLine());
            Console.WriteLine("---> Please input password");
            string? password = Convert.ToString(Console.ReadLine());
            if (username == "alice" && password == "bob123")
            {
                Console.WriteLine("\n---> Logged in Successfully");
                loginComplete = true;
                MainMenu();
            }
            else {
                Console.WriteLine("\n---> Invalid login entry. Please try again.");
                loginComplete = false;
            }
            }
            while (loginComplete==false);
        }
        static void MainMenu()
        {
            bool menuExit = false;
            Console.WriteLine();
            Console.WriteLine("****Buff Hotel Main Menu****");
            do {
            Console.WriteLine("---> Please select one of the options below:");
            Console.Write("\n1. Show available rooms\n2. Check in\n3. Show reserved rooms\n4. Check out\n5. Log out\n");
            var menuSelect = Convert.ToInt16(Console.ReadLine());
            switch (menuSelect)
            {
                default:
                Console.WriteLine("---> Invalid entry.");
                continue;
                case 1:
                // Show Available Rooms
                ShowAvailableRooms(0);
                continue;
                case 2:
                // Check in
                CheckIn();
                continue;
                case 3:
                // Show Reserved Rooms
                ShowReservedRooms();            
                continue;    
                case 4:
                // Check out
                CheckOut();
                continue;
                case 5:
                // Quit
                Console.Write("Logging off of system..");
                menuExit = true;
                break;
            }
        } while (menuExit == false);
        static void ShowAvailableRooms(int desiredCapacity) {
            Room.filteredRoomList.Clear();
            var filteredRooms = Room.roomList.Where(Room => Room.roomCapacity >= desiredCapacity);
                foreach(var Room in filteredRooms)
                {
                    Console.WriteLine($"++ Room Number: {Room.roomNum}, Room Capacity: {Room.roomCapacity}");
                    Room.filteredRoomList.Add(Room);
                }
            if (Room.filteredRoomList.Count > 0 ){
            Console.WriteLine($"---> Total available rooms: {Room.filteredRoomList.Count}");
            }
            else
            {
                Console.WriteLine("---> Sorry, there are no available rooms of this capacity.");
            }
        }

        static void CheckIn() {
            Console.WriteLine("---> Input desired room capacity:");
            int desiredCapacity = Convert.ToInt16(Console.ReadLine());
            ShowAvailableRooms(desiredCapacity);
            Console.WriteLine("---> Input desired room number:");
            int desiredRoom = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("---> Input room guest name:");
            string? guestName = Console.ReadLine();
            Console.WriteLine("---> Input room guest email:");
            string? guestEmail = Console.ReadLine();
            {
                foreach (var room in Room.filteredRoomList.Where(Room => Room.roomNum == desiredRoom))
                {
                    if (room.isRoomReserved==true)
                    {
                        Console.WriteLine($"Room is already reserved to another guest");
                        continue;
                    }
                    else {
                    room.isRoomReserved = true;
                    room.RoomGuestEmail = guestEmail;
                    room.RoomGuestName = guestName;
                    Room.reservedRoomList.Add(room);
                    Room.roomList.Remove(room);
                    Console.WriteLine($"Checked in successfully! {room.RoomGuestName} with email {room.RoomGuestEmail} has been checked into Room {room.roomNum}.");
                    }
                }
            }
         }

        static void ShowReservedRooms() {
            
        }

        static void CheckOut() {

        }
        
    }
    }
    public class Room {

        public static List<Room> filteredRoomList = new List<Room>();
        public static List<Room> roomList = new List<Room>();
        public static List<Room> reservedRoomList = new List<Room>();
        public int roomNum;
        public bool isRoomReserved;
        private string? roomGuestName;
        private string? roomGuestEmail;
        public int roomCapacity;
        public string? RoomGuestName
        {
            get; set;
        }

        public string? RoomGuestEmail
        {
            get; set;
        }

        public Room(int initRoomNum, int initRoomCapacity)
        {
            roomNum = initRoomNum;
            roomCapacity = initRoomCapacity;
            roomList.Add(this);
            isRoomReserved = false;
            roomGuestEmail = null;
            roomGuestName = null;
        }
    }
}
