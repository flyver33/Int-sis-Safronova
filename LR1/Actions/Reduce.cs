using System.Runtime.InteropServices;

public class Reduce : Action {
    private int value;
    public Reduce(int value) {
        this.value = value;
    }
    public (int, string) Act(Stack<int> stateStack, Stack<string> symbolStack, int curState, string curSymbol, Lexer lexer) {
        (string symbol, List<string> symbols) = lexer.GetRules(value);

        symbols.ForEach(s => {
            stateStack.Pop();
            symbolStack.Pop();
        });

        symbolStack.Push(symbol);
        
        return (stateStack.Peek(), symbol);
    }
}