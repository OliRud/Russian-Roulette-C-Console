using System;
using System.Threading;

class Program
{
    static bool Game_running = true;
    static int Fatal_chamber = new Random().Next(1, 6);
    static bool turn = true;

    static void PulledTrigger(string target)
    {
        Fatal_chamber--;

        if (Fatal_chamber == 0)
        {
            if (target == "player")
            {
                Console.WriteLine(SceneChange(6));
                Game_running = false;
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (target == "opponent")
            {
                Console.WriteLine(SceneChange(4));
                Game_running = false;
                Console.WriteLine("BANG!\nYou Win");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        else
        {
            Console.WriteLine("\n*click*");
        }
    }

    static bool PlayerTurn(int action)
    {
        if (action == 1)
        {
            Fatal_chamber = new Random().Next(1, 6);
            Console.WriteLine("\nYou spin the cylinder");
            return true;
        }
        else if (action == 2)
        {
            Console.WriteLine(SceneChange(2));
            Console.WriteLine("\nYou pull the trigger");
            Thread.Sleep(2000);
            PulledTrigger("player");
            return false;
        }
        else if (action == 3)
        {
            Console.WriteLine("\nYou pull the trigger on the opponent");
            Console.WriteLine(SceneChange(2));
            Thread.Sleep(1000);
            PulledTrigger("opponent");
            Console.WriteLine("\nYou pull the trigger on yourself");
            Thread.Sleep(2000);
            PulledTrigger("player");
            return false;
        }
        return false;
    }

    static bool OpponentTurn(int action)
    {
        if (action == 1)
        {
            Fatal_chamber = new Random().Next(1, 6);
            Console.WriteLine("\nThey spin the cylinder...");
            Thread.Sleep(1000);
            return false;
        }
        else if (action == 2)
        {
            Console.WriteLine("opponent pulled trigger");
            Thread.Sleep(1000);
            PulledTrigger("opponent");
            return true;
        }
        else if (action == 3)
        {
            Console.WriteLine("opponent pulls on you");
            Console.WriteLine(SceneChange(5));
            Thread.Sleep(2000);
            PulledTrigger("player");
            Console.WriteLine("opponent pulls on themselves");
            Console.WriteLine(SceneChange(3));
            Thread.Sleep(1000);
            PulledTrigger("opponent");
            return true;
        }
        return false;
    }

    static string SceneChange(int scene)
    {
        string scene1 = "             0\n       _____/|\\_____\n      /      __     \\\n     /      /        \\\n    /                 \\";
        string scene2 = "             0\n       _____/|\\_____\n      /             \\\n     /               \\\n    /                 \\";
        string scene3 = "             0-\n       _____/|/_____\n      /             \\\n     /               \\\n    /                 \\";
        string scene4 = "           ###\n       _____/|\\_____\n      /             \\\n     /               \\\n    /                 \\";
        string scene5 = "             0\n       _____/|._____\n      /             \\\n     /               \\\n    /                 \\";
        string scene6 = "####################\n####################\n####################\n####################\nYou died, unlucky.";

        switch (scene)
        {
            case 1: return scene1;
            case 2: return scene2;
            case 3: return scene3;
            case 4: return scene4;
            case 5: return scene5;
            case 6: return scene6;
            default: return "";
        }
    }

static void Main()
{
    while (Game_running)
    {
        if (turn)
        {
            Console.WriteLine("-Your turn-");
            Console.WriteLine(SceneChange(1));

            try
            {
                Console.WriteLine("Enter action (1: spin cylander 2: pull trigger 3: shoot opponent");
                int action = Convert.ToInt32(Console.ReadLine());
                if (action != 1 && action != 2 && action != 3)
                {
                    Console.WriteLine("\n--invalid response--");
                }
                else
                {
                    turn = PlayerTurn(action);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n--invalid response--");
            }
        }
        else
        {
            Console.WriteLine("-Opponent turn-");
            Console.WriteLine(SceneChange(3));
            int opponent_choice = new Random().Next(1, 4);
            turn = OpponentTurn(opponent_choice);
        }
    }
}
}