public class NextStates {
    private Dictionary<int, int?> nextStates;

    public NextStates(Dictionary<int, int?> nextStates) {
        this.nextStates  = nextStates;
    }

    public Dictionary<int, int?> GetDict() {
        return nextStates;
    }
}