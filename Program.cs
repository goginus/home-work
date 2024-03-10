
using System;
using System.Collections.Generic;
using System.Linq;

public interface IGameObject
{
    int Id { get; set; }
    int PlayerId { get; set; }
}

public interface IMovable
{
    bool IsMoving { get; set; }
}

public class Spaceship : IGameObject, IMovable
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public bool IsMoving { get; set; }
}

public class Torpedo : IGameObject, IMovable
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public bool IsMoving { get; set; }
}

public class Meteorite : IGameObject
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
}

public static class GameObjectExtensions
{
    public static IEnumerable<Spaceship> Spaceships(this IEnumerable<IGameObject> gameObjects, int playerId)
    {
        return gameObjects.OfType<Spaceship>().Where(s => s.PlayerId == playerId);
    }

    public static IGameObject GameObjectById(this IEnumerable<IGameObject> gameObjects, int id)
    {
        return gameObjects.FirstOrDefault(obj => obj.Id == id);
    }

    public static IEnumerable<IGameObject> Movable(this IEnumerable<IGameObject> gameObjects)
    {
        return gameObjects.OfType<IMovable>().Where(obj => obj.IsMoving).Cast<IGameObject>();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<IGameObject> gameObjects = new List<IGameObject>
        {
            new Spaceship { Id = 1, PlayerId = 1, IsMoving = true },
            new Spaceship { Id = 2, PlayerId = 2, IsMoving = false },
            new Torpedo { Id = 3, PlayerId = 1, IsMoving = true },
            new Meteorite { Id = 4, PlayerId = 0 }
        };

        var player1Spaceships = gameObjects.Spaceships(1);
        Console.WriteLine("Космические корабли игрока 1:");
        foreach (var ship in player1Spaceships)
        {
            Console.WriteLine($"ID: {ship.Id}, В движении: {ship.IsMoving}");
        }

        var gameObject = gameObjects.GameObjectById(3);
        Console.WriteLine($"Игровой объект с ID 3 принадлежит игроку с ID: {gameObject?.PlayerId}");

        var movingObjects = gameObjects.Movable();
        Console.WriteLine("Объекты в движении:");
        foreach (var obj in movingObjects)
        {
            Console.WriteLine($"ID: {obj.Id}, Тип: {obj.GetType().Name}");
        }
    }
}
