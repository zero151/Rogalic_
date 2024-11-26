using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Рогалик
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int kills = 0;
            int damage = 0;
            int useAids = 0;
            int deaths = 0;
            List<Weapon> weapons = new List<Weapon>(5);
            weapons.Add(new Weapon("Деревянный меч", 15));
            weapons.Add(new Weapon("Железный меч", 25));
            weapons.Add(new Weapon("Алмазный меч", 50));
            weapons.Add(new Weapon("Кинжал", 20));
            weapons.Add(new Weapon("Пластиковый ножик", 5));

            List<Enemy> enemies = new List<Enemy>(7);
            enemies.Add(new Enemy("Гоблин-маг", 20, 20, 5));
            enemies.Add(new Enemy("Мегаскелет", 30, 30, 10));
            enemies.Add(new Enemy("Воин", 25, 25, 7));
            enemies.Add(new Enemy("Нежить с мёртвыми", 40, 40, 15));
            enemies.Add(new Enemy("Переросток огр", 50, 50, 40));
            enemies.Add(new Enemy("Золотой скелет", 10, 10, 50));
            enemies.Add(new Enemy("Неизвентный", 45, 45, 20));

            List<Aid> healing = new List<Aid>(6);
            healing.Add(new Aid("Бинт", 5, 1));
            healing.Add(new Aid("Маленькая аптечка", 10, 1));
            healing.Add(new Aid("Средняя аптечка", 20, 1));
            healing.Add(new Aid("Большая аптечка", 30, 1));
            healing.Add(new Aid("Военная аптечка", 50, 1));
            healing.Add(new Aid("У вас нет аптечки", 0, 1));
            Random rnd = new Random();
        Start:
            Console.WriteLine("Вы вошли в тёмное подземелье с монстрами! Игра начинается)");
            Console.Write("Кто ты воин? Назови себя: ");
            string n = Console.ReadLine();
            Player player = new Player(n);
            Console.WriteLine("\nПриветсвуем тебя воин " + player.name + " настало и твоё время побороться за сокровища, что хранит в себе это подземелье");

            while (player.health > 0)
            {
                Console.WriteLine("Ты имеешь " + player.health + " здоровья и " + player.aid.name + " и в руках у вас находиться: " + player.weapon.name);
                int e = rnd.Next(0, enemies.Count);
                Enemy enemy = enemies[e];
                enemy.health = enemy.maxHealth;
                int enemyWeapon = rnd.Next(0, weapons.Count);
                enemy.weapon = weapons[enemyWeapon];
                Console.WriteLine("Он имеет " + enemy.health + "хп и в руках он держит " + enemy.weapon.name + "! (урон оружия: " + enemy.weapon.damage + ")");
                while (player.health > 0 && enemy.health > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ваши действия?\n" +
                        "1 - Атаковать\n" +
                        "2 - Применить аптечку\n" +
                        "3 - Попробовать избежать боя\n");
                    int action = Convert.ToInt32(Console.ReadLine());
                    switch (action)
                    {
                        case 1:
                            Console.WriteLine();
                            enemy.health -= player.weapon.damage;
                            damage += player.weapon.damage;
                            Console.WriteLine("Вы ударили " + enemy.name + " и нанесли ему: " + player.weapon.damage);
                            Console.WriteLine("У него осталось " + enemy.health + " здоровья");
                            break;
                        case 2:
                            Console.WriteLine();
                            if (player.aid.value > 0)
                            {
                                useAids += 1;
                                player.health += player.aid.healthRestore;
                                Console.WriteLine("Вы излечили себя и у вас " + player.health + " здоровья");
                                player.aid.value -= 1;
                                if (player.aid.value <= 0)
                                {
                                    player.aid = healing[5];
                                }
                            }
                            break;
                        case 3:
                            int escape = rnd.Next(0, 4);
                            if (escape == 3)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Вы смогли сбежали");
                                enemy.health = 0;
                                continue;
                            }
                            else
                            {
                                Console.WriteLine(escape);
                                Console.WriteLine("У вас не получилось сбежать");
                                escape = rnd.Next(0, 4);
                                break;
                            }
                    }
                    if (enemy.health > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine(enemy.name + " атакует вас и наносит вам " + enemy.weapon.damage + " урона");
                        player.health -= enemy.weapon.damage;
                        Console.WriteLine("У вас сейчас " + player.health + " здоровья и " + player.aid.name + " и в руках у вас сияет: " + player.weapon.name);
                    }
                    else
                    {
                        kills += 1;
                        player.point += enemy.pointsaward;
                        Console.WriteLine();
                        Console.WriteLine(enemy.name + " был побеждён тобой!");
                        int a = rnd.Next(0, healing.Count);
                        Aid guh = healing[a];
                        Console.WriteLine("В  трупе " + enemy.name + " вы нашли: " + enemy.weapon.name + " и " + guh.name);
                        string choice;
                        Console.WriteLine("Хотите ли вы заменить свою аптечку на его аптечку?(Да/Нет)");
                        choice = Console.ReadLine();
                        if (choice == "Да" || choice == "да")
                        {
                            player.aid = healing[a];
                        }
                        Console.WriteLine("Хотите ли вы забрать его оружие?(Да/Нет)");
                        choice = Console.ReadLine();
                        if (choice == "Да" || choice == "да")
                        {
                            player.aid = healing[a];
                            player.weapon = enemy.weapon;
                        }
                    }
                }
            }
            deaths += 1;
            Console.WriteLine();
            Console.WriteLine("Вы сражались в посиках великих сокровищ, но это испытание всё равно поглотило тебя");
            Console.WriteLine();
            Console.WriteLine("Статистика:");
            Console.WriteLine("Монстров было побеждено: " + kills);
            Console.WriteLine("Очков заработано: " + player.point);
            Console.WriteLine("Аптечек использовано: " + useAids);
            Console.WriteLine("Урона нанесено: " + damage);
            Console.WriteLine("Смерти: " + deaths);
            Console.WriteLine("Начать сначала?(Да/Нет)");
            string c;
            c = Console.ReadLine();
            if (c == "Да" || c == "да")
            {
                Console.WriteLine();
                goto Start;
            }
        }
    }
}