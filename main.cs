using System;
using System.Threading;

public class Program {

  public static void Main(string[] args) {
    double wallProbability = 0.25;
    Console.Clear();
    Console.WriteLine("Witaj w uproszczonej RPG!");
    Console.WriteLine("Po rozpoczęciu, grę zakończysz klawiszem \"Q\"");
    Console.WriteLine("Sterujesz klawiszami W S A D :)");


    Console.WriteLine("");
    /*Console.WriteLine("Podaj wysokość planszy:");
    int height = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Podaj szerokość planszy:");
    int width = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Czy generować losowo ściany w trakcie gry? \n" +
    "Wpisz \"tak\" lub cokolwiek innego, jeśli nie, i naciśnij Enter:");
    string generateWallsInput = Console.ReadLine();
    if(generateWallsInput == "tak"){
      generateWalls = true;
    }*/
    Boolean generateWalls = true;
    int height = 30;
    int width = 50;

    string[,] map = new string[height, width];
    
    Tuple<int, int> playerPosition = new Tuple<int, int>(height/2, width/2);

    for(int i = 0; i < map.GetLength(0); i++){
      for(int j = 0; j < map.GetLength(1); j++){
        if(generateWalls){
          Random random = new Random();
          if(random.NextDouble() < wallProbability){
              map[i,j] = "█";
          } else {
            map[i,j] = ".";
          }
        } else {
          map[i,j] = ".";
        }
      }
    }
    Console.WriteLine("Zaczynamy!");
    map[playerPosition.Item1, playerPosition.Item2] = "P";
    printWholeMap(map);

    DateTime startDate = DateTime.Now;
    while(true){
      printWholeMap(map);
      ConsoleKeyInfo klawisz = Console.ReadKey(true);
      if(klawisz.Key == ConsoleKey.R){
        continue;
      } else if (klawisz.Key == ConsoleKey.Q){
        Console.Clear();
        Console.WriteLine("Koniec, dzięki za grę!");
        break;
      } else if (klawisz.Key == ConsoleKey.W){
        playerPosition = moveUp(playerPosition, 1, map);
      } else if (klawisz.Key == ConsoleKey.S){
        playerPosition = moveDown(playerPosition, 1, map);
      } else if (klawisz.Key == ConsoleKey.A){
        playerPosition = moveLeft(playerPosition, 1, map);
      } else if (klawisz.Key == ConsoleKey.D){
        playerPosition = moveRight(playerPosition, 1, map);
      }
      if(checkIfWon(playerPosition, map)){
        Console.WriteLine("Gratulacje wygranej!");
        break;
      }
    }
    DateTime endDate = DateTime.Now;
    Console.WriteLine("Wyjście z labiryntu zajęło Ci " + (endDate - startDate).TotalSeconds + " sekund!");
  }

  public static Boolean checkIfWon(Tuple<int, int> playerPosition,string[,] map){
    if(playerPosition.Item1 <= 0 || playerPosition.Item1 >= map.GetLength(0)-1){
      return true;
    }
    if(playerPosition.Item2 <= 0 || playerPosition.Item2 >= map.GetLength(1)-1){
      return true;
    }
    return false;
  }

  public static void printWholeMap(string[,] mapToPrint){
    Console.Clear();
    for(int i = 0; i < mapToPrint.GetLength(0); i++){
      for(int j = 0; j < mapToPrint.GetLength(1); j++){
        Console.Write(mapToPrint[i,j]);
      }
      Console.WriteLine();
    }
      Console.WriteLine();
    Console.WriteLine("Grę zakończysz klawiszem \"Q\" w dowolnej chwili");
  }

  public static Boolean canMoveThere(string[,] map, Tuple<int, int> newPosition){
    if(newPosition.Item1 < 0 || newPosition.Item1 >= map.GetLength(0)){
      return false;
    }
    if(newPosition.Item2 < 0 || newPosition.Item2 >= map.GetLength(1)){
      return false;
    }
    if(map[newPosition.Item1, newPosition.Item2]!="."){
      return false;
    }
    return true;
  }

  public static Tuple<int, int> moveUp(Tuple<int, int> lastPosition, int length, string[,] map){
    Tuple<int, int> newPosition = new Tuple<int, int>(lastPosition.Item1 - length, lastPosition.Item2);
    if(canMoveThere(map, newPosition)==true){
      map[lastPosition.Item1, lastPosition.Item2] = ".";
      map[newPosition.Item1, newPosition.Item2] = "P";
      return newPosition;
      }
    return lastPosition;
  }

  public static Tuple<int, int> moveDown(Tuple<int, int> lastPosition, int length, string[,] map){
    Tuple<int, int> newPosition = new Tuple<int, int>(lastPosition.Item1 + length, lastPosition.Item2);
    if(canMoveThere(map, newPosition)==true){
      map[lastPosition.Item1, lastPosition.Item2] = ".";
      map[newPosition.Item1, newPosition.Item2] = "P";
      return newPosition;
      }
    return lastPosition;
  }
  
  public static Tuple<int, int> moveLeft(Tuple<int, int> lastPosition, int length, string[,] map){
    Tuple<int, int> newPosition = new Tuple<int, int>(lastPosition.Item1, lastPosition.Item2 - length);
    if(canMoveThere(map, newPosition)==true){
      map[lastPosition.Item1, lastPosition.Item2] = ".";
      map[newPosition.Item1, newPosition.Item2] = "P";
      return newPosition;
      }
    return lastPosition;
  }
  
  public static Tuple<int, int> moveRight(Tuple<int, int> lastPosition, int length, string[,] map){
    Tuple<int, int> newPosition = new Tuple<int, int>(lastPosition.Item1, lastPosition.Item2 + length);
    if(canMoveThere(map, newPosition)==true){
      map[lastPosition.Item1, lastPosition.Item2] = ".";
      map[newPosition.Item1, newPosition.Item2] = "P";
      return newPosition;
      }
    return lastPosition;
  }
}