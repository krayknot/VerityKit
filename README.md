# VerityKit
A fast, extensible validation toolkit for .NET with 800+ built-in validations out of the box. Works with primitives, strings, dates, collections, and formats. No dependencies. .NET 8.

## Install
```bash
# from source
cd src
# add the project to your solution and reference it
```


## Quick start
```csharp
var registry = BuiltInRules.CreateDefault();
var result = registry.Require("any.notNull", "string.email", "string.length.atLeast.8")
.Validate(inputEmail);
if (!result.IsValid)
{
// surface messages
}
```


## Features
- 800+ built-in rules generated at startup.
- Fluent composition: `registry.Require(...).Validate(value)`.
- Strong categories: `string.*`, `{int|long|float|double|decimal}.*`, `date.*`, `collection.*`, `guid`, `bool`, `country.*`, `language.*`.
- Clear failure messages.
- Open registry: add or replace rules by key.


## Adding custom rules
```csharp
var reg = BuiltInRules.CreateDefault();
reg.Add(new Rule(
key: "string.contains.jockow",
targetType: typeof(string),
predicate: v => v is string s && s.Contains("Jockow", StringComparison.OrdinalIgnoreCase),
messageTemplate: "{key}: must contain 'Jockow'"
));
```


## Rule keys cheat sheet
- `any.notNull` | `any.null`
- `string.*` basics: `notEmpty`, `notBlank`, `alpha`, `alnum`, `numeric`, `upper`, `lower`, `digit`, `symbol`
- `string.length.atLeast.N` and `string.length.atMost.N` for N=1..300
- `string.startsWith.X` and `string.endsWith.X` for A..Z
- `string.email`, `string.url`, `string.ipv4`, `string.ipv6`, `string.uuid`, `string.e164`, `string.indianMobile`, `string.panIndia`, `string.ifscIndia`, `string.pincodeIndia`, `string.isoDate`, `string.base64`, `string.password.basic`, `string.password.strong`
- Numeric packs for `int|long|float|double|decimal`:
- `{type}.positive`, `{type}.nonNegative`, `{type}.negative`
- `{type}.between.-N_+N`, `{type}.atLeast.N`, `{type}.atMost.N` for N=1..50
- `{type}.divisibleBy.D` for D=2..20 (mod for non-integers uses numeric remainder)
- `date` basics: `date.past`, `date.future`, `date.today`, `date.age.atLeast.N` for N=1..120
- Collections: `collection`, `collection.notEmpty`, `collection.count.atLeast.N` for N=1..200
- Formats: `guid`, `bool`, `currency.inr`, `country.iso2`, `country.iso3`, `language.iso639-1`


## Counts
With the generators, total rule count is typically >800. A guard rule `meta.min500` fails if count < 500.


## Performance
All rules compile to delegates. No reflection at validation time. Rule lookup is O(1). Validation is O(R).


## Versioning
SemVer. Breaking key changes bump major.


## License
MIT
