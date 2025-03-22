# Policy
Manage the policy rules

[![Build status](https://ci.appveyor.com/api/projects/status/n3hxq342l2lywhlr/branch/main?svg=true)](https://ci.appveyor.com/project/gaelgael5/policy/branch/main)

## Syntax documentation
For detailed information on how to use the policy syntax, see the [Documentation of the syntax](https://github.com/Black-Beard-Sdk/Policy/blob/main/syntax.md).

## Testing with keycloak
For testing you can use keycloak [Documentation to install keycloak](https://github.com/Black-Beard-Sdk/Policy/blob/main/Install_keycloak.md).


## How to use the library

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

## Intégration with web site
If you have a web site, reference nuget package **Black.Beard.Policy.Web**
```csharp
WebApplicationBuilder builder;
builder.AddPolicy("file path", c => true );

var app = builder.Build();
app.ConfigurePolicy()
app.Run();

```

sample of policy file
```batch

/* base rules */
policy isAuthenticated
    : Identity.IsAuthenticated

policy isAnonymous  
    : !Identity.IsAuthenticated

policy IsAdmin
    : role = administrator

policy IsUser
    : role = user


/* default rules */
policy default
    : isAuthenticated

policy fallback
    : isAnonymous


/* web rules */
policy Mycontroler.get (web_root)
    : isAuthenticated

```
