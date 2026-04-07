
using Raylib_cs;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

class crate
{
    public int type;
    public int savory;
    public int sweet;
    public int spicy;
    public int mild;
    public int cost;
    public string name;
    public Raylib_cs.Rectangle face = new Raylib_cs.Rectangle();
    public crate(int type)
    {
        face.Width = 40;
        face.Height = face.Width;
        face.X = 20;
        this.type = type;
        if (type == 1) { name = "savory_1"; cost = 100; savory = 10; sweet = 0; spicy = 0; mild = 0; }
        if (type == 2) { name = "savory_2"; cost = 200; savory = 20; sweet = 0; spicy = 0; mild = 0; }
        if (type == 3) { name = "savory_3"; cost = 400; savory = 40; sweet = 0; spicy = 0; mild = 0; }
        if (type == 4) { name = "savory_sweet_1"; cost = 200; savory = 8; sweet = 8; spicy = 0; mild = 0; }
        if (type == 5) { name = "savory_sweet_2"; cost = 400; savory = 16; sweet = 16; spicy = 0; mild = 0; }
        if (type == 6) { name = "savory_sweet_3"; cost = 1600; savory = 32; sweet = 32; spicy = 0; mild = 0; }
    }
}

class dishes
{
    public int savory = 0;
    public int sweet = 0;
    public int spicy = 0;
    public int mild = 0;
    public dishes(int asavory, int asweet, int aspicy, int amild)
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
    public customer(int asavory_stat, int asweet_stat, int aspicy_stat, int amild_stat)
    {
        savory_stat = asavory_stat;
        sweet_stat = asweet_stat;
        spicy_stat = aspicy_stat;
        mild_stat = amild_stat;
    }
}

class Program
{
    static void Main()
    { 
        Random rnd = new Random();
        int width = 1200;
        int height = 600;
        
        int savory = 0;
        int sweet = 0;
        int spicy = 0;
        int mild = 0;

        int savory_storage = 0;
        int sweet_storage = 0;
        int spicy_storage = 0;
        int mild_storage = 0;

        int day = 1;

        int money = 10000;
        int discrepancy = 1;

        List<dishes> food_list = new List<dishes>();
        List<customer> customer_list = new List<customer>();
        List<crate> crate_list = new List<crate>();

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

        Raylib_cs.Rectangle buy_savory_knapp = new Raylib_cs.Rectangle(300, 400, cube, cube);
        Raylib_cs.Rectangle buy_sweet_knapp = new Raylib_cs.Rectangle(400, 400, cube, cube);
        Raylib_cs.Rectangle buy_spicy_knapp = new Raylib_cs.Rectangle(500, 400, cube, cube);
        Raylib_cs.Rectangle buy_mild_knapp = new Raylib_cs.Rectangle(600, 400, cube, cube);

        Raylib_cs.Rectangle ship_knapp = new Raylib_cs.Rectangle(600, 200, cube, cube);
        Raylib_cs.Rectangle customer_knapp = new Raylib_cs.Rectangle(1000, 200, cube, cube);
        Raylib_cs.Rectangle seller_knapp = new Raylib_cs.Rectangle(700, 200, cube, cube);
        Raylib_cs.Rectangle finnish_knapp = new Raylib_cs.Rectangle(width - 140, height - 80, cube, cube);


        Raylib.InitWindow(width, height, "game i made");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Colors(3));
            Raylib.SetTargetFPS(60);


            // mouse
            Vector2 mouse_pos = Raylib.GetMousePosition();
            Raylib_cs.Rectangle mouse_collision = new Raylib_cs.Rectangle(mouse_pos.X, mouse_pos.Y, 1, 1);


            // shop
            foreach (var crate in crate_list)
            {
                crate.face.Y = 400 + crate_list.IndexOf(crate) * 60;
                Raylib.DrawRectangle((int)crate.face.X, (int)crate.face.Y, (int)crate.face.Width, (int)crate.face.Height, Colors(2));
               
                Raylib.DrawText(crate.name, cube, (int)crate.face.Y, cube, Colors(0));
                
                if (Raylib.CheckCollisionRecs(mouse_collision, crate.face))
                {
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        money -= crate.cost;
                        savory_storage += crate.savory;
                        sweet_storage += crate.sweet;
                        spicy_storage += crate.spicy;
                        mild_storage += crate.mild;
                    }
                }
            }

            // button stuff
            if (Raylib.CheckCollisionRecs(mouse_collision, savory_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    savory_storage -= 1;
                    savory += 1;
                }
            }
            Raylib.DrawRectangle((int)savory_knapp.X, (int)savory_knapp.Y, (int)savory_knapp.Width, (int)savory_knapp.Height, Colors(2));
            Raylib.DrawText($"savory:{savory}", (int)savory_knapp.X, (int)savory_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, sweet_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    sweet_storage -= 1;
                    sweet += 1;
                }
            }
            Raylib.DrawRectangle((int)sweet_knapp.X, (int)sweet_knapp.Y, (int)sweet_knapp.Width, (int)sweet_knapp.Height, Colors(2));
            Raylib.DrawText($"sweet:{sweet}", (int)sweet_knapp.X, (int)sweet_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, spicy_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    spicy_storage -= 1;
                    spicy += 1;
                }
            }
            Raylib.DrawRectangle((int)spicy_knapp.X, (int)spicy_knapp.Y, (int)spicy_knapp.Width, (int)spicy_knapp.Height, Colors(2));
            Raylib.DrawText($"spicy:{spicy}", (int)spicy_knapp.X, (int)spicy_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, mild_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    mild_storage -= 1;
                    mild += 1;
                }
            }
            Raylib.DrawRectangle((int)mild_knapp.X, (int)mild_knapp.Y, (int)mild_knapp.Width, (int)mild_knapp.Height, Colors(2));
            Raylib.DrawText($"mild:{mild}", (int)mild_knapp.X, (int)mild_knapp.Y - 30, cube, Colors(2));


            if (Raylib.CheckCollisionRecs(mouse_collision, buy_savory_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    money -= 10;
                    savory_storage += 1;
                }
            }
            Raylib.DrawRectangle((int)buy_savory_knapp.X, (int)buy_savory_knapp.Y, (int)buy_savory_knapp.Width, (int)buy_savory_knapp.Height, Colors(2));
            Raylib.DrawText($"savory, 10kr", (int)buy_savory_knapp.X, (int)buy_savory_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, buy_sweet_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    money -= 10;
                    sweet_storage += 1;
                }
            }
            Raylib.DrawRectangle((int)buy_sweet_knapp.X, (int)buy_sweet_knapp.Y, (int)buy_sweet_knapp.Width, (int)buy_sweet_knapp.Height, Colors(2));
            Raylib.DrawText($"sweet,10kr", (int)buy_sweet_knapp.X, (int)buy_sweet_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, buy_spicy_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    money -= 10;
                    spicy_storage += 1;
                }
            }
            Raylib.DrawRectangle((int)buy_spicy_knapp.X, (int)buy_spicy_knapp.Y, (int)buy_spicy_knapp.Width, (int)buy_spicy_knapp.Height, Colors(2));
            Raylib.DrawText($"spicy,10kr", (int)buy_spicy_knapp.X, (int)buy_spicy_knapp.Y - 30, cube, Colors(2));
            if (Raylib.CheckCollisionRecs(mouse_collision, buy_mild_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    money -= 10;
                    mild_storage += 1;
                }
            }
            Raylib.DrawRectangle((int)buy_mild_knapp.X, (int)buy_mild_knapp.Y, (int)buy_mild_knapp.Width, (int)buy_mild_knapp.Height, Colors(2));
            Raylib.DrawText($"mild,10kr", (int)buy_mild_knapp.X, (int)buy_mild_knapp.Y - 30, cube, Colors(2));

            Raylib.DrawText($"storage:{savory_storage},{sweet_storage},{spicy_storage},{mild_storage}", (int)buy_mild_knapp.X + 200, (int)buy_mild_knapp.Y - 30, cube, Colors(2));


            if (Raylib.CheckCollisionRecs(mouse_collision, finnish_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    crate_list.Clear();
                    day += 1;
                    for (int i = 0; i < day; i++) { customer_list.Add(new customer(rnd.Next(10), rnd.Next(10), rnd.Next(10), rnd.Next(10))); }
                    for (int i = 0; i < 3; i++)
                    {
                        int num;
                        num = rnd.Next(5) + 1;
                        crate_list.Add(new crate(num));
                    }
                }
            }
            Raylib.DrawRectangle((int)finnish_knapp.X, (int)finnish_knapp.Y, (int)finnish_knapp.Width, (int)finnish_knapp.Height, Colors(2));
            Raylib.DrawText($"finnish day", (int)finnish_knapp.X, (int)finnish_knapp.Y - 30, cube, Colors(2));


            if (Raylib.CheckCollisionRecs(mouse_collision, ship_knapp))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    food_list.Add(new dishes(savory, sweet, spicy, mild));
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
                    customer_list.Add(new customer(rnd.Next(10), rnd.Next(10), rnd.Next(10), rnd.Next(10)));
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


            // draws money and days
            Raylib.DrawText($"Kr{money}", 0, 0, 20, Colors(2));
            Raylib.DrawText($"Days{day}", 100, 0, 20, Colors(2));
            discrepancy = 1;

            // end
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}