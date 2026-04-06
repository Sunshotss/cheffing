
using Raylib_cs;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

class box
{
    public int x = 0;
    public int y = 0;
    public box(int ax, int ay) 
    { 
        x = ax; y = ay; 
    }
    public int move(int direction, int xa, int ya, int xb, int yb)
    {
        if (xa == xb) { return xa + direction; }
        return 0;
    } 
}

class food
{
    public int savory = 0;
    public int sweet = 0;
    public int spicy = 0;
    public int mild = 0;
    public food(int asavory, int asweet, int aspicy, int amild)
    {
        savory = asavory;
        sweet = asweet;
        spicy = aspicy;
        mild = amild;
    }
}

class customer
{
    public int savory_stat = 0;
    public int sweet_stat = 0;
    public int spicy_stat = 0;
    public int mild_stat = 0;
    public Raylib_cs.Rectangle face = new Raylib_cs.Rectangle();
    public customer(int asavory_stat, int asweet_stat, int aspicy_stat, int amild_stat, int a, int b, int c, int d)
    {
        savory_stat = asavory_stat;
        sweet_stat = asweet_stat;
        spicy_stat = aspicy_stat;
        mild_stat = amild_stat;
        face = new Raylib_cs.Rectangle(a,b,c,d);
    }

}

class Program
{
    static void Main()
    {
        Random rnd = new Random();
        int width = 1200;
        int height = 600;
        int rows = 12;
        int cols = 30;
        
        int savory = 0;
        int sweet = 0;
        int spicy = 0;
        int mild = 0;
        
        int money = 10000;
        int discrepancy = 1;

        List<food> food_list = new List<food>();
        List<customer> customer_list = new List<customer>();

        int x = 1;
        int y = 1;
        char[,] map = {
            {'b','b','b','b','b','b','b','b','b','b','b','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','a','a','a','a','a','a','a','a','a','a','b' },
            {'b','b','b','b','b','b','b','b','b','b','b','b' },
        };
        int cube = 20;


        Raylib_cs.Color Colors(int color)
        {
            if (color == 0) return Raylib_cs.Color.Black;
            if (color == 1) return Raylib_cs.Color.Gray;
            if (color == 2) return Raylib_cs.Color.White;
            if (color == 3) return Raylib_cs.Color.Blue;
            if (color == 4) return Raylib_cs.Color.SkyBlue;
            if (color == 5) return Raylib_cs.Color.Maroon;

            return Raylib_cs.Color.Black;
        }

        // buttons
        Raylib_cs.Rectangle savory_knapp = new Raylib_cs.Rectangle(300, 100, cube, cube);
        Raylib_cs.Rectangle sweet_knapp = new Raylib_cs.Rectangle(400, 100, cube, cube);
        Raylib_cs.Rectangle spicy_knapp = new Raylib_cs.Rectangle(500, 100, cube, cube);
        Raylib_cs.Rectangle mild_knapp = new Raylib_cs.Rectangle(600, 100, cube, cube);

        Raylib_cs.Rectangle ship_knapp = new Raylib_cs.Rectangle(600, 200, cube, cube);
        Raylib_cs.Rectangle customer_knapp = new Raylib_cs.Rectangle(1000, 200, cube, cube);
        Raylib_cs.Rectangle seller_knapp = new Raylib_cs.Rectangle(700, 200, cube, cube);


        Raylib.InitWindow(width, height, "game i made");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Colors(3));
            Raylib.SetTargetFPS(60);


            // movement
            if (Raylib.IsKeyPressed(KeyboardKey.W))
            {
                if (map[y - 1, x] != 'b') { y -= 1; }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.A))
            {
                if (map[y, x - 1] != 'b') { x -= 1; }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.S))
            {
                if (map[y + 1, x] != 'b') { y += 1; }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.D))
            {
                if (map[y, x + 1] != 'b') { x += 1; }
            }

            // mouse
            Vector2 mouse_pos = Raylib.GetMousePosition();
            Raylib_cs.Rectangle mouse_collision = new Raylib_cs.Rectangle(mouse_pos.X, mouse_pos.Y, 1, 1);


            // button stuff
            if (Raylib.CheckCollisionRecs(mouse_collision, savory_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    savory += 1;
                }
            }
            Raylib.DrawRectangle((int)savory_knapp.X, (int)savory_knapp.Y, (int)savory_knapp.Width, (int)savory_knapp.Height, Colors(2));
            Raylib.DrawText($"savory:{savory}", (int)savory_knapp.X, (int)savory_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, sweet_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    sweet += 1;
                }
            }
            Raylib.DrawRectangle((int)sweet_knapp.X, (int)sweet_knapp.Y, (int)sweet_knapp.Width, (int)sweet_knapp.Height, Colors(2));
            Raylib.DrawText($"sweet:{sweet}", (int)sweet_knapp.X, (int)sweet_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, spicy_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    spicy += 1;
                }
            }
            Raylib.DrawRectangle((int)spicy_knapp.X, (int)spicy_knapp.Y, (int)spicy_knapp.Width, (int)spicy_knapp.Height, Colors(2));
            Raylib.DrawText($"spicy:{spicy}", (int)spicy_knapp.X, (int)spicy_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, mild_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    mild += 1;
                }
            }
            Raylib.DrawRectangle((int)mild_knapp.X, (int)mild_knapp.Y, (int)mild_knapp.Width, (int)mild_knapp.Height, Colors(2));
            Raylib.DrawText($"mild:{mild}", (int)mild_knapp.X, (int)mild_knapp.Y - 30, cube, Colors(2));

            if (Raylib.CheckCollisionRecs(mouse_collision, ship_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    food_list.Add(new food(savory, sweet, spicy, mild));
                    savory = 0;
                    sweet = 0;
                    spicy = 0;
                    mild = 0;
                }
            }
            Raylib.DrawRectangle((int)ship_knapp.X, (int)ship_knapp.Y, (int)ship_knapp.Width, (int)ship_knapp.Height, Colors(2));
            Raylib.DrawText("ship", (int)ship_knapp.X, (int)ship_knapp.Y - 30, cube, Colors(2));

            if (Raylib.CheckCollisionRecs(mouse_collision, customer_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    customer_list.Add(new customer(rnd.Next(10), rnd.Next(10), rnd.Next(10), rnd.Next(10), (int)customer_knapp.X, (int)customer_knapp.Y, cube, cube));
                }
            }
            Raylib.DrawRectangle((int)customer_knapp.X, (int)customer_knapp.Y, (int)customer_knapp.Width, (int)customer_knapp.Height, Colors(2));
            Raylib.DrawText("new customer", (int)customer_knapp.X, (int)customer_knapp.Y - 30, cube, Colors(2));
            
            // draws "customer_list"
            foreach (var customer in customer_list)
            {
                Raylib.DrawText($"{customer},{customer.savory_stat},{customer.sweet_stat},{customer.spicy_stat},{customer.mild_stat}", (int)customer_knapp.X, (int)customer_knapp.Y + customer_list.IndexOf(customer) * cube + cube, 20, Colors(2));

            }

            // draws "food_list"
            foreach (var food in food_list)
            {
                Raylib.DrawText($"{food},{food.savory},{food.sweet},{food.spicy},{food.mild}", (int)ship_knapp.X, (int)ship_knapp.Y + food_list.IndexOf(food) * cube + cube, 20, Colors(2));

            }

            // sells doof on top of "food_list"
            if (Raylib.CheckCollisionRecs(mouse_collision, seller_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    if (customer_list[0].savory_stat <= food_list[0].savory) { discrepancy += (food_list[0].savory - customer_list[0].savory_stat);  customer_list[0].savory_stat = 0; }
                    else customer_list[0].savory_stat -= food_list[0].savory;
                    if (customer_list[0].sweet_stat <= food_list[0].sweet) { discrepancy += (food_list[0].sweet - customer_list[0].sweet_stat);  customer_list[0].sweet_stat = 0; }
                    else customer_list[0].sweet_stat -= food_list[0].sweet;
                    if (customer_list[0].spicy_stat <= food_list[0].spicy) { discrepancy += (food_list[0].spicy - customer_list[0].spicy_stat);  customer_list[0].spicy_stat = 0; }
                    else customer_list[0].spicy_stat -= food_list[0].spicy;
                    if (customer_list[0].mild_stat <= food_list[0].mild) { discrepancy += (food_list[0].mild - customer_list[0].mild_stat); customer_list[0].mild_stat = 0; }
                    else customer_list[0].mild_stat -= food_list[0].mild;
                    if (food_list[0].savory == 0) { food_list[0].savory = 1; }
                    if (food_list[0].sweet == 0) { food_list[0].sweet = 1; }
                    if (food_list[0].spicy == 0) { food_list[0].spicy = 1; }
                    if (food_list[0].mild == 0) { food_list[0].mild = 1; }

                    money += (food_list[0].savory * food_list[0].sweet * food_list[0].spicy * food_list[0].mild) / discrepancy;

                    food_list.Remove(food_list[0]);
                    if (customer_list[0].savory_stat == 0 && customer_list[0].sweet_stat == 0 && customer_list[0].spicy_stat == 0 && customer_list[0].mild_stat == 0) { customer_list.RemoveAt(0); } 
                }
            }
            Raylib.DrawRectangle((int)seller_knapp.X, (int)seller_knapp.Y, (int)seller_knapp.Width, (int)seller_knapp.Height, Colors(2));
            Raylib.DrawText($"sell", (int)seller_knapp.X, (int)seller_knapp.Y - 30, 20, Colors(2));


            // draws map
            for (int i = 0; i <= rows - 1; i++)
            {
                for (int j = 0; j <= cols - 1; j++)
                {
                    if (map[j, i] == 'a') Raylib.DrawRectangle(i * cube, j * cube, cube, cube, Colors(5));
                    if (map[j, i] == 'b') Raylib.DrawRectangle(i * cube, j * cube, cube, cube, Colors(4));
                }
            }
            Raylib.DrawRectangle(x * cube, y * cube, cube, cube, Colors(4));

            // draws money
            Raylib.DrawText($"Kr{money}", 0, 0, 20, Colors(2));
            
            // end
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}