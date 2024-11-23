namespace Lab2;

public interface IElevatable
{
    void GoDown();
    void OpenDoors();
    void CloseDoors();
    void GoUp();
    int GetCurrentFloor();
    string GetID();
    void RemoveFloor(int floor);
    void AddFloor(int floor);
    string GetState();
    void SetState(string nextState);
    string GetClosestFloor();
    int? GetDistantFloor();
    void SetDirNo();
    void AddOperated((int, int) addingElement);
    void AddRangeOperated(List<(int, int)> addingList);
    List<(int, int)> FindAllOperated(Predicate<(int, int)> match);
    int RemoveAllOperated(Predicate<(int, int)> match);
    List<(int, int)> GetOperated();
    void AddTaken((int, int) addingElement);
    void AddRangeTaken(List<(int, int)> addingList);
    List<(int, int)> FindAllTaken(Predicate<(int, int)> match);
    int RemoveAllTaken(Predicate<(int, int)> match);
    List<(int, int)> GetTaken();
}
