public class ActionsDict {
    public Dictionary<string, Func<int, Action>> GetDict() {
        return new()
        {
            { "s", (value) => new Shift(value) },
            { "r", (value) => new Reduce(value) },
            { "b", (value) => new Back(value) },
            { "a", (value) => new Accept(value) },
            { "e", (value) => new Error(value) }
        };
    }
}