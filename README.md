# Policy
Manage the policy rules



## any samples for create policy

Create an alias for using in the policy like claim name
```batch
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
```

create a policy like a role admin is required
```batch
policy p1 : role=admin
```

create a policy like a role can't be guest
```batch
policy p1 : role != guest
```

create a policy like a role admin is required
```batch
policy p1 : (role = admin)
```

create a policy like a role can't be guest
```batch
policy p1 : !(role == guest)
```

create a policy like a role can be admin or guest
```batch
policy p1 : role in [admin, guest]
```

create a policy like a role can't be admin or guest
```batch
policy p1 : role !in [admin, guest]
```

create a policy like a role must not to have admin and guest
```batch
policy p1 : role !has [admin, guest]
```

create a policy like a role must to have admin and guest
```batch
policy p1 : role has [admin, guest]
```

create a policy like a role should to have admin and guest
```batch
policy p1 : role? has [admin, guest]
```

create a policy like the value to evaluate is in another object with a property name equal to "property" and value equal to "1"
```batch
policy p1 : source2.property? = "1"
```

Ensure is authenticated
```batch
policy p1 : Identity.IsAuthenticated
```

Can ben anonymous
```batch
policy p1 : Identity.IsAuthenticated?
```

## How to use library

```csharp
string policyPayload = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
";

var policies = Policy.Evaluate(policyPayload);


```
