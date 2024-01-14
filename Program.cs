namespace Program {

    public class Setup {
        public static int GetStarterID() {
            string[] fileInput = File.ReadAllLines("CurrentId.txt");
            return int.Parse(fileInput[fileInput.Length-1]);
        }
    }

    
    public class Entry {
        // Figured out how to give each entry a unique id by looking at an old Java program of mine.
        // Probably got it from Dr. Halterman.
        private static int counter = Setup.GetStarterID();
        private int id;
        private string logDate;
        private string logText;

        public Entry(string today, string text) {
            id = counter;
            counter++;
            logDate = today;
            logText = text;
        } 

        public int getId() {
            return id;
        }
        public string getText() {
            return logText;
        }
        public string getLogDate() {
            return logDate;
        }

    }

    public class Program {
        private static readonly string filename = "log.csv";

        
        private static int ExecAdd() {
            // entries[0] = "hi!";
            // for (int i = 0; i < entries.Length; i++) {
            //     Console.WriteLine($"Entry {i} is {entries[i]}");
            // }
            Entry newEntry;
            Console.Write("Enter your note: ");
            string thisDay = DateTime.Today.ToString("d");
            string userText = Console.ReadLine() ?? "err";
            if (userText != "err") {
                newEntry = new Entry(thisDay, userText);
                string logText = $"{newEntry.getId()}, {newEntry.getLogDate()}, {newEntry.getText()}\r\n";
                File.AppendAllText(filename, logText);
                File.WriteAllText("CurrentId.txt", $"{newEntry.getId()+1}");
                return 0;
            } else {
                return 1;
            }
        }    

        private static int ExecDisplay() {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines) {
                Console.WriteLine(line);
            }
            return 0;
        }

        private static void ParseInput(string input) {
            // Add a 'positions' hashmap here
            string[] splitInput = input.Split(' ');
            string cmd = splitInput[0];
            int status = 1;
            switch (cmd) {
                case "add":
                    status = ExecAdd();
                    break;
                case "view":
                    status = ExecDisplay();
                    break;
                
            }
            
            if (status == 0) {
                Console.WriteLine("Command completed successfully.");
            } else {
                Console.WriteLine("An error occured; please try again.");
            }
        }

        public static void Main() {
            string msg = "Please enter a command: ";
            Console.Write(msg);
            string input = Console.ReadLine() ?? "err";

            while (input != "q" && input != "err")  {
                ParseInput(input);
                Console.Write(msg);
                input = Console.ReadLine() ?? "err";
                Console.WriteLine();
            }
        }
    }
}