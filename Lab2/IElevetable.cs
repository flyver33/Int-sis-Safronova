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
}
