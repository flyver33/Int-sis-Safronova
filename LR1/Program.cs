namespace LR1;

class Program
{
    static void Main(string[] args)
    {
        string pathToCase = "C:\\dev\\Int-sis\\LR1\\testCases\\positiveCase.txt";
        string Case = File.ReadAllText(pathToCase);
        
        string pathToTsv = "C:\\dev\\Int-sis\\LR1\\data\\LR1.tsv";
        string pathToRules = "C:\\dev\\Int-sis\\LR1\\data\\rules.txt";

        ActionsDict actDict = new();
        Dictionary<string, Func<int, Action>> actionsDict = actDict.GetDict();

        TsvReader reader = new();
        Dictionary<int, Dictionary<string, Action>> actionTable = reader.Read(pathToTsv, actionsDict);

        RulesReader rulesReader = new();

        LexerLR1 lexer = new LexerLR1(Case, rulesReader.Read(pathToRules));
        lexer.SplitLine();

        int curState = 0;
        string curSymbol = lexer.GetCurEl();

        Stack<int> stateStack = new();
        stateStack.Push(curState);

        Stack<string> symbolStack = new();

        try {
            while (!curSymbol.Equals("accepted")) {
                (int nextState, string nextSymbol) = actionTable[curState][curSymbol].Act(stateStack, symbolStack, curState, curSymbol, lexer);

                curState = nextState;
                curSymbol = nextSymbol;
            }
            Console.WriteLine("Выражение относится к этой грамматике");
        } catch (DoesNotBelongGrammarException) {
            Console.WriteLine("Выражение не относится к этой грамматике");
        }
    }
}
