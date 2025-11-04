using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace VerityKit;


public sealed record ValidationFailure(
string RuleKey,
string Message,
object? Value
);


public sealed class ValidationResult
{
private readonly List<ValidationFailure> _failures = new();
public bool IsValid => _failures.Count == 0;
public IReadOnlyList<ValidationFailure> Failures => _failures;
internal void AddFailure(ValidationFailure f) => _failures.Add(f);
}


public sealed class Rule
{
public string Key { get; }
public Type TargetType { get; }
public Func<object?, bool> Predicate { get; }
public string MessageTemplate { get; }


public Rule(string key, Type targetType, Func<object?, bool> predicate, string messageTemplate)
{
Key = key;
TargetType = targetType;
Predicate = predicate;
MessageTemplate = messageTemplate;
}


public bool TryEvaluate(object? value) => Predicate(value);
}
