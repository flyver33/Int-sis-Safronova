
public class StateSymbols {
    private Dictionary<int, List<string>> stateSymbols;

    public StateSymbols(Dictionary<int, List<string>> stateSymbols) {
        this.stateSymbols  = stateSymbols;
    }

    public Dictionary<int, List<string>> GetDict() {
        return stateSymbols;
    }
}