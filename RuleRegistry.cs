namespace VerityKit;


public sealed class RuleRegistry
{
private readonly Dictionary<string, Rule> _rules = new(StringComparer.OrdinalIgnoreCase);


public void Add(Rule rule)
{
if (string.IsNullOrWhiteSpace(rule.Key)) throw new ArgumentException("Key required");
_rules[rule.Key] = rule;
}


public bool TryGet(string key, out Rule rule) => _rules.TryGetValue(key, out rule!);


public IEnumerable<string> Keys => _rules.Keys.OrderBy(k => k);


public int Count => _rules.Count;
}
