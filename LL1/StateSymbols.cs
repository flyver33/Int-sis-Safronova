
public class StateSymbols {
    private Dictionary<int, List<string>> stateSymbols;

    public StateSymbols() {
        stateSymbols  = new() {
            {10, new List<string>() {"ID", "Digit", "("}},
            {20, new List<string>() {"+"}},
            {21, new List<string>() {"-"}},
            {22, new List<string>() {")", "#"}},
            {30, new List<string>() {"ID", "Digit", "("}},
            {40, new List<string>() {"*"}},
            {41, new List<string>() {"/"}},
            {42, new List<string>() {"+", "-", ")", "#"}},
            {50, new List<string>() {"ID"}},
            {51, new List<string>() {"Digit"}},
            {52, new List<string>() {"("}},
            {100, new List<string>() {"ID", "Digit", "("}},
            {101, new List<string>() {"+", "-", ")", "#"}},
            {200, new List<string>() {"+"}},
            {201, new List<string>() {"ID", "Digit", "("}},
            {202, new List<string>() {"+", "-", ")", "#"}},
            {210, new List<string>() {"-"}},
            {211, new List<string>() {"ID", "Digit", "("}},
            {212, new List<string>() {"+", "-", ")", "#"}},
            {220, new List<string>() {")", "#"}},
            {300, new List<string>() {"ID", "Digit", "("}},
            {301, new List<string>() {"*", "/", "+", "-", ")", "#"}},
            {400, new List<string>() {"*"}},
            {401, new List<string>() {"ID", "Digit", "("}},
            {402, new List<string>() {"*", "/", "+", "-", ")", "#"}},
            {410, new List<string>() {"/"}},
            {411, new List<string>() {"ID", "Digit", "("}},
            {412, new List<string>() {"*", "/", "+", "-", ")", "#"}},
            {420, new List<string>() {"+", "-", ")", "#"}},
            {500, new List<string>() {"ID"}},
            {510, new List<string>() {"Digit"}},
            {520, new List<string>() {"("}},
            {521, new List<string>() {"ID", "Digit", "("}},
            {522, new List<string>() {")"}}
        };
    }

    public Dictionary<int, List<string>> GetDict() {
        return stateSymbols;
    }
}