
using Raylib_cs;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class ingredient
{
    public float quality;
    public float savory;
    public float sweet;
    public float spicy;
    public float mild;
    public float cost;
    public string name;
    public int type;
    public Raylib_cs.Rectangle face = new Raylib_cs.Rectangle();
    public ingredient(float quality, float savory, float sweet, float spicy, float mild, int type)
    {
        face.Width = 400;
        face.Height = 80;
        face.X = 40;
        this.quality = quality;
        this.savory = savory;
        this.sweet = sweet;
        this.spicy = spicy;
        this.mild = mild;
        cost = quality * ((savory + sweet + spicy + mild) / 4);
        if (type == 1) { this.name = "cow meat"; this.savory = savory * quality; }
        if (type == 2) { this.name = "peas"; this.sweet = sweet * quality; }
        if (type == 3) { this.name = "pepper"; this.spicy = spicy * quality; }
        if (type == 4) { this.name = "milk"; this.mild = mild * quality; }
    }
}

class dishes
{
    public float quality;
    public float savory;
    public float sweet;
    public float spicy;
    public float mild;
    public float cost;
    public dishes(float quality, float savory, float sweet, float spicy, float mild)
    {
        this.quality = quality;
        this.savory = savory;
        this.sweet = sweet;
        this.spicy = spicy;
        this.mild = mild;
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

class vari
{
    public static Random rnd = new Random();
    public static int width = 1200;
    public static int height = 600;
    public static int phase = 1;

    public static int savory = 0;
    public static int sweet = 0;
    public static int spicy = 0;
    public static int mild = 0;

    public static int savory_storage = 0;
    public static int sweet_storage = 0;
    public static int spicy_storage = 0;
    public static int mild_storage = 0;

    public static int day = 1;

    public static float money = 10000;
    public static int discrepancy = 1;

    public static List<dishes> food_list = new List<dishes>();
    public static List<customer> customer_list = new List<customer>();

    public static List<ingredient> shop_list = new List<ingredient>();
    public static List<ingredient> ingredients_list = new List<ingredient>();
    public static List<ingredient> cooking_list = new List<ingredient>();
    public static List<dishes> cooked_list = new List<dishes>();

    public static int cube = 20;

    // buttons
    public static Raylib_cs.Rectangle cook_knapp = new Raylib_cs.Rectangle(400, 200, cube, cube);
    public static Raylib_cs.Rectangle ship_knapp = new Raylib_cs.Rectangle(600, 200, cube, cube);
    public static Raylib_cs.Rectangle customer_knapp = new Raylib_cs.Rectangle(1000, 200, cube, cube);
    public static Raylib_cs.Rectangle seller_knapp = new Raylib_cs.Rectangle(700, 200, cube, cube);
    public static Raylib_cs.Rectangle finish_knapp = new Raylib_cs.Rectangle(width - 140, height - 80, cube, cube);


    public static Raylib_cs.Rectangle reroll_knapp = new Raylib_cs.Rectangle(width / 2, 300, cube, cube);
}
class Program
{
    static Raylib_cs.Color Colors(int color)
    {
        if (color == 0) return Raylib_cs.Color.Black;
        if (color == 1) return Raylib_cs.Color.Gray;
        if (color == 2) return Raylib_cs.Color.White;
        if (color == 3) return Raylib_cs.Color.Blue;
        if (color == 4) return Raylib_cs.Color.SkyBlue;
        if (color == 5) return Raylib_cs.Color.Maroon;

        return Raylib_cs.Color.Black;
    }
    static void Shop()
    {
        // mouse
        Vector2 mouse_pos = Raylib.GetMousePosition();
        Raylib_cs.Rectangle mouse_collision = new Raylib_cs.Rectangle(mouse_pos.X, mouse_pos.Y, 1, 1);

        // shop
        if (Raylib.CheckCollisionRecs(mouse_collision, vari.reroll_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                vari.shop_list.Clear();
                for (int i = 0; i < 6; i++)
                {
                    vari.shop_list.Add(new ingredient((float)(vari.rnd.Next(5) + 1), (float)(vari.rnd.Next(9) + 1) + vari.rnd.NextSingle(), (float)(vari.rnd.Next(9) + 1) + vari.rnd.NextSingle(), (float)(vari.rnd.Next(9) + 1) + vari.rnd.NextSingle(), (float)(vari.rnd.Next(9) + 1) + vari.rnd.NextSingle(), vari.rnd.Next(4) + 1));
                }
            }
        }
        Raylib.DrawRectangle((int)vari.reroll_knapp.X, (int)vari.reroll_knapp.Y, (int)vari.reroll_knapp.Width, (int)vari.reroll_knapp.Height, Colors(2));
        Raylib.DrawText("reroll", (int)vari.reroll_knapp.X, (int)vari.reroll_knapp.Y, vari.cube, Colors(0));


        foreach (ingredient ingredient in vari.shop_list)
        {
            ingredient.face.Y = 40 + vari.shop_list.IndexOf(ingredient) * 90;
            Raylib.DrawRectangle((int)ingredient.face.X, (int)ingredient.face.Y, (int)ingredient.face.Width, (int)ingredient.face.Height, Colors(2));

            Raylib.DrawText($"Kr: {ingredient.cost:F2} Quality: {ingredient.quality} Name: {ingredient.name}", 40, (int)ingredient.face.Y, vari.cube, Colors(0));
            Raylib.DrawText($"Svry: {ingredient.savory:F2} Swt: {ingredient.sweet:F2} Spcy: {ingredient.spicy:F2} Mld: {ingredient.mild:F2}", 40, (int)ingredient.face.Y + 21, vari.cube, Colors(0));

            if (Raylib.CheckCollisionRecs(mouse_collision, ingredient.face))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    vari.money -= ingredient.cost;
                    vari.ingredients_list.Add(new ingredient(ingredient.quality, ingredient.savory, ingredient.sweet, ingredient.spicy, ingredient.mild, ingredient.type));
                }
            }
        }

        if (Raylib.CheckCollisionRecs(mouse_collision, vari.finish_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                vari.phase = 2;
            }
        }
        Raylib.DrawRectangle((int)vari.finish_knapp.X, (int)vari.finish_knapp.Y, (int)vari.finish_knapp.Width, (int)vari.finish_knapp.Height, Colors(2));
        Raylib.DrawText($"finish shop", (int)vari.finish_knapp.X, (int)vari.finish_knapp.Y - 30, vari.cube, Colors(2));
    }

    static void Selling()
    {
        // mouse
        Vector2 mouse_pos = Raylib.GetMousePosition();
        Raylib_cs.Rectangle mouse_collision = new Raylib_cs.Rectangle(mouse_pos.X, mouse_pos.Y, 1, 1);

        // button stuff

        // movews on too the next scene
        if (Raylib.CheckCollisionRecs(mouse_collision, vari.finish_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                vari.day += 1;
                for (int i = 0; i < vari.day; i++) { vari.customer_list.Add(new customer(vari.rnd.Next(11), vari.rnd.Next(11), vari.rnd.Next(11), vari.rnd.Next(11))); }
                vari.phase = 1;
            }
        }
        Raylib.DrawRectangle((int)vari.finish_knapp.X, (int)vari.finish_knapp.Y, (int)vari.finish_knapp.Width, (int)vari.finish_knapp.Height, Colors(2));
        Raylib.DrawText($"finish day", (int)vari.finish_knapp.X, (int)vari.finish_knapp.Y - 30, vari.cube, Colors(2));

        // may be removed ships the food ypu have cooked
        if (Raylib.CheckCollisionRecs(mouse_collision, vari.ship_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                vari.food_list.Add(new dishes(vari.savory, vari.sweet, vari.spicy, vari.mild));
            }
        }
        Raylib.DrawRectangle((int)vari.ship_knapp.X, (int)vari.ship_knapp.Y, (int)vari.ship_knapp.Width, (int)vari.ship_knapp.Height, Colors(2));
        Raylib.DrawText("ship", (int)vari.ship_knapp.X, (int)vari.ship_knapp.Y - 30, vari.cube, Colors(2));

        // adds customer dev tool
        if (Raylib.CheckCollisionRecs(mouse_collision, vari.customer_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                vari.customer_list.Add(new customer(vari.rnd.Next(10), vari.rnd.Next(10), vari.rnd.Next(10), vari.rnd.Next(10)));
            }
        }
        Raylib.DrawRectangle((int)vari.customer_knapp.X, (int)vari.customer_knapp.Y, (int)vari.customer_knapp.Width, (int)vari.customer_knapp.Height, Colors(2));
        Raylib.DrawText("new customer", (int)vari.customer_knapp.X, (int)vari.customer_knapp.Y - 30, vari.cube, Colors(2));

        // draws "customer_list"
        foreach (customer customer in vari.customer_list)
        {
            Raylib.DrawText($"{customer},{customer.savory_stat},{customer.sweet_stat},{customer.spicy_stat},{customer.mild_stat}", (int)vari.customer_knapp.X, (int)vari.customer_knapp.Y + vari.customer_list.IndexOf(customer) * vari.cube + vari.cube, 20, Colors(2));
        }

        // draws "food_list"
        foreach (dishes food in vari.food_list)
        {
            Raylib.DrawText($"{food},{food.savory},{food.sweet},{food.spicy},{food.mild}", (int)vari.ship_knapp.X, (int)vari.ship_knapp.Y + vari.food_list.IndexOf(food) * vari.cube + vari.cube, 20, Colors(2));
        }

        // sells doof on top of "food_list"
        if (Raylib.CheckCollisionRecs(mouse_collision, vari.seller_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                if (vari.customer_list[0].savory_stat <= vari.food_list[0].savory) { vari.discrepancy += (vari.food_list[0].savory - vari.customer_list[0].savory_stat); vari.customer_list[0].savory_stat = 0; }
                else vari.customer_list[0].savory_stat -= vari.food_list[0].savory;
                if (vari.customer_list[0].sweet_stat <= vari.food_list[0].sweet) { vari.discrepancy += (vari.food_list[0].sweet - vari.customer_list[0].sweet_stat); vari.customer_list[0].sweet_stat = 0; }
                else vari.customer_list[0].sweet_stat -= vari.food_list[0].sweet;
                if (vari.customer_list[0].spicy_stat <= vari.food_list[0].spicy) { vari.discrepancy += (vari.food_list[0].spicy - vari.customer_list[0].spicy_stat); vari.customer_list[0].spicy_stat = 0; }
                else vari.customer_list[0].spicy_stat -= vari.food_list[0].spicy;
                if (vari.customer_list[0].mild_stat <= vari.food_list[0].mild) { vari.discrepancy += (vari.food_list[0].mild - vari.customer_list[0].mild_stat); vari.customer_list[0].mild_stat = 0; }
                else vari.customer_list[0].mild_stat -= vari.food_list[0].mild;
                if (vari.food_list[0].savory == 0) { vari.food_list[0].savory = 1; }
                if (vari.food_list[0].sweet == 0) { vari.food_list[0].sweet = 1; }
                if (vari.food_list[0].spicy == 0) { vari.food_list[0].spicy = 1; }
                if (vari.food_list[0].mild == 0) { vari.food_list[0].mild = 1; }

                vari.money += (vari.food_list[0].savory * vari.food_list[0].sweet * vari.food_list[0].spicy * vari.food_list[0].mild) / vari.discrepancy;

                vari.food_list.Remove(vari.food_list[0]);
                if (vari.customer_list[0].savory_stat == 0 && vari.customer_list[0].sweet_stat == 0 && vari.customer_list[0].spicy_stat == 0 && vari.customer_list[0].mild_stat == 0) { vari.customer_list.RemoveAt(0); }
            }
        }
        Raylib.DrawRectangle((int)vari.seller_knapp.X, (int)vari.seller_knapp.Y, (int)vari.seller_knapp.Width, (int)vari.seller_knapp.Height, Colors(2));
        Raylib.DrawText($"sell", (int)vari.seller_knapp.X, (int)vari.seller_knapp.Y - 30, 20, Colors(2));


        vari.discrepancy = 1;

        // draws ingredients you have bought
        foreach (ingredient ingredient in vari.ingredients_list)
        {
            ingredient.face.Y = 40 + vari.ingredients_list.IndexOf(ingredient) * 90;
            Raylib.DrawRectangle((int)ingredient.face.X, (int)ingredient.face.Y, (int)ingredient.face.Width, (int)ingredient.face.Height, Colors(2));

            Raylib.DrawText($"Quality: {ingredient.quality} Name: {ingredient.name}", 40, (int)ingredient.face.Y, vari.cube, Colors(0));
            Raylib.DrawText($"Svry: {ingredient.savory:F2} Swt: {ingredient.sweet:F2} Spcy: {ingredient.spicy:F2} Mld: {ingredient.mild:F2}", 40, (int)ingredient.face.Y + 21, vari.cube, Colors(0));

            if (Raylib.CheckCollisionRecs(mouse_collision, ingredient.face))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    vari.cooking_list.Add(ingredient);
                }
            }
        }
        if (Raylib.CheckCollisionRecs(mouse_collision, vari.cook_knapp))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var
                var
                var
                var
                var
                foreach (ingredient ingredient in vari.cooking_list)
                {
                }
            }
        }
        Raylib.DrawRectangle((int)vari.cook_knapp.X, (int)vari.cook_knapp.Y, (int)vari.cook_knapp.Width, (int)vari.cook_knapp.Height, Colors(2));
        Raylib.DrawText($"cook", (int)vari.cook_knapp.X, (int)vari.cook_knapp.Y, vari.cube, Colors(2));


    }
    static void Main()
    {
        Raylib.InitWindow(vari.width, vari.height, "cheffing");
        // food selling
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Colors(3));
            Raylib.SetTargetFPS(60);


            if (vari.phase == 1)
            {
                Shop();
            }
            else Selling();

            // draws money and days
            Raylib.DrawText($"Kr{vari.money:F2}", 0, 0, 20, Colors(2));
            Raylib.DrawText($"Days{vari.day}", 200, 0, 20, Colors(2));

            // end
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}