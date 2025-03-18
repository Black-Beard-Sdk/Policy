# Policy
Manage the policy rules

[![Build status](https://ci.appveyor.com/api/projects/status/n3hxq342l2lywhlr/branch/main?svg=true)](https://ci.appveyor.com/project/gaelgael5/policy/branch/main)

## any samples for Create policy

Create an alias for using in the policy like claim name
```batch
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
```

Create a policy like a claim ope is required but the value is not important
```batch
policy p1 : ope+
```

Create a policy like a role admin is required
```batch
policy p1 : role=admin
```

Create a policy like a role can't be guest
```batch
policy p1 : role != guest
```

Create a policy like a role admin is required
```batch
policy p1 : (role = admin)
```

Create a policy like a role can't be guest
```batch
policy p1 : !(role == guest)
```

Create a policy like a role can be admin or guest
```batch
policy p1 : role in [admin, guest]
```

Create a policy like a role can't be admin or guest
```batch
policy p1 : role !in [admin, guest]
```

Create a policy like a role must not to have admin and guest
```batch
policy p1 : role !has [admin, guest]
```

Create a policy like a role must to have admin and guest
```batch
policy p1 : role has [admin, guest]
```

Create a policy like the value to evaluate is in another object with a property name equal to "property" and value equal to "1"
```batch
policy p1 : source2.property = "1"
```

Create a policy like identity must be authenticated
```batch
policy p1 : Identity.IsAuthenticated = true
```

Add category on a rule
```batch
policy p1 (web) : Identity.IsAuthenticated = true
```

## How to use library

```csharp

string policyPayload = @"// your rules";

var policies = Policy.Evaluate(policyPayload);
if (!policies.Diagnostics.Success)
    throw new Exception("Failed to evaluate file policies");
var evaluator = new PolicyEvaluator(policies);
if (evaluator.Evaluate(policyRule.Name, c.User, out RuntimeContext context))
{
    // access granted
}
```


If you have a web site, reference nuget package **Black.Beard.Policy.Web**
```csharp
WebApplicationBuilder builder;
builder.AddPolicy("file path", c => true );
```

sample of policy file
```batch

alias mycustomrole : "http://schemas.microsoft.com/ws/2000/06/identity/claims/role"

policy default : Identity.IsAuthenticated = true
policy fallback : Identity.IsAuthenticated = true
policy admin : mycustomrole = admin
policy user : mycustomrole = user

```
