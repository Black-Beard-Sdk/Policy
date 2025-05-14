# Policy
The Black.Beard.Policies library is designed to help developers manage and evaluate authorization policies in .NET applications. It provides a flexible syntax for defining rules based on user claims, roles, and other contextual data. With this library, you can easily create, organize, and enforce complex access control policies, making it suitable for scenarios where fine-grained security and dynamic rule evaluation are required. It also integrates with web applications and supports external identity providers like Keycloak for authentication testing.


[![Build status](https://ci.appveyor.com/api/projects/status/n3hxq342l2lywhlr/branch/main?svg=true)](https://ci.appveyor.com/project/gaelgael5/policy/branch/main)

## Documentation
For detailed information on how to use the policy syntax, see the [Documentation of the syntax](https://github.com/Black-Beard-Sdk/Policy/blob/main/syntax.md).

## Testing with keycloak
For testing you can use keycloak [Documentation to install keycloak](https://github.com/Black-Beard-Sdk/Policy/blob/main/Install_keycloak.md).

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

Create a policy like the value to evaluate is in another object with a property name equal to "property" and value equal to 1
```batch
policy p1 : source2.property = 1
```

Create a policy like the value to evaluate is in another object with a property name equal to "property" and value equal to 1
```batch
policy p1 : source2.property = 1
```

Create a policy like that check if greater than 18
```batch
policy isAdult : Identity.Age >= 18
```

Add category on a rule
```batch
policy p1 (web) : Identity.IsAuthenticated
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
