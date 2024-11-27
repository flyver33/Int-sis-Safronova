namespace Lab2;

class FloorsManager {
    
    private Dictionary<string, HashSet<int>> GetSortedFloors(HashSet<int> neededFloors, int currentFloor) {
        Dictionary<string, HashSet<int>> sortedFloors = new();

        sortedFloors["up"] = neededFloors.Where(x => x >= currentFloor).ToHashSet<int>();
        sortedFloors["dn"] = neededFloors.Where(x => x <= currentFloor).ToHashSet<int>();
        sortedFloors["_"] = neededFloors;

        return sortedFloors;
    }

    public string GetClosestFloor(HashSet<int> neededFloors, string direction, int currentFloor) {

        Dictionary<string, HashSet<int>> sortedFloors = GetSortedFloors(neededFloors, currentFloor);

        try {
            return sortedFloors[direction].MinBy(x => Math.Abs((long) x - currentFloor)).ToString();
        }
        catch(InvalidOperationException) {
            return "_";
        }
        
    }

    public int? GetDistantFloor(HashSet<int> neededFloors, string direction, int currentFloor) {

        Dictionary<string, HashSet<int>> sortedFloors = GetSortedFloors(neededFloors, currentFloor);

        try {
            return sortedFloors[direction].MaxBy(x => Math.Abs((long) x - currentFloor));
        }
        catch(InvalidOperationException) {
            throw;
        }
        
    }
}