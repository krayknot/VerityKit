namespace VerityKit;


public sealed class Validator
{
private readonly RuleRegistry _registry;
private readonly List<string> _ruleKeys = new();


public Validator(RuleRegistry registry) => _registry = registry;


public Validator Rule(string key)
{
_ruleKeys.Add(key);
return this;
}


public ValidationResult Validate(object? value)
{
var result = new ValidationResult();
foreach (var key in _ruleKeys)
{
if (!_registry.TryGet(key, out var rule))
{
result.AddFailure(new ValidationFailure(key, $"Rule '{key}' not found", value));
continue;
}


if (!rule.TryEvaluate(value))
{
var msg = rule.MessageTemplate.Replace("{key}", rule.Key)
.Replace("{type}", rule.TargetType.Name)
.Replace("{value}", value?.ToString() ?? "<null>");
result.AddFailure(new ValidationFailure(rule.Key, msg, value));
}
}
return result;
}


// Helper
public static Validator For(RuleRegistry registry, params string[] keys)
=> new Validator(registry).Rules(keys);


public Validator Rules(IEnumerable<string> keys)
{
_ruleKeys.AddRange(keys);
return this;
}
}
