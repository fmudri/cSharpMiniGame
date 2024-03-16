using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;
// Added to track if player is frozen
bool isPlayerFrozen = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;

InitializeGame();
while (!shouldExit)
{
    Move();

    // METHOD CALLS ADDED BY ME
    TerminalQuit(); // CREATED
    FoodEaten(); // CREATED
    ChangePlayer(); //
    FreezePlayer(); // MODIFIED
}

// Returns true if the Terminal was resized 
bool TerminalResized()
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

void TerminalQuit()
{
    if (TerminalResized() == true)
    {
        Console.Clear();
        Console.WriteLine("Console was resized. Program exiting");
        Thread.Sleep(2000);
        Console.Clear();
        Environment.Exit(0);
    }
}

// METHOD ADDED BY ME
void FoodEaten()
{
    // Check if the player consumed the food
    if (playerX == foodX && playerY == foodY)
    {
        ShowFood();
        if (foods[food] == "#####")
        {
            if (!isPlayerFrozen)
            {
                FreezePlayer();
                isPlayerFrozen = true;
            }
        }
    }
}

// Displays random food at a random location
void ShowFood()
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer()
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving
//IF STATEMENT ADDED
void FreezePlayer()
{
    if (player == states[2])
            {
                if (!isPlayerFrozen)
                {
                    System.Threading.Thread.Sleep(1000);
                    player = states[0];
                    isPlayerFrozen = true;
                }
            }
    
}

// Reads directional input from the Console and moves the player
// IF STATEMENTS ADDED BY ME
void Move()
{
    int lastX = playerX;
    int lastY = playerY;

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY--;
            if (player == states[1])
            {
                playerY -= 3;
            }
            break;
        case ConsoleKey.DownArrow:
            playerY++;
            if (player == states[1])
            {
                playerY += 3;
            }
            break;
        case ConsoleKey.LeftArrow:
            playerX--;
            if (player == states[1])
            {
                playerX -= 3;
            }
            break;
        case ConsoleKey.RightArrow:
            playerX++;
            if (player == states[1])
            {
                playerX += 3;
            }
            break;
        case ConsoleKey.Escape:
            shouldExit = true;
            break;
        // DEFAULT CASE ADDED BY ME
        default:
            Console.Clear();
            Console.WriteLine("Nondirectional key pressed. Exiting program.");
            Thread.Sleep(2000);
            Console.Clear();
            shouldExit = true;
            break;
    }

    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++)
    {
        Console.Write(" ");
    }

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}