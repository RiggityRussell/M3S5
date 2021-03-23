using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FInchRussell
{
    #region COMMAND ENUM
    /// <summary>
    /// User commands called from ENUM
    /// </summary>

    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        MAKENOISE,
        STOPNOISE,
        ALLSTUFF,
        STOPALLSTUFF,
        DONE
    }
    #endregion
    class Program
    {
        // **********************************************************************
        // *Title:              Finch M3S5                                      *    
        // *Application Type:   Console, Finch                                  *
        // *Author:             Arlt, Russell                                   *
        // *Description:        Many Applications                               *
        // *Date Created:       3/20/2021                                       *
        // *Date Revised:       Constantly                                      *
        // **********************************************************************

        #region MAIN
        /// <summary>
        /// The first screen
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplaySetTheme();
            //SetTheme();
            DisplayLoginRegister();
            DisplayWelcomeScreen();
            DisplayMainMenuScreen();
        }
        #endregion

        #region SET THEME
        /// <summary>
        /// The beautiful colors of screen and text.
        /// </summary>
        //static void SetTheme()
        //{
        //    Console.ForegroundColor = ConsoleColor.DarkCyan;
        //    Console.BackgroundColor = ConsoleColor.White;
        //}
        #endregion

        #region ALL USER PROGRAMMING SCHTUFF

        #region USER PROGRAMMING DISPLAY MENU SCREEN
        /// <summary>
        /// Secret area. NO GO
        /// </summary>
        /// <param name="Reznor"></param>
        static void UserProgrammingDisplayMenuScreen(Finch Reznor)
        {
            string menuChoice;
            bool quitMenu = false;

            // tuple time

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayHeader("User Programming Menu!");

                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        UserProgrammingDisplayFinchCommands(commands);
                        break;

                    case "d":
                        UserProgrammingDisplayExecuteFinchCommands(Reznor, commands, commandParameters);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine("\n\tPlease enter a letter from the menu choices!\n");
                        DisplayContinuePromt();
                        break;

                }

            } while (!quitMenu);

        }
        #endregion

        #region USER PROGRAM EXECUTE COMMANDS
        /// <summary>
        /// Eggsecute
        /// </summary>
        /// <param name="reznor"></param>
        /// <param name="commands"></param>
        /// <param name="commandParameters"></param>
        private static void UserProgrammingDisplayExecuteFinchCommands(Finch Reznor, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds * 1000);
            string commandFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayHeader("Execute Reznors Commands");

            Console.WriteLine("\tReznor is ready to execute the list of commands.");
            DisplayContinuePromt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        Reznor.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        Reznor.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.STOPMOTORS:
                        Reznor.setMotors(0, 0);
                        commandFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.WAIT:
                        Reznor.wait(waitMilliSeconds);
                        commandFeedback = Command.WAIT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        Reznor.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNRIGHT.ToString();
                        break;

                    case Command.TURNLEFT:
                        Reznor.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.LEDON:
                        Reznor.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandFeedback = Command.LEDON.ToString();
                        break;

                    case Command.LEDOFF:
                        Reznor.setLED(0, 0, 0);
                        commandFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandFeedback = $"Temperature: {Reznor.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.MAKENOISE:
                        Reznor.noteOn(200);
                        commandFeedback = Command.MAKENOISE.ToString();
                        break;

                    case Command.STOPNOISE:
                        Reznor.noteOff();
                        commandFeedback = Command.STOPNOISE.ToString();
                        break;

                    case Command.ALLSTUFF:
                        Reznor.noteOn(400);
                        Reznor.setLED(ledBrightness, ledBrightness, ledBrightness);
                        Reznor.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.ALLSTUFF.ToString();
                        break;

                    case Command.STOPALLSTUFF:
                        Reznor.noteOff();
                        Reznor.setLED(0, 0, 0);
                        Reznor.setMotors(0, 0);
                        commandFeedback = Command.STOPALLSTUFF.ToString();
                        break;

                    case Command.DONE:
                        commandFeedback = Command.DONE.ToString();
                        break;

                    default:

                        break;

                }

                Console.WriteLine($"\t{commandFeedback}");
            }
            DisplayMenuPrompt("User Programming");
        }
        #endregion

        #region USER PROGRAM DISPLAY COMMANDS
        /// <summary>
        /// Display Commands
        /// </summary>
        /// <param name="commands"></param>
        private static void UserProgrammingDisplayFinchCommands(List<Command> commands)
        {
            DisplayHeader("Reznors Commands Which YOU Entered");

            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }

            DisplayMenuPrompt("User Programming");
        }

        #endregion

        #region USER PROGRAM GET COMMANDS
        /// <summary>
        /// Get the commands
        /// </summary>
        /// <param name="commands"></param>
        private static void UserProgrammingDisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayHeader("Reznors Commands");

            // List commands
            int commandCount = 1;
            Console.WriteLine("\tList of Available Commands\n");
            Console.Write("\t-");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"- {commandName.ToLower()}  -");
                if (commandCount % 5 == 0) Console.Write("-\n\t-");
                commandCount++;
            }
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.Write("\tEnter Command: ");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t\t#############################################");
                    Console.WriteLine("\t\tONLY COMMANDS FROM LIST ABOVE ARE ACCEPTABLE!");
                    Console.WriteLine("\t\t#############################################");
                }
            }

            Console.WriteLine("\n\tLooks like you have some commands!");
            Console.WriteLine("\n\tPress any key to see your commands");
            Console.ReadKey();

            UserProgrammingDisplayFinchCommands(commands);

        }
        #endregion

        #region USER PROGRAM GET COMMAND PARAMETERS
        /// <summary>
        /// Collects the command parameters
        /// </summary>
        /// <returns>commandParameters</returns>
        private static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            DisplayHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            //create the getvalids
            GetValidInteger("\tEnter Motor Speed [1 - 255]: ", 1, 255, out commandParameters.motorSpeed);
            GetValidInteger("\tEnter LED Brightness [1 - 255]: ", 1, 255, out commandParameters.ledBrightness);
            GetValidDouble("\tEnter Wait in Seconds: ", 0, 10, out commandParameters.waitSeconds);

            Console.WriteLine($"\n\tMotor Speed: {commandParameters.motorSpeed}");
            Console.WriteLine($"\tLED Brightness: {commandParameters.ledBrightness}");
            Console.WriteLine($"\tWait Command Duration: {commandParameters.waitSeconds}");

            DisplayMenuPrompt("User Programming");

            return commandParameters;
        }
        #endregion
        #endregion

        #region GETTING VALID DOUBLE
        /// <summary>
        /// Proud of this one. gets a double, validates that, sends it back.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="funkyWait"></param>
        private static void GetValidDouble(string v1, int v2, int v3, out double funkyWait)
        {
            double userInput = 0;


            Console.WriteLine(v1);
            bool crumbles = double.TryParse(Console.ReadLine(), out userInput);


            if (userInput >= v2 && userInput <= v3 && crumbles == true)
            {
                funkyWait = userInput;
                return;
            }

            else
            {
                Console.WriteLine("\n\tWRONG, please enter a valid number.\n");
                GetValidDouble(v1, v2, v3, out funkyWait);
            }
        }
        #endregion

        #region GETTING VALID INTEGER
        /// <summary>
        /// Similar to Double, only INT
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="funkyInt"></param>
        private static void GetValidInteger(string v1, int v2, int v3, out int funkyInt)
        {
            int userInpoot = 0;

            Console.WriteLine(v1);

            Int32.TryParse(Console.ReadLine(), out userInpoot);

            if (userInpoot >= v2 && userInpoot <= v3)
            {

                funkyInt = userInpoot;
                return;
            }

            else
            {
                Console.WriteLine("\n\tYa dun goofed it! Put in a number between 1-255 please.\n");
                GetValidInteger(v1, v2, v3, out funkyInt);
            }

        }
        #endregion

        #region ALL DATA RECORDER 

        #region DATA RECORDER DISPLAY MENU SCREEN
        /// <summary>
        /// The data recorder display
        /// </summary>
        /// <param name="Reznor"></param>
        static void DataRecorderDisplayMenuScreen(Finch Reznor)
        {
            // record temps and put them in array, then go back and display that array. 
            // We have to talk with user, ask how many data points they want to gather, time between those.
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0; // double because we ask for seconds.
            double[] temperatures = null;

            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayHeader("Data Recorder Menu!");
                Console.WriteLine("\n\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\te) Light Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice Please:");
                menuChoice = Console.ReadLine().ToLower();

                DisplayContinuePromt();

                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetTemperatureData(numberOfDataPoints, dataPointFrequency, Reznor);
                        break;

                    case "d":
                        DataRecorderDisplayData(temperatures);
                        break;

                    case "e":
                        GetLightData(Reznor);
                        break;

                    case "f":

                        break;

                    case "q":
                        DisplayMenuPrompt("Data Recorder");
                        quitMenu = true;
                        break;
                }
            } while (!quitMenu);

        }
        #endregion

        #region DATA RECORDER DISPLAY DATA
        /// <summary>
        /// Shows Data
        /// </summary>
        /// <param name="temperatures"></param>
        static void DataRecorderDisplayData(double[] temperatures)
        {
            DisplayHeader("Show Your Data");

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePromt();
        }
        #endregion

        #region DATA RECORDER DISPLAY TABLE
        /// <summary>
        /// The Display Table, got that celsius to fahrenheit!
        /// </summary>
        /// <param name="temperatures"></param>
        static void DataRecorderDisplayTable(double[] temperatures)
        {
            //
            // display table headers below.
            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temp".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                );

            //
            // display table data below.
            for (int index = 0; index < temperatures.Length; index++)
            {
                // find celcius data
                double fahrenheit = ConvertCelsiusToFahrenheit(temperatures[index]);


                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    fahrenheit.ToString("n2").PadLeft(15) + "°F"
              );
            }
        }
        #endregion

        #region CONVERT CELSIUS TO FAHRENHEIT
        /// <summary>
        /// Method exclusively for Converting Celsius to Fahrenheit
        /// </summary>
        /// <param name="celsius"></param>
        /// <returns>shmelsius</returns>
        static double ConvertCelsiusToFahrenheit(double celsius)
        {
            return celsius * 9 / 5 + 32;
        }
        #endregion

        #region DATA RECORDER DISPLAY GET TEMPERATURE DATA
        /// <summary>
        /// How we get the data from the user. 
        /// </summary>
        /// <param name="numberOfDataPoints"></param>
        /// <param name="dataPointFrequency"></param>
        /// <param name="Reznor"></param>
        /// <returns>Temperatures</returns>
        static double[] DataRecorderDisplayGetTemperatureData(int numberOfDataPoints, double dataPointFrequency, Finch Reznor)
        {
            double[] temperatures = new double[numberOfDataPoints];

            DisplayHeader("Get Data");

            Console.WriteLine($"\tNumber of data points: {numberOfDataPoints}");
            Console.WriteLine($"\t\nData point frequency: {dataPointFrequency}");
            Console.WriteLine("\n\tReznor is ready to begin recording the temperature data.");
            DisplayContinuePromt();
            // Set up a loop that will gather the data,
            // We need to get temp reading from finch,
            // echo reading back to user,
            // add that reading to element in array,
            // put a wait in, so it doesn't fly through very quickly.

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = Reznor.getTemperature();
                Console.WriteLine($"\tReading {index + 1}: {temperatures[index].ToString("n2")}");// .ToString("n2") will make it have only 2 decimal places.
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                Reznor.wait(waitInSeconds);
            }

            DisplayContinuePromt();
            DisplayHeader("Get Data");

            Console.WriteLine("\t\nTable of Temperatures\n");

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePromt();

            return temperatures;
        }
        #endregion

        #region DATA RECORDER DISPLAY GET DATA POINT FREQUENCY
        /// <summary>
        /// get the frequency of data points from user
        /// </summary>
        /// <returns>frequency of data points</returns>
        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double dataPointFrequency;


            DisplayHeader("Data Point Frequency");

            Console.Write("\tEnter the frequency you would like to receive data points: ");

            // instead of using Console.Readline(); ^ we declare it in the double.TryParse section below.
            // validate user input

            double.TryParse(Console.ReadLine(), out dataPointFrequency);
            if (dataPointFrequency >= 1 && dataPointFrequency <= 1000000)
            {
                Console.WriteLine($"{dataPointFrequency} looks good!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("We just dont know what this means!!");
                Console.ReadKey();
                DataRecorderDisplayGetDataPointFrequency();

            }
            DisplayContinuePromt();

            return dataPointFrequency;
        }
        #endregion

        #region DATA RECORDER DISPLAY GET NUMBER OF DATA POINTS
        /// <summary>
        /// Gets the number of data points from user
        /// </summary>
        /// <returns>number of data points</returns>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            string userResponse;

            DisplayHeader("Number of Data Points");

            Console.Write("\tPlease enter a number of times you would like to receive data: ");
            userResponse = Console.ReadLine();

            // validate user input


            int.TryParse(userResponse, out numberOfDataPoints);

            DisplayContinuePromt();

            return numberOfDataPoints;
        }
        #endregion

        #region GET  AND DISPLAY LIGHT DATA
        /// <summary>
        /// This is just for getting light data in an array
        /// </summary>
        /// <param name="Reznor"></param>
        /// <returns> The reading of both amounts of light!</returns>
        public static int[] GetLightData(Finch Reznor)
        {
            DisplayHeader("Reznor will see how much light is nearby!");
            DisplayContinuePromt();

            int[] lightSensor = Reznor.getLightSensors();


            for (int index = 0; index < lightSensor.Length; index++)
            {
                Console.WriteLine($"\tHere we go!  {lightSensor[index]}");
            }

            DisplayContinuePromt();

            return lightSensor;
            //int leftLight;
            //leftLight = Reznor.getLeftLightSensor();
            //    Console.WriteLine($"SHOW ME LIGHT {leftLight}");
            //    Console.ReadKey();


        }
        #endregion

        #endregion

        #region ALL TALENT SHOW METHODS

        #region TALENT SHOW DISPLAY MENU SCREEN
        /// <summary>
        /// Our beautiful Finch makes music and lights and dances.
        /// </summary>
        /// <param name="Reznor"></param>
        static void TalentShowDisplayMenuScreen(Finch Reznor)
        {
            Console.CursorVisible = true;

            bool quitTalentShow = false;
            string menuChoice;

            do
            {
                DisplayHeader("Talent Show Time!");
                Console.WriteLine("\n\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing it up");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice Please:");
                menuChoice = Console.ReadLine().ToLower();

                DisplayContinuePromt();

                switch (menuChoice)
                {
                    case "a":
                        LightAndSound(Reznor);
                        break;

                    case "b":
                        DanceMenu(Reznor);
                        break;

                    case "c":
                        MixItUpMenu(Reznor);
                        break;

                    case "q":
                        DisplayMenuPrompt("Main");
                        quitTalentShow = true;
                        break;

                    default:
                        Console.WriteLine("\t\nPlease enter a letter for the menu choice");
                        DisplayContinuePromt();
                        break;
                }
            } while (!quitTalentShow);
        }
        #endregion

        #region DANCE MENU  
        /// <summary>
        /// Dance Party!
        /// </summary>
        /// <param name="Reznor"></param>
        static void DanceMenu(Finch Reznor)
        {
            Console.CursorVisible = false;

            DisplayHeader("Lets DANCE!");
            Console.WriteLine("\n\tReznor will now dance for you!");
            DisplayContinuePromt();
            Reznor.setMotors(100, 100);
            Reznor.wait(3000);
            Reznor.setMotors(100, -100);
            Reznor.wait(3000);
            Reznor.setMotors(0, 0);
            Reznor.setMotors(-100, 100);
            Reznor.wait(3000);
            Reznor.setMotors(0, 0);

            Console.WriteLine("Wow! What incredibly inspired moves! Jealous much?");
            DisplayMenuPrompt("Talent Show");
        }
        #endregion

        #region MIX IT UP
        /// <summary>
        /// Moving and lights!
        /// </summary>
        /// <param name="Reznor"></param>
        static void MixItUpMenu(Finch Reznor)
        {
            Console.CursorVisible = false;

            DisplayHeader("Lets get silly up in here! Dancing! Light show! Music!\n");
            DisplayContinuePromt();

            Reznor.setLED(50, 100, 200);
            Reznor.noteOn(263);
            Reznor.setMotors(-100, 100);
            Reznor.wait(2000);
            Reznor.setMotors(0, 0);
            Reznor.setLED(100, 200, 0);
            Reznor.noteOn(138);
            Reznor.setMotors(100, -100);
            Reznor.wait(2000);
            Reznor.setLED(0, 50, 255);
            Reznor.noteOn(800);
            Reznor.setMotors(100, 100);
            Reznor.wait(2000);
            Reznor.setMotors(0, 0);
            Reznor.setLED(10, 20, 50);
            Reznor.noteOn(138);
            Reznor.setMotors(30, -30);
            Reznor.wait(2000);
            Reznor.setLED(50, 100, 200);
            Reznor.noteOn(263);
            Reznor.setMotors(-100, 100);
            Reznor.wait(2000);
            Reznor.setMotors(0, 0);
            Reznor.setLED(100, 200, 0);
            Reznor.noteOn(138);
            Reznor.setMotors(100, -100);
            Reznor.wait(2000);
            Reznor.setLED(0, 50, 255);
            Reznor.noteOn(800);
            Reznor.setMotors(100, 100);
            Reznor.wait(2000);
            Reznor.setMotors(0, 0);
            Reznor.setLED(10, 20, 50);
            Reznor.noteOn(138);
            Reznor.setMotors(30, -30);
            Reznor.wait(2000);
            Reznor.noteOff();
            Reznor.setLED(0, 0, 0);

            Console.WriteLine("Wow, wasn't that something!");

            DisplayMenuPrompt("Talent Show");

            //for (int allTheThings = 0; allTheThings < 1000 ; allTheThings ++)
            //{
            //    Reznor.setLED(30, 74, 100);
            //    Reznor.noteOn(allTheThings * 20);
            //    Reznor.setMotors(allTheThings, allTheThings);
            //    Reznor.setLED(100, 50, 60);
            //}
        }
        #endregion

        #region LIGHT AND SOUND
        /// <summary>
        /// A for loop to create light and sound
        /// </summary>
        /// <param name="Reznor"></param>
        static void LightAndSound(Finch Reznor)
        {
            Console.CursorVisible = false;

            DisplayHeader("Light and Sound Time!");

            Console.WriteLine("\n\tReznor will now scream and glow rapidly!");
            DisplayContinuePromt();
            for (int lightSound = 0; lightSound < 500; lightSound++)
            {
                Reznor.setLED(lightSound, 0, lightSound);
                Reznor.noteOn(lightSound * 50);
            }
            Reznor.setLED(0, 0, 0);
            DisplayMenuPrompt("Talent Show");
        }
        #endregion

        #endregion

        #region ALL ALARM SYSTEM METHODS

        #region ALARM SYSTEM DISPLAY MENU SCREEN
        /// <summary>
        /// Probably be a real loud one here.
        /// </summary>
        /// <param name="Reznor"></param>
        static void AlarmSystemDisplayMenuScreen(Finch Reznor)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayHeader("\tAlarm Menu!");
                Console.WriteLine("\n\t\ta) Set Sensors to Monitor");
                Console.WriteLine("\t\tb) Set Range Type");
                Console.WriteLine("\t\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\t\td) Set Time to Monitor");
                Console.WriteLine("\t\te) Set Alarm");
                Console.WriteLine("\t\tf) Set Temp and Light Alarm");
                Console.WriteLine("\t\tq) Main Menu");
                Console.Write("\t\tEnter Choice Please:");
                menuChoice = Console.ReadLine().ToLower();

                DisplayContinuePromt();

                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmSetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmSetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmSetMinMaxThresholdValue(rangeType, Reznor);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmSetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(Reznor, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "f":
                        TempAndLightAlarm(Reznor, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        DisplayMenuPrompt("Data Recorder");
                        quitMenu = true;
                        break;
                }
            } while (!quitMenu);
        }
        #endregion

        #region TEMP AND LIGHT ALARM
        static void TempAndLightAlarm(Finch Reznor, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {

            double alarmTemp = Reznor.getTemperature();
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            bool thresholdExceededTemp = false;
            int currentLightSensorValue = 0;
            double hotCity = 0;

            DisplayHeader("\tTemp data!");

            Console.WriteLine($"\n\t\tCurrent Temperature is: {alarmTemp * 9 / 5 + 32}°F");
            Console.WriteLine("\n\tLets set a minimum and or a maximum temperature!");
            Console.WriteLine("\n\tWould you like minimum or maximum?");
            string minOrMax = Console.ReadLine();

            if (minOrMax == "minimum")
            {
                Console.WriteLine($"\n\t{minOrMax} it is!");
                Console.WriteLine("\n\tPlease enter the minimum temperature!");
                bool minReading = double.TryParse(Console.ReadLine(), out hotCity);
                Console.WriteLine($"\n\t{hotCity} is what we will go with then.");
                Console.WriteLine("\n\tPress any key to continue!");
                Console.ReadKey();

            }
            else if (minOrMax == "maximum")
            {
                Console.WriteLine($"\n\t{minOrMax} sounds good!");
                Console.WriteLine("\n\tPlease enter the maximum temperature!");
                bool maxReading = double.TryParse(Console.ReadLine(), out hotCity);

                Console.WriteLine($"\n\t{hotCity} is perfect.");
                Console.WriteLine("\n\tPress any key to continue!");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\n\tOh didly dang! Better give that another shot.");
                Console.WriteLine("\n\tBACK TO THE BEGINNING WE GO! Press any key");
                Console.ReadKey();
                TempAndLightAlarm(Reznor, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
            }

            //Console.WriteLine("How hot is too hot?: ");
            //bool tooHot = double.TryParse(Console.ReadLine(), out double hotCity);
            //if (tooHot)
            //{
            //    Console.WriteLine("WOW that's hot!"); 
            //}
            //else
            //{
            //    Console.WriteLine("WHAT IS THAT! Ya dun messed up now.");
            //    Console.WriteLine("Try again");
            //    Console.ReadKey();

            //    TempAndLightAlarm(Reznor, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
            //}

            DisplayHeader("\tSet Alarm");

            Console.WriteLine($"\n\t\tCurrent Temperature: {alarmTemp * 9 / 5 + 32}°F");
            Console.WriteLine($"\t\tSensors to monitor: {sensorsToMonitor}");
            Console.WriteLine("\t\tRange Type: {0}", rangeType);
            Console.WriteLine("\t\tMin/Max threshold value: " + minMaxThresholdValue);
            Console.WriteLine($"\t\tTime to monitor: {timeToMonitor}\n");

            Console.WriteLine("\tPress any key to begin monitoring!\n");
            Console.ReadKey();

            while (secondsElapsed < timeToMonitor && !thresholdExceeded && !thresholdExceededTemp)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = Reznor.getLeftLightSensor();
                        break;

                    case "right":
                        currentLightSensorValue = Reznor.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (Reznor.getRightLightSensor() + Reznor.getLeftLightSensor()) / 2;
                        break;
                }

                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }

                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }

                        break;
                }
                switch (minOrMax)
                {
                    case "minimum":
                        if (alarmTemp * 9 / 5 + 32 < hotCity)
                        {
                            thresholdExceededTemp = true;
                        }
                        break;

                    case "maximum":
                        if (alarmTemp * 9 / 5 + 32 > hotCity)
                        {
                            thresholdExceededTemp = true;
                        }
                        break;
                }

                Reznor.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Reznor.noteOn(300);
                Reznor.wait(2000);
                Reznor.noteOff();
                Console.WriteLine($"The {rangeType} threshold value of {minMaxThresholdValue} was exceeded by the current light sensor value of {currentLightSensorValue}.");
            }
            if (thresholdExceededTemp)
            {
                Reznor.noteOn(500);
                Reznor.wait(2000);
                Reznor.noteOff();
                Console.WriteLine($"{hotCity}°F!?? The temperature has been exceeded!!");
            }

            else
            {
                Console.WriteLine($"The {rangeType} threshold value {minMaxThresholdValue} and {hotCity}°F was not exceeded.");
            }

            DisplayMenuPrompt("Light Alarm");
        }
        #endregion

        #region LIGHT ALARM SET SENSORS TO MONITOR
        static string LightAlarmSetSensorsToMonitor()
        {
            string sensorsToMonitor;

            DisplayHeader("\tSensors to Monitor");


            Console.WriteLine("\n\t\tWhich sensors would you like to monitor? [left, right, both]");
            sensorsToMonitor = Console.ReadLine();
            if (sensorsToMonitor == "left" || sensorsToMonitor == "right" || sensorsToMonitor == "both")
            {
                Console.WriteLine($"\n\t{sensorsToMonitor} eh? Sounds good to me!");
            }
            else
            {
                Console.WriteLine("\tWARNING UNKOWN VARIABLE- Please try again.");
                Console.WriteLine("\n\t\tPress any key to try again");
                Console.ReadKey();
                LightAlarmSetSensorsToMonitor();
            }

            DisplayMenuPrompt("Light Alarm");

            return sensorsToMonitor;
        }
        #endregion

        #region LIGHT ALARM SET RANGE TYPE
        static string LightAlarmSetRangeType()
        {
            string rangeType;

            DisplayHeader("\tRange Type");

            Console.WriteLine("\t\tPlease enter the Range Type you would like to use between: [minimum, maximum]");
            rangeType = Console.ReadLine();

            if (rangeType == "minimum" || rangeType == "maximum")
            {
                Console.WriteLine($"\t{rangeType} is what I like to use too!");
            }
            else
            {
                Console.WriteLine("\n\tUh oh! Looks like you done goofed it up! Better try again.");
                Console.WriteLine("\n\tPress any key to try again!");
                Console.ReadKey();
                LightAlarmSetRangeType();
            }

            DisplayMenuPrompt("Light Alarm");

            return rangeType;
        }
        #endregion

        #region LIGHT ALARM SET MIN MAX THRESHOLD VALUE

        static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch Reznor)
        {
            int minMaxThresholdValue;

            DisplayHeader("Min/Max Threshold Value");

            Console.WriteLine($"\tCurrent Left light sensor ambient value: {Reznor.getLeftLightSensor()}");
            Console.WriteLine($"\tCurrent Right light sensor ambient value: {Reznor.getRightLightSensor()}\n");

            // validate value

            Console.WriteLine($"\tEnter the {rangeType} light sensor value: \n");
            int.TryParse(Console.ReadLine(), out minMaxThresholdValue);
            if (minMaxThresholdValue >= 0 || minMaxThresholdValue <= 255)
            {
                Console.WriteLine($"{minMaxThresholdValue} sounds like a good plan. SMORT.");
            }
            else
            {
                Console.WriteLine("\tGolly gee you just have to enter a number! Maybe try again.");
                Console.WriteLine("\n\tPress any key to try again!");
                Console.ReadKey();
                LightAlarmSetMinMaxThresholdValue(rangeType, Reznor);
            }
            // echo value back to user

            DisplayMenuPrompt("Light Alarm");

            return minMaxThresholdValue;
        }
        #endregion

        #region LIGHT ALARM SET TIME TO MONITOR

        static int LightAlarmSetTimeToMonitor()
        {
            int timeToMonitor;

            DisplayHeader("Time to Monitor");

            // validate value
            Console.WriteLine($"\tHow many seconds would you like to Monitor?: \n");
            int.TryParse(Console.ReadLine(), out timeToMonitor);
            if (timeToMonitor >= 0 || timeToMonitor <= 100000000)
            {
                Console.WriteLine($"{timeToMonitor} many seconds eh?");
            }
            else
            {
                Console.WriteLine("Whoopsie Doo! Try again please!");
                Console.WriteLine("Press any key to give it another shot. (I believe in you)");
                Console.ReadKey();
                LightAlarmSetTimeToMonitor();
            }
            // echo value back to user

            DisplayMenuPrompt("Light Alarm");

            return timeToMonitor;
        }
        #endregion     

        #region LIGHT ALARM SET ALARM
        static void LightAlarmSetAlarm(Finch Reznor, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;

            DisplayHeader("\tSet Alarm");

            Console.WriteLine($"\n\t\tSensors to monitor: {sensorsToMonitor}");
            Console.WriteLine("\t\tRange Type: {0}", rangeType);
            Console.WriteLine("\t\tMin/Max threshold value: " + minMaxThresholdValue);
            Console.WriteLine($"\t\tTime to monitor: {timeToMonitor}\n");

            Console.WriteLine("\tPress any key to begin monitoring!\n");
            Console.ReadKey();

            while (secondsElapsed < timeToMonitor && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = Reznor.getLeftLightSensor();
                        break;

                    case "right":
                        currentLightSensorValue = Reznor.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (Reznor.getRightLightSensor() + Reznor.getLeftLightSensor()) / 2;
                        break;
                }

                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }

                Reznor.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Reznor.noteOn(300);
                Reznor.wait(2000);
                Reznor.noteOff();
                Console.WriteLine($"The {rangeType} threshold value of {minMaxThresholdValue} was exceeded by the current light sensor value of {currentLightSensorValue}.");
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value {minMaxThresholdValue} was not exceeded.");
            }

            DisplayMenuPrompt("Light Alarm");
        }
        #endregion

        #endregion

        #region ALL DISPLAY METHODS

        #region DISPLAY MENU SCREEN
        /// <summary>
        /// Main Menu Screen
        /// </summary>
        static void DisplayMainMenuScreen()
        {
            //
            //  MAIN MENU
            //
            Console.CursorVisible = true;
            bool quitApplication = false;
            string menuChoice;
            //This creates a new Finch object
            Finch Reznor = new Finch();

            do
            {
                DisplayHeader("\tWelcome to the Finch Menu!");
                Console.WriteLine("\n\t\ta) Connect Finch Robot named Reznor");
                Console.WriteLine("\t\tb) Talent Show");
                Console.WriteLine("\t\tc) Data Recorder");
                Console.WriteLine("\t\td) Alarm System");
                Console.WriteLine("\t\te) User Programming");
                Console.WriteLine("\t\tf) Disconnect Finch Robot");
                Console.WriteLine("\t\tg) Reconfigure the Theme");
                Console.WriteLine("\t\th) Change Username and Password");
                Console.WriteLine("\t\tq) Quit");
                Console.Write("\t\t\tEnter your choice Please:");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(Reznor);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(Reznor);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(Reznor);
                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(Reznor);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(Reznor);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(Reznor);
                        break;

                    case "g":
                        DisplaySetTheme();
                        break;

                    case "h":
                        DisplayLoginRegister();
                        break;

                    case "q":
                        DisplayClosingScreen(Reznor);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine("\t\nPlease enter a letter for the menu choice");
                        DisplayContinuePromt();
                        break;
                }
            } while (!quitApplication);
        }
        #endregion

        #region DISPLAY CONNECT FINCH ROBOT
        /// <summary>
        /// This is how the FInch connects to the application
        /// </summary>
        /// <param name="Reznor"></param>
        /// <returns>That the finch is connected</returns>
        static bool DisplayConnectFinchRobot(Finch Reznor)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayHeader("Connect Finch Robot");

            Console.WriteLine("\n\nThe application is going to connect with Reznor now. Please don't remove the cord!");

            DisplayContinuePromt();

            //This connects it, this Finch is named Reznor.
            robotConnected = Reznor.connect();


            while (robotConnected)
            {
                Reznor.setLED(100, 0, 100);
                Reznor.noteOn(400);
                Reznor.wait(1000);
                Reznor.noteOff();
                Reznor.setLED(0, 0, 0);
                Console.WriteLine("Hooray! Reznor is ready to party now!");
                Console.ReadKey();
                break;

            }


            if (!(robotConnected))
            {
                Console.WriteLine("WARNING FAILURE TO CONNECT WARNING! Press any key to escape");
                Console.ReadKey();
            }
            return robotConnected;
            //use while loop to connect and confirm to the Finch Robot
            //Return a bool value indicating if the connection was successful
        }
        #endregion

        #region DISPLAY MENU PROMPT
        /// <summary>
        /// Our menu calling method
        /// </summary>
        /// <param name="menuName"></param>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine($"\n\tPress any key to go back to the {menuName} Menu");
            Console.ReadKey();
        }
        #endregion

        #region DISPLAY DISCONNECT FINCH ROBOT
        /// <summary>
        /// How we disconnect the Finchy
        /// </summary>
        /// <param name="Reznor"></param>
        //RUSSELL YOU CHANGED BOOL TO VOID ON DISPLAYDISCONNECTFINCHROBOT
        static void DisplayDisconnectFinchRobot(Finch Reznor)
        {
            DisplayHeader("Disconnect Finch Robot named Reznor");

            Console.WriteLine("Done already!? Well Reznor will disconnect now. Please be patient!");
            Reznor.disConnect();
            Console.WriteLine("Our Finch friend Reznor has been disconnected.");

            DisplayContinuePromt();

        }
        #endregion

        #region DISPLAY WELCOME SCREEN
        /// <summary>
        /// VELCOME TO OUR APPLICATION
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine("\n\t\tRussells Finch Control!\n");

            DisplayContinuePromt();

        }
        #endregion

        #region DISPLAY CLOSING SCREEN
        /// <summary>
        /// GOODBYE TIME
        /// </summary>
        /// <param name="Reznor"></param>
        static void DisplayClosingScreen(Finch Reznor)
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine("\n\t\tThanks for playing with Reznor!\n");

            DisplayContinuePromt();
        }
        #endregion

        #region DISPLAY HEADER
        /// <summary>
        /// Method called to display a header
        /// </summary>
        /// <param name="headerText"></param>
        static void DisplayHeader(string headerText)
        {

            // DisplayHeader will return whatever is in the writeline in the app above.
            Console.Clear();
            Console.WriteLine($"\n\t\t{headerText}\n");

        }
        #endregion

        #region DISPLAY CONTINUE PROMPT
        /// <summary>
        /// A readkey method
        /// </summary>
        static void DisplayContinuePromt()
        {   //
            // pause for user
            //
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue!\n");
            Console.ReadKey();
        }
        #endregion

        #endregion

        #region ALL SET THEME DATA
        #region DISPLAY SET THEME
        static void DisplaySetTheme()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            bool themeChosen = false;

            //Current theme

            themeColors = ReadThemeData();
            Console.ForegroundColor = themeColors.foregroundColor;
            Console.BackgroundColor = themeColors.backgroundColor;
            Console.Clear();
            DisplayHeader("Set Application Theme");

            Console.WriteLine($"\tCurrent foreground color is- {Console.ForegroundColor}");
            Console.WriteLine($"\tCurrent background color is- {Console.BackgroundColor}\n");

            Console.Write("\tWould you like to change the current theme [ yes | no ]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                do
                {
                    themeColors.foregroundColor = GetConsoleColorFromUser("foreground");
                    themeColors.backgroundColor = GetConsoleColorFromUser("background");

                    //users new theme

                    Console.ForegroundColor = themeColors.foregroundColor;
                    Console.BackgroundColor = themeColors.backgroundColor;
                    Console.Clear();
                    DisplayHeader("Set Application Theme");
                    Console.WriteLine($"\tNew foreground color- {Console.ForegroundColor}");
                    Console.WriteLine($"\tNew background color- {Console.BackgroundColor}\n");

                    Console.Write("\tIs this the theme you want?");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        themeChosen = true;
                        WriteThemeData(themeColors.foregroundColor, themeColors.backgroundColor);
                    }

                } while (!themeChosen);
            }
        }
        #endregion

        #region WRITE DAT THEME DATA
        static void WriteThemeData(ConsoleColor foreground, ConsoleColor background)
        {
            string dataPath = @"Data/Theme.txt";

            File.WriteAllText(dataPath, foreground.ToString() + "\n");
            File.AppendAllText(dataPath, background.ToString());
        }

        static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) ReadThemeData()
        {
            string dataPath = @"Data/Theme.txt";
            string[] themeColors;

            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;

            themeColors = File.ReadAllLines(dataPath);

            Enum.TryParse(themeColors[0], true, out foregroundColor);
            Enum.TryParse(themeColors[1], true, out backgroundColor);

            return (foregroundColor, backgroundColor);
        }
        #endregion

        #region GET CONSOLE COLOR FROM USER
        /// <summary>
        /// Gets the console color from user
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        static ConsoleColor GetConsoleColorFromUser(string property)
        {
            ConsoleColor consoleColor;
            bool validConsoleColor;

            do
            {
                Console.Write($"\tEnter a value for the {property}:");
                validConsoleColor = Enum.TryParse<ConsoleColor>(Console.ReadLine(), true, out consoleColor);

                if (!validConsoleColor)
                {
                    Console.WriteLine("\n\tPLEASE ENTER A COLOR WE ONLY UNDERTAND COLORS!\n");
                }
                else
                {
                    validConsoleColor = true;
                }

            } while (!validConsoleColor);

            return consoleColor;
        }
        #endregion
        #endregion

        #region ALL USER LOGIN DATA

        #region DISPLAYLOGIN REGISTER
        /// <summary>
        /// asks about user registration
        /// </summary>
        private static void DisplayLoginRegister()
        {
            DisplayHeader("Login/Register");

            Console.Write("\n\tAre you a registered user [ yes | no]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                DisplayLogin();
            }
            else
            {
                DisplayRegisterUser();
                DisplayLogin();
            }
        }
        #endregion

        #region DISPLAY LOGIN
        /// <summary>
        /// gets login data, puts user in loop if they cannot supply correct data
        /// </summary>
        private static void DisplayLogin()
        {
            string userName;
            string passWord;
            bool validLogin;

            do
            {
                DisplayHeader("Login");

                Console.Write("\n\tEnter your User Name:");
                userName = Console.ReadLine();
                Console.Write("\tEnter your Password:");
                passWord = Console.ReadLine();

                validLogin = IsValidLoginInfo(userName, passWord);

                if (validLogin)
                {
                    Console.WriteLine("\n\tY'all is logged in now!");
                }
                else
                {
                    Console.WriteLine("\n\tPassword or User Name is incorrect.");
                    Console.WriteLine("\tGive it another shot partner!");
                }

                DisplayContinuePromt();
            } while (!validLogin);
        }
        #endregion

        #region IS VALID LOGIN INFO
        /// <summary>
        /// checks and validates that the username and passwors are correct.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private static bool IsValidLoginInfo(string userName, string passWord)
        {
            (string userName, string passWord) userInfo;
            bool validUser;

            userInfo = ReadLoginInfoData();

            validUser = (userInfo.userName == userName) && (userInfo.passWord == passWord);

            return validUser;
        }
        #endregion

        #region READ LOGIN INFO DATA
        /// <summary>
        /// creates tuple and array to split the login data into username and password
        /// </summary>
        /// <returns></returns>
        private static (string userName, string passWord) ReadLoginInfoData()
        {
            string dataPath = @"Data/Logins.txt";

            string loginInfoText;
            string[] loginInfoArray;
            (string userName, string passWord) loginInfoTuple;

            loginInfoText = File.ReadAllText(dataPath);

            // Split method spereates usename and password into array

            loginInfoArray = loginInfoText.Split(',');
            loginInfoTuple.userName = loginInfoArray[0];
            loginInfoTuple.passWord = loginInfoArray[1];

            return loginInfoTuple;
        }
        #endregion

        #region DISPLAY REGISTER USER
        /// <summary>
        /// if user needs to create a name and password it allows it here.
        /// </summary>
        private static void DisplayRegisterUser()
        {
            string userName;
            string passWord;

            DisplayHeader("Register Time!");

            Console.Write("\tEnter your user name:");
            userName = Console.ReadLine();
            Console.Write("\tEnter your password:");
            passWord = Console.ReadLine();

            WriteLoginInfoData(userName, passWord);

            Console.WriteLine("\n\tYou entered the following and it has been saved.");
            Console.WriteLine($"\tUser Name: {userName}");
            Console.WriteLine($"\tPassword: {passWord}");

            DisplayContinuePromt();
        }
        #endregion

        #region WRITE LOGIN INFO DATA
        /// <summary>
        /// allows the program to interact with Logins.txt
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        private static void WriteLoginInfoData(string userName, string passWord)
        {
            string datapath = @"Data/Logins.txt";
            string loginInfoText;

            loginInfoText = userName + "," + passWord;

            File.WriteAllText(datapath, loginInfoText);
        }
        #endregion

        #endregion
    }

}
