using System.Globalization;
var lo = T.CreateChecked(-n);
var hi = T.CreateChecked(n);
int cap = n;
r.Add(new Rule($"{name}.between.-{cap}_+{cap}", typeof(T), v => IsNum(v, out var x) && x >= lo && x <= hi, $"{{key}}: must be between -{cap} and +{cap}"));
r.Add(new Rule($"{name}.atLeast.{cap}", typeof(T), v => IsNum(v, out var x) && x >= T.CreateChecked(cap), $"{{key}}: must be ≥ {cap}"));
r.Add(new Rule($"{name}.atMost.{cap}", typeof(T), v => IsNum(v, out var x) && x <= T.CreateChecked(cap), $"{{key}}: must be ≤ {cap}"));
}


// Divisibility for 2..20
for (int d = 2; d <= 20; d++)
{
int div = d;
r.Add(new Rule($"{name}.divisibleBy.{div}", typeof(T), v => IsNum(v, out var x) && x % T.CreateChecked(div) == T.Zero, $"{{key}}: must be divisible by {div}"));
}
}


private static void AddDateRules(RuleRegistry r)
{
bool IsDate(object? v, out DateTime dt)
{
if (v is DateTime t) { dt = t; return true; }
return DateTime.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out dt);
}


r.Add(new Rule("date", typeof(DateTime), v => IsDate(v, out _), "{key}: value must be a DateTime or parseable date"));
r.Add(new Rule("date.past", typeof(DateTime), v => IsDate(v, out var d) && d < DateTime.UtcNow, "{key}: must be in the past"));
r.Add(new Rule("date.future", typeof(DateTime), v => IsDate(v, out var d) && d > DateTime.UtcNow, "{key}: must be in the future"));
r.Add(new Rule("date.today", typeof(DateTime), v => IsDate(v, out var d) && d.Date == DateTime.UtcNow.Date, "{key}: must be today (UTC)"));


// Age rules: age >= 1..120
for (int age = 1; age <= 120; age++)
{
int a = age;
r.Add(new Rule($"date.age.atLeast.{a}", typeof(DateTime), v => IsDate(v, out var dob) && GetAge(dob) >= a, $"{{key}}: age must be ≥ {a}"));
}


static int GetAge(DateTime dob)
{
var today = DateTime.UtcNow.Date;
int age = today.Year - dob.Year;
if (dob.Date > today.AddYears(-age)) age--;
return age;
}
}


private static void AddCollectionRules(RuleRegistry r)
{
bool IsCollection(object? v, out ICollection? c)
{
if (v is string) { c = null; return false; }
if (v is ICollection col) { c = col; return true; }
c = null; return false;
}


r.Add(new Rule("collection", typeof(ICollection), v => IsCollection(v, out _), "{key}: value must be a collection"));
r.Add(new Rule("collection.notEmpty", typeof(ICollection), v => IsCollection(v, out var c) && c!.Count > 0, "{key}: collection must not be empty"));
for (int n = 1; n <= 200; n++)
{
int cap = n;
r.Add(new Rule($"collection.count.atLeast.{cap}", typeof(ICollection), v => IsCollection(v, out var c) && c!.Count >= cap, $"{{key}}: count must be ≥ {cap}"));
}
}


private static void AddFormatRules(RuleRegistry r)
{
r.Add(new Rule("guid", typeof(Guid), v => v is Guid || (v is string s && Guid.TryParse(s, out _)), "{key}: must be a GUID"));
r.Add(new Rule("bool", typeof(bool), v => v is bool || bool.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture), out _), "{key}: must be boolean"));
r.Add(new Rule("currency.inr", typeof(decimal), v => decimal.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture), NumberStyles.Currency, new CultureInfo("en-IN"), out _), "{key}: must be a valid INR amount"));
r.Add(new Rule("country.iso2", typeof(string), v => v is string s && Regex.IsMatch(s, @"^[A-Z]{2}$"), "{key}: must be ISO-3166-1 alpha-2"));
r.Add(new Rule("country.iso3", typeof(string), v => v is string s && Regex.IsMatch(s, @"^[A-Z]{3}$"), "{key}: must be ISO-3166-1 alpha-3"));
r.Add(new Rule("language.iso639-1", typeof(string), v => v is string s && Regex.IsMatch(s, @"^[a-z]{2}$"), "{key}: must be ISO 639-1"));
}
}
