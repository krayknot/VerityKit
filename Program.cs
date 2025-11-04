using System;
using System.Collections.Generic;
using VerityKit;


class Program
{
static void Main()
{
var registry = BuiltInRules.CreateDefault();
Console.WriteLine($"Built-in rules: {registry.Count}"); // should be >= 800


var email = "test@example.com";
var result = registry.Require("any.notNull", "string", "string.email", "string.length.atLeast.6")
.Validate(email);


Print(email, result);


int number = 42;
var numberResult = registry.Require("int", "int.positive", "int.divisibleBy.2", "int.atMost.50")
.Validate(number);
Print(number, numberResult);


var dob = new DateTime(1995, 5, 1);
var ageResult = registry.Require("date", "date.past", "date.age.atLeast.18")
.Validate(dob);
Print(dob, ageResult);


var list = new List<int> { 1,2,3 };
var listResult = registry.Require("collection", "collection.notEmpty", "collection.count.atLeast.2")
.Validate(list);
Print(list, listResult);
}


static void Print(object? value, ValidationResult result)
{
Console.WriteLine("Value: " + (value is System.Collections.IEnumerable and not string ? "[sequence]" : value));
Console.WriteLine("Valid: " + result.IsValid);
foreach (var f in result.Failures)
Console.WriteLine($" - {f.RuleKey}: {f.Message}");
Console.WriteLine();
}
}
