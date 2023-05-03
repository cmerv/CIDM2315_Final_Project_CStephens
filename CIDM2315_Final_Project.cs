namespace CIDM2315_Final_Project_CStephens;
using System;
using System.Linq;
using System.Collections.Generic;
class Hotel_Main
{
    static void Main(string[] args)
    {  
        // variables for later usage
        // login key dictionary for extensibility
        Dictionary<string,string> userDict = new Dictionary<string, string>();
        userDict.Add("alice","bob123");
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

        void ReadLogin()
        {
            Console.WriteLine("\n---Buff Hotel Login---");
            bool loginComplete = false;
            do {
            Console.WriteLine("---> Please input username:");
            string? username = Convert.ToString(Console.ReadLine());
            Console.WriteLine("---> Please input password");
            string? password = Convert.ToString(Console.ReadLine());
            if (username!=null&&password!=null){
            if (userDict.ContainsKey(username)&&userDict.ContainsValue(password))
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
        bool ShowAvailableRooms(int desiredCapacity) {
            bool areRoomsAvailable = false;
            Room.filteredRoomList.Clear();
            var filteredRooms = Room.roomList.Where(Room => Room.roomCapacity >= desiredCapacity);
                foreach(var Room in filteredRooms)
                {
                    Console.WriteLine($"++ Room Number: {Room.roomNum}, Room Capacity: {Room.roomCapacity}");
                    Room.filteredRoomList.Add(Room);
                }
            if (Room.filteredRoomList.Count > 0 ){
            Console.WriteLine($"---> Total available rooms: {Room.filteredRoomList.Count}");
                areRoomsAvailable=true;
                return areRoomsAvailable;
            }
            else
            {
                Console.WriteLine("---> Sorry, there are no available rooms of this capacity.");
                areRoomsAvailable=false;
                return areRoomsAvailable;
            }
        }

         void CheckIn() {
            Console.WriteLine("---> Input desired room capacity:");
            int desiredCapacity = Convert.ToInt16(Console.ReadLine());
            bool areRoomsAvailable = ShowAvailableRooms(desiredCapacity);
            if (areRoomsAvailable == true)
            {
            Console.WriteLine("---> Input desired room number:");
            int desiredRoom = Convert.ToInt16(Console.ReadLine());
            bool roomOnReservedList = Room.reservedRoomList.Any(Room => Room.roomNum == desiredRoom);
            if (roomOnReservedList==false) {
            Console.WriteLine("---> Input room guest name:");
            string? guestName = Console.ReadLine();
            Console.WriteLine("---> Input room guest email:");
            string? guestEmail = Console.ReadLine();
                foreach (var room in Room.filteredRoomList.Where(Room => Room.roomNum == desiredRoom))
                {
                    room.isRoomReserved = true;
                    room.RoomGuestEmail = guestEmail;
                    room.RoomGuestName = guestName;
                    Room.reservedRoomList.Add(room);
                    Room.roomList.Remove(room);
                    Console.WriteLine($"Checked in successfully! {room.RoomGuestName} with email {room.RoomGuestEmail} has been checked into Room {room.roomNum}.");
                    }
                }
                else if (roomOnReservedList==true) {
                Console.WriteLine($"---> Room is already reserved to another guest");
              }
              }
         }

        static void ShowReservedRooms() {
            Console.WriteLine("---Reserved Room List---");
            foreach (var room in Room.reservedRoomList) 
            {
                Console.WriteLine($"++ Room: {room.roomNum}; Guest: {room.RoomGuestName}");
            }
        }

        static void CheckOut() {
            Console.WriteLine("---> Please input the desired room number to check out:");
            int desiredRoom = Convert.ToInt16(Console.ReadLine());
            bool roomOnReservedList = Room.reservedRoomList.Any(Room => Room.roomNum == desiredRoom);
            if (roomOnReservedList==true)
            {
                var toBeRemovedList = Room.reservedRoomList.ToList();
                 foreach (var room in toBeRemovedList.Where(Room => Room.roomNum == desiredRoom)){
                    Console.WriteLine($"---> Room: {room.roomNum}; Guest {room.RoomGuestName}");
                    Console.WriteLine("---> Please confirm the room guest name and number. Input y to check out OR input any other key to cancel check out.");
                    string? response = Convert.ToString(Console.ReadLine());
                    if (response!=null)
                    {
                        if (response=="y")
                        {
                            roomOnReservedList=false;
                            room.isRoomReserved=false;
                            Room.roomList.Add(room);
                            Room.reservedRoomList.Remove(room);
                            ShowReservedRooms();
                            Console.WriteLine($"---> Check out is complete. Guest {room.RoomGuestName} has been checked out from {room.roomNum}.");
                        }
                        else 
                        {
                        }
                    }
                  }
                }
            else 
            {
                Console.WriteLine("---> Unable to check out. This room is not currently reserved by a guest.");
            }
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
