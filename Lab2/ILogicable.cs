namespace Lab2;

public interface ILogicable
{
    void up(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);  
    void dn(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ups(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void dns(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ss(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void m(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
}
