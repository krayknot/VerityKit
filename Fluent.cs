namespace VerityKit;


public static class Fluent
{
public static Validator Require(this RuleRegistry registry, params string[] keys)
=> Validator.For(registry, keys);
}
