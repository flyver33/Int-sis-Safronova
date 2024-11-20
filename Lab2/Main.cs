namespace Lab2;
using System;

class Lab2{
    public static void Main(string[] args) {
        IEnumerable<String> input_lines = File.ReadLines("C:\\dev\\Int-sis\\Lab2\\input.txt");
        
        int maxFloor = Convert.ToInt32(input_lines.ElementAt(0));

        List<int> currentFloors = new();
        string[] curFloors = input_lines.ElementAt(1).Split(' ');
        foreach (string floor in curFloors) {
            currentFloors.Add(Convert.ToInt32(floor));
        }

        Queue<(int, int)> calls = new();
        foreach (string floors in input_lines.Skip(2)) {
            string[] curCall = floors.Split(' ');
            calls.Enqueue((Convert.ToInt32(curCall[0]), Convert.ToInt32(curCall[1])));
        }

        MyOperator myoperator = new MyOperator(maxFloor, currentFloors, calls, 2);

    }

}